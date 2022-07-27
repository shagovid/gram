using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Auth.Commands.SignUp;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Domain;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors.Access;

namespace Rossgram.Application.Accounts.Commands.UpdateAvatar;

public class UpdateAvatarCommandHandler
    : IRequestHandler<UpdateAvatarCommand, UpdateAvatarResponseDTO>
{
    private readonly ICurrentAuth _auth;
    private readonly IAppDbContext _context;

    public UpdateAvatarCommandHandler(
        ICurrentAuth auth,
        IAppDbContext context)
    {
        _auth = auth;
        _context = context;
    }

    public async Task<UpdateAvatarResponseDTO> Handle(
        UpdateAvatarCommand request,
        CancellationToken cancellationToken)
    {
        UploadedFile? uploadedFile = await _context.UploadedFiles
            .FirstOrDefaultAsync(x => x.Id == request.FileId, cancellationToken);

        if (uploadedFile is null || uploadedFile.OwnerId != _auth.Id) 
            throw new ForbiddenAccessError();
        
        if (uploadedFile.Type != FileType.Photo) 
            throw new NotImplementedException();
        
        Account? account = await _context.Accounts
            .FirstOrDefaultAsync(x => x.Id == _auth.Id, cancellationToken);
        if (account is null) throw new NotImplementedException();
        
        account.AvatarId = request.FileId;
        
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync(cancellationToken);

        // Return result
        return new UpdateAvatarResponseDTO();
    }
}

public class UpdateAvatarResponseDTO
{
    
}