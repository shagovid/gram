using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Services;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Entities.Messages;
using Rossgram.Domain.Errors.Access;

namespace Rossgram.Application.Conversations.Commands.SendMessage;

public class SendMessageCommandHandler
    : IRequestHandler<SendMessageCommand, SendMessageResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly ITimeService _time;
    private readonly IAppDbContext _context;

    public SendMessageCommandHandler(
        ICurrentAuth auth,
        ITimeService time,
        IAppDbContext context)
    {
        _auth = auth;
        _time = time;
        _context = context;
    }

    public async Task<SendMessageResponseDTO> Handle(
        SendMessageCommand request,
        CancellationToken cancellationToken)
    {
        // Request validation
        string messageText = request.Text.Trim();
        if (string.IsNullOrEmpty(messageText)) throw new NotImplementedException();

        // Query information about conversation
        long conversationId;
        Conversation? conversation;
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
            if (privateConversation is null)
            {
                conversationId = default;
                conversation = new PrivateConversation(
                    id: default,
                    olderAccountId: olderId,
                    olderAccount: default!,
                    newerAccountId: newerId,
                    newerAccount: default!,
                    messages: default!
                );
            }
            else
            {
                conversationId = privateConversation.Id;
                conversation = default;
            }
        }
        else if (request.ConversationId is not null)
        {
            GroupConversation? groupConversation = await _context.Conversations
                .OfType<GroupConversation>()
                .Include(x => x.Members)
                .SingleOrDefaultAsync(x => x.Id == request.ConversationId, cancellationToken);
            if (groupConversation is null) throw new ForbiddenAccessError();
            if (groupConversation.Members.All(x => x.AccountId != _auth.Id)) throw new ForbiddenAccessError();
            conversationId = groupConversation.Id;
            conversation = default;
        }
        else
            throw new NotImplementedException();

        List<MessageAttachment> attachments = new();
        int order = 1;
        foreach (SendMessageCommand.AttachmentDTO attachment in request.Attachments)
        {
            switch (attachment)
            {
                case SendMessageCommand.AttachmentFileDTO attachmentFile:
                    attachments.Add(new MessageFileAttachment(
                            id: default,
                            messageId: default,
                            message: default!,
                            order: order++,
                            uploadedFileId: attachmentFile.UploadedFileId,
                            uploadedFile: default!
                        )
                    );
                    break;
                case SendMessageCommand.AttachmentLinkDTO attachmentLink:
                    attachments.Add(new MessageLinkAttachment(
                            id: default,
                            messageId: default,
                            message: default!,
                            order: order++,
                            link: attachmentLink.Link
                        )
                    );
                    break;
                case SendMessageCommand.AttachmentPostDTO attachmentPost:
                    attachments.Add(new MessagePostAttachment(
                            id: default,
                            messageId: default,
                            message: default!,
                            order: order++,
                            postId: attachmentPost.PostId,
                            post: default!
                        )
                    );
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        Message message = new(
            id: default,
            ownerId: _auth.Id,
            owner: default!,
            conversationId: conversationId,
            conversation: conversation!,
            createdAt: _time.Now,
            text: messageText,
            attachments: attachments,
            afterEditMessageId: null,
            afterEditMessage: default!,
            beforeEditMessage: default!,
            likes: default!
        );

        await _context.Messages.AddAsync(message, cancellationToken);

        // Load info about uploaded files
        await _context.Entry(message)
            .Collection(x => x.Attachments)
            .Query()
            .Include(x => (x as MessageFileAttachment)!.UploadedFile)
            .LoadAsync(cancellationToken);

        // Check that all ids exist and that uploads owns by current user
        List<UploadedFile?> uploadedFiles = message.Attachments
            .OfType<MessageFileAttachment>()
            .Select(x => x.UploadedFile ?? null)
            .ToList();
        if (uploadedFiles.Any(x => x is null || x.OwnerId != _auth.Id)) throw new ForbiddenAccessError();

        await _context.SaveChangesAsync(cancellationToken);

        // Return result
        return new SendMessageResponseDTO(
            messageId: message.Id
        );
    }
}

public class SendMessageResponseDTO
{
    public long MessageId { get; set; }
    
    public SendMessageResponseDTO(
        long messageId)
    {
        MessageId = messageId;
    }
}