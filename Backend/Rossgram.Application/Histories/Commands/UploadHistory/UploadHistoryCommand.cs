using System.Collections.Generic;
using MediatR;
using Rossgram.Application.Common.Behaviours;
using Rossgram.Domain;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Application.Histories.Commands.UploadHistory;

[Authorize]
public class UploadHistoryCommand
    : IRequest<UploadHistoryResponseDTO>
{
    public long UploadedFileId { get; set; }
}