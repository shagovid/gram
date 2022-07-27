using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Auth.Commands.SignUp;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Application.Common.Services;
using Rossgram.Domain;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors.Access;

namespace Rossgram.Application.Histories.Commands.UploadHistory;

public class UploadHistoryCommandHandler
    : IRequestHandler<UploadHistoryCommand, UploadHistoryResponseDTO>
{
    private readonly IPostConfig _config;
    private readonly ICurrentAuth _auth;
    private readonly ITimeService _time;
    private readonly CommentController _commentController;
    private readonly IAppDbContext _context;

    public UploadHistoryCommandHandler(
        IPostConfig config,
        ICurrentAuth auth,
        ITimeService time,
        CommentController commentController,
        IAppDbContext context)
    {
        _config = config;
        _auth = auth;
        _time = time;
        _commentController = commentController;
        _context = context;
    }

    public async Task<UploadHistoryResponseDTO> Handle(
        UploadHistoryCommand request,
        CancellationToken cancellationToken)
    {
        History history = new History(
            id: default,
            ownerId: _auth.Id,
            owner: default!,
            createdAt: _time.Now,
            uploadedFileId: request.UploadedFileId,
            uploadedFile: default!,
            messagesAttachments: default!
        );
        
        // Store data
        await _context.Histories.AddAsync(history, cancellationToken);

        // Check that all ids exist and that uploads owns by current user
        UploadedFile? uploadedFile = await _context.UploadedFiles.FindAsync(request.UploadedFileId);

        if (uploadedFile is null) throw new ForbiddenAccessError();
        if (uploadedFile.OwnerId != _auth.Id) throw new ForbiddenAccessError();
        if (uploadedFile.Type != FileType.Photo || uploadedFile.Type != FileType.Video)
            throw new NotImplementedException();
        
        await _context.SaveChangesAsync(cancellationToken);
        
        // Return result
        return new UploadHistoryResponseDTO()
        {
            Id = history.Id
        };
    }
}

public class UploadHistoryResponseDTO
{
    public long Id { get; set; }
}