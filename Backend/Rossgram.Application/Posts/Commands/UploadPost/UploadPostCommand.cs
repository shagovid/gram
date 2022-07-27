using System.Collections.Generic;
using MediatR;
using Rossgram.Application.Common.Behaviours;
using Rossgram.Domain;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Application.Posts.Commands.UploadPost;

[Authorize]
public class UploadPostCommand
    : IRequest<UploadPostResponseDTO>
{
    public string Comment { get; set; } = null!;
    public List<AttachmentDTO> Attachments { get; set; } = null!;

    public abstract class AttachmentDTO
    {
        public int Order { get; set; }
        public AttachmentType AttachmentType { get; set; } = null!;
    }
    public class AttachmentFileDTO : AttachmentDTO
    {
        public long UploadedFileId { get; set; }
    }
}