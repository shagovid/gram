using System.Collections.Generic;
using MediatR;
using Rossgram.Application.Common.Behaviours;
using Rossgram.Domain;

namespace Rossgram.Application.Storages.Commands.Upload;

[Authorize]
public class UploadCommand
    : IRequest<UploadResponseDTO>
{
    public List<FileData> Files { get; set; } = null!;
}