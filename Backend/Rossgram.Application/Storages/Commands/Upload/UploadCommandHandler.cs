using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rossgram.Application.Common.BaseDTOs;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Services;
using Rossgram.Application.Posts.Commands.UploadPost;
using Rossgram.Domain;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Application.Storages.Commands.Upload;

public class UploadCommandHandler
    : IRequestHandler<UploadCommand, UploadResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly ObjectsStorageService _objectsStorage;
    private readonly IAppDbContext _context;

    public UploadCommandHandler(
        ICurrentAuth auth,
        ObjectsStorageService objectsStorage,
        IAppDbContext context)
    {
        _auth = auth;
        _objectsStorage = objectsStorage;
        _context = context;
    }

    public async Task<UploadResponseDTO> Handle(
        UploadCommand request,
        CancellationToken cancellationToken)
    {
        // Upload to the object storage
        List<UploadedFile> uploadedFiles = new List<UploadedFile>();
        foreach (FileData file in request.Files)
        {
            string objectStorageKey = await _objectsStorage.Upload(file.FullName, file.Stream);

            uploadedFiles.Add(new UploadedFile(
                    id: default,
                    ownerId: _auth.Id,
                    owner: default!,
                    fullName: file.FullName,
                    type: file.Type,
                    objectsStorageKey: objectStorageKey
                )
            );
        }

        // Store data
        await _context.UploadedFiles.AddRangeAsync(uploadedFiles, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        // Return result
        return new UploadResponseDTO(
            files: uploadedFiles.Select(x => new UploadedFileBaseDTO(
                        id: x.Id,
                        fullName: x.FullName,
                        type: x.Type,
                        link: _objectsStorage.GetLinkFor(x.ObjectsStorageKey)
                    )
                )
                .ToList()
        );
    }
}

public class UploadResponseDTO
{
    public List<UploadedFileBaseDTO> Files { get; }

    public UploadResponseDTO(
        List<UploadedFileBaseDTO> files)
    {
        Files = files;
    }

    public class FileDTO
    {

    }
}