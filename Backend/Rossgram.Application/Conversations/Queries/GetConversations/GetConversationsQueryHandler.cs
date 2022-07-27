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
using Rossgram.Domain.Entities.Messages;
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors.Access;
using Rossgram.Domain.Views;

namespace Rossgram.Application.Conversations.Queries.GetConversations;

public class GetConversationsQueryHandler
    : IRequestHandler<GetConversationsQuery, GetConversationsResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly ObjectsStorageService _objectsStorage;
    private readonly IAppDbContext _context;

    public GetConversationsQueryHandler(
        ICurrentAuth auth,
        ObjectsStorageService objectsStorage,
        IAppDbContext context)
    {
        _auth = auth;
        _objectsStorage = objectsStorage;
        _context = context;
    }

    public async Task<GetConversationsResponseDTO> Handle(
        GetConversationsQuery request,
        CancellationToken cancellationToken)
    {
        // Base query
        IQueryable<Message> query = _context.Messages;

        // Filter by account
        query = query
            .Where(x => (x.Conversation as PrivateConversation)!.OlderAccountId == _auth.Id ||
                        (x.Conversation as PrivateConversation)!.NewerAccountId == _auth.Id ||
                        (x.Conversation as GroupConversation)!.Members.Any(y => y.AccountId == _auth.Id)
            );

        // Include accounts
        query = query
            .Include(x => x.Conversation)
            .ThenInclude(x => (x as PrivateConversation)!.OlderAccount)
            .Include(x => x.Conversation)
            .ThenInclude(x => (x as PrivateConversation)!.NewerAccount);

        // Include owner and attachments
        query = query
            .Include(x => x.Owner)
            .Include(x => x.Attachments);

        // Pagination and take only last per conversation
        int offset = Math.Max(request.Offset ?? 0, 0);
        int limit = Math.Clamp(request.Limit ?? 20, 0, 100);
        query = query
            .GroupBy(x => x.ConversationId)
            .Select(x => x.OrderByDescending(y => y.CreatedAt).First())
            .Skip(offset).Take(limit);

        // Query data
        List<Message> lastMessages = await query.ToListAsync(cancellationToken);

        return new GetConversationsResponseDTO(
            conversations: lastMessages.Select(x => x.Conversation switch
                {
                    PrivateConversation privateConversation => (ConversationBaseDTO)
                        new PrivateConversationBaseDTO(
                            id: privateConversation.Id,
                            recipient: privateConversation.OlderAccountId == _auth.Id
                                ? new AccountShortBaseDTO(
                                    id: privateConversation.NewerAccount.Id,
                                    nickname: privateConversation.NewerAccount.Nickname,
                                    isVerified: privateConversation.NewerAccount.IsVerified,
                                    avatarLink: privateConversation.NewerAccount.Avatar is not null
                                        ? _objectsStorage.GetLinkFor(privateConversation.NewerAccount.Avatar
                                            .ObjectsStorageKey
                                        )
                                        : null
                                )
                                : new AccountShortBaseDTO(
                                    id: privateConversation.OlderAccount.Id,
                                    nickname: privateConversation.OlderAccount.Nickname,
                                    isVerified: privateConversation.OlderAccount.IsVerified,
                                    avatarLink: privateConversation.OlderAccount.Avatar is not null
                                        ? _objectsStorage.GetLinkFor(privateConversation.OlderAccount.Avatar
                                            .ObjectsStorageKey
                                        )
                                        : null
                                ),
                            lastMessage: new MessageBaseDTO(
                                id: x.Id,
                                owner: new AccountShortBaseDTO(
                                    id: x.Owner.Id,
                                    nickname: x.Owner.Nickname,
                                    isVerified: x.Owner.IsVerified,
                                    avatarLink: x.Owner.Avatar is not null
                                        ? _objectsStorage.GetLinkFor(x.Owner.Avatar.ObjectsStorageKey)
                                        : null
                                ),
                                timeStamp: x.CreatedAt,
                                text: x.Text,
                                attachments: x.Attachments.Select(y => y switch
                                    {
                                        MessageFileAttachment attachmentFile => (AttachmentBaseDTO)
                                            new FileAttachmentBaseDTO(
                                                id: attachmentFile.Id,
                                                order: attachmentFile.Order,
                                                file: null
                                            ),
                                        MessageLinkAttachment attachmentLink =>
                                            new LinkAttachmentBaseDTO(
                                                id: attachmentLink.Id,
                                                order: attachmentLink.Order,
                                                link: attachmentLink.Link
                                            ),
                                        MessagePostAttachment attachmentPost =>
                                            new PostAttachmentBaseDTO(
                                                id: attachmentPost.Id,
                                                order: attachmentPost.Order,
                                                post: null
                                            ),
                                        MessageHistoryAttachment attachmentHistory =>
                                            new HistoryAttachmentBaseDTO(
                                                id: attachmentHistory.Id,
                                                order: attachmentHistory.Order,
                                                history: null
                                            ),
                                        _ => throw new NotImplementedException()
                                    }
                                ).ToList()
                            )
                        ),
                    GroupConversation groupConversation =>
                        new GroupConversationBaseDTO(
                            id: groupConversation.Id,
                            name: groupConversation.Name,
                            lastMessage: new MessageBaseDTO(
                                id: x.Id,
                                owner: new AccountShortBaseDTO(
                                    id: x.Owner.Id,
                                    nickname: x.Owner.Nickname,
                                    isVerified: x.Owner.IsVerified,
                                    avatarLink: x.Owner.Avatar is not null
                                        ? _objectsStorage.GetLinkFor(x.Owner.Avatar.ObjectsStorageKey)
                                        : null
                                ),
                                timeStamp: x.CreatedAt,
                                text: x.Text,
                                attachments: x.Attachments.Select(y => y switch
                                    {
                                        MessageFileAttachment attachmentFile => (AttachmentBaseDTO)
                                            new FileAttachmentBaseDTO(
                                                id: attachmentFile.Id,
                                                order: attachmentFile.Order,
                                                file: null
                                            ),
                                        MessageLinkAttachment attachmentLink =>
                                            new LinkAttachmentBaseDTO(
                                                id: attachmentLink.Id,
                                                order: attachmentLink.Order,
                                                link: attachmentLink.Link
                                            ),
                                        MessagePostAttachment attachmentPost =>
                                            new PostAttachmentBaseDTO(
                                                id: attachmentPost.Id,
                                                order: attachmentPost.Order,
                                                post: null
                                            ),
                                        MessageHistoryAttachment attachmentHistory =>
                                            new HistoryAttachmentBaseDTO(
                                                id: attachmentHistory.Id,
                                                order: attachmentHistory.Order,
                                                history: null
                                            ),
                                        _ => throw new NotImplementedException()
                                    }
                                ).ToList(),
                                isEdited: null,
                                likesCount: null,
                                isLiked: null
                            )
                        ),
                    _ => throw new NotImplementedException()
                }
            ).ToList()
        );

    }
}

public class GetConversationsResponseDTO
{
    public List<ConversationBaseDTO> Conversations { get; }

    public GetConversationsResponseDTO(
        List<ConversationBaseDTO> conversations)
    {
        Conversations = conversations;
    }
}