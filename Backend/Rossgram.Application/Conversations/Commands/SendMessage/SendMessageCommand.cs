using System.Collections.Generic;
using MediatR;
using Rossgram.Application.Common.Behaviours;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Application.Conversations.Commands.SendMessage;

[Authorize]
public class SendMessageCommand
    : IRequest<SendMessageResponseDTO>
{
    public long? RecipientId { get; set; }
    public long? ConversationId { get; set; }
    public string Text { get; set; } = null!;
    
    public List<AttachmentDTO> Attachments { get; set; } = null!;

    public abstract class AttachmentDTO
    {
        public AttachmentType AttachmentType { get; set; } = null!;
    }
    public class AttachmentFileDTO : AttachmentDTO
    {
        public FileType FileType { get; set; } = null!;
        public long UploadedFileId { get; set; }
    }
    public class AttachmentLinkDTO : AttachmentDTO
    {
        public string Link { get; set; } = null!;
    }
    public class AttachmentPostDTO : AttachmentDTO
    {
        public long PostId { get; set; }
    }
}