using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rossgram.Application.Posts.Commands.UploadPost;
using Rossgram.Application.Storages.Commands.Upload;
using Rossgram.WebApi.Services;

namespace Rossgram.WebApi.Controllers;

public class StorageController : MediatorBaseController
{
    [HttpPost("upload")]
    public Task<UploadResponseDTO> Upload(
        [FromServices] FormFileAdapter formFileAdapter,
        [FromForm] List<IFormFile> medias)
    {
        return Mediator.Send(new UploadCommand()
        {
            Files = medias.Select(formFileAdapter.ToFileData).ToList()
        });
    }
}