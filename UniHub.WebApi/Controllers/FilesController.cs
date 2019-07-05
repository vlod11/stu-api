using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.Shared;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class FilesController : BaseController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly IFileService _fileService;
        
        public FilesController(
            IServiceResultMapper viewMapper,
            IFileService fileService)
        {
            _viewMapper = viewMapper;
            _fileService = fileService;
        }
        
        [HttpPost("upload-image")]
        [RequestSizeLimit(100_000_000)]
        [Authorize(Roles = nameof(ERoleType.Admin) + "," + nameof(ERoleType.Student))]
        public async Task<IActionResult> UploadImageAsync(IFormFile file)
            => _viewMapper.ServiceResultToContentResult
                (await _fileService.UploadImageAsync(file));

        [HttpPost("upload-file")]
        [Authorize(Roles = nameof(ERoleType.Admin) + "," + nameof(ERoleType.Student))]
        public async Task<IActionResult> UploadFileAsync(IFormFile file)
            => _viewMapper.ServiceResultToContentResult
                (await _fileService.UploadFileAsync(file));
    }
}