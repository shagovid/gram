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

namespace Rossgram.Application.Posts.Commands.UploadPost;

public class UploadPostCommandHandler
    : IRequestHandler<UploadPostCommand, UploadPostResponseDTO>
{
    private readonly IPostConfig _config;
    private readonly ICurrentAuth _auth;
    private readonly ITimeService _time;
    private readonly CommentController _commentController;
    private readonly IAppDbContext _context;

    public UploadPostCommandHandler(
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

    public async Task<UploadPostResponseDTO> Handle(
        UploadPostCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Attachments.Count < _config.MinAttachmentsCount)
            throw new NotImplementedException();
            
        if (request.Attachments.Count > _config.MaxAttachmentsCount)
            throw new NotImplementedException();

        List<PostAttachment> attachments = new ();
        int order = 1;
        foreach (UploadPostCommand.AttachmentDTO attachment in request.Attachments)
        {
            switch (attachment)
            {
                case UploadPostCommand.AttachmentFileDTO attachmentFile:
                    attachments.Add(new PostAttachmentFile(
                        id: default,
                        postId: default,
                        post: default!,
                        order: order++,
                        uploadedFileId: attachmentFile.UploadedFileId,
                        uploadedFile: default!)
                    );
                    break;
                default: throw new NotImplementedException();
            }
        }

        Post post = new Post(
            id: default,
            ownerId: _auth.Id,
            owner: default!,
            timeStamp: _time.Now,
            attachments: attachments,
            comment: _commentController.Validate(request.Comment),
            likes: default!,
            comments: default!,
            messagesAttachments: default!
        );
        
        // Store data
        await _context.Posts.AddAsync(post, cancellationToken);

        // Check that all ids exist and that uploads owns by current user
        List<long> uploadedFilesIds = attachments
            .OfType<PostAttachmentFile>()
            .Select(x => x.UploadedFileId)
            .ToList();
        if (uploadedFilesIds.Count > 0)
        {
            List<UploadedFile> uploadedFiles = await _context.UploadedFiles
                .Where(x => uploadedFilesIds.Contains(x.Id))
                .ToListAsync(cancellationToken);
            
            if (uploadedFiles.Any(x => x.OwnerId != _auth.Id))
                throw new ForbiddenAccessError();
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        
        // Return result
        return new UploadPostResponseDTO()
        {
            Id = post.Id
        };
    }
}

public class UploadPostResponseDTO
{
    public long Id { get; set; }
}