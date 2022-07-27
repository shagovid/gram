using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Common.BaseDTOs;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Services;
using Rossgram.Application.Conversations.Queries.GetConversations;
using Rossgram.Domain.Entities.Messages;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors.Access;
using Rossgram.Domain.Views;

namespace Rossgram.Application.Conversations.Queries.GetMessages;

public class GetMessagesQueryHandler
    : IRequestHandler<GetMessagesQuery, GetMessagesResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly ObjectsStorageService _objectsStorage;
    private readonly IAppDbContext _context;

    public GetMessagesQueryHandler(
        ICurrentAuth auth,
        ObjectsStorageService objectsStorage,
        IAppDbContext context)
    {
        _auth = auth;
        _objectsStorage = objectsStorage;
        _context = context;
    }

    public async Task<GetMessagesResponseDTO> Handle(
        GetMessagesQuery request,
        CancellationToken cancellationToken)
    {
        // Query information about conversation
        long conversationId;
        if (request.RecipientId is not null)
        {
            long olderId = Math.Min(_auth.Id, request.RecipientId!.Value);
            long newerId = Math.Max(_auth.Id, request.RecipientId!.Value);
            PrivateConversation? privateConversation = await _context.Conversations
                .OfType<PrivateConversation>()
                .SingleOrDefaultAsync(
                    predicate: x => x.OlderAccountId == olderId && x.NewerAccountId == newerId,
                    cancellationToken: cancellationToken
                );
            if (privateConversation is null) return GetMessagesResponseDTO.Empty;
            conversationId = privateConversation.Id;
        }
        else if (request.ConversationId is not null)
        {
            Conversation? conversation = await _context.Conversations
                .Include(x => (x as GroupConversation)!.Members)
                .SingleOrDefaultAsync(x => x.Id == request.ConversationId, cancellationToken);
            if (conversation is null) throw new ForbiddenAccessError();
            if (conversation is GroupConversation groupConversation &&
                groupConversation.Members.All(x => x.AccountId != _auth.Id))
                throw new ForbiddenAccessError();
            conversationId = conversation.Id;
        }
        else
            throw new NotImplementedException();

        // Base query
        IQueryable<MessageView> query = _context.MessagesView;

        // Filter by conversation
        query = query
            .Where(x => x.ConversationId == conversationId);

        // Filter messages that is was edited
        query = query
            .Where(x => x.AfterEditMessageId == null);

        // Include information about editing 
        query = query
            .Include(x => x.BeforeEditMessage);

        // Include owner and media
        query = query
            .Include(x => x.Owner)
            .Include(x => x.Attachments)
            .ThenInclude(x => (x as MessageFileAttachment)!.UploadedFile);

        // Include account like
        query = query
            .Include(x => x.Likes.Where(y => y.OwnerId == _auth.Id));

        // Pagination
        int offset = Math.Max(request.Offset ?? 0, 0);
        int limit = Math.Clamp(request.Limit ?? 20, 0, 100);
        query = query
            .OrderByDescending(x => x.TimeStamp)
            .Skip(offset).Take(limit);

        // Query data
        List<MessageView> messages = await query.ToListAsync(cancellationToken);

        return new GetMessagesResponseDTO(
            messages: messages.Select(x => new MessageBaseDTO(
                    id: x.Id,
                    owner: new AccountShortBaseDTO(
                        id: x.Owner.Id,
                        nickname: x.Owner.Nickname.ToString(),
                        isVerified: x.Owner.IsVerified,
                        avatarLink: x.Owner.Avatar is not null
                            ? _objectsStorage.GetLinkFor(x.Owner.Avatar.ObjectsStorageKey)
                            : null
                    ),
                    timeStamp: x.TimeStamp,
                    text: x.Text,
                    attachments: x.Attachments.Select(y => y switch
                        {
                            MessageFileAttachment attachmentFile => (AttachmentBaseDTO)
                                new FileAttachmentBaseDTO(
                                    id: attachmentFile.Id,
                                    order: attachmentFile.Order,
                                    file: new UploadedFileBaseDTO(
                                        id: attachmentFile.UploadedFile.Id,
                                        type: attachmentFile.UploadedFile.Type,
                                        fullName: attachmentFile.UploadedFile.FullName,
                                        link: _objectsStorage.GetLinkFor(attachmentFile.UploadedFile.ObjectsStorageKey)
                                    )
                                ),
                            MessageLinkAttachment attachmentLink =>
                                new LinkAttachmentBaseDTO(
                                    id: attachmentLink.Id,
                                    order: attachmentLink.Order,
                                    link: attachmentLink.Link
                                ),
                            _ => throw new NotImplementedException()
                        }
                    ).ToList(),
                    isEdited: x.BeforeEditMessage != null,
                    likesCount: x.LikesCount,
                    isLiked: x.Likes.Any()
                )
            ).ToList()
        );
    }
}

public class GetMessagesResponseDTO
{
    public static GetMessagesResponseDTO Empty => new(new List<MessageBaseDTO>());
    
    public List<MessageBaseDTO> Messages { get; }

    public GetMessagesResponseDTO(
        List<MessageBaseDTO> messages)
    {
        Messages = messages;
    }
}