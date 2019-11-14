using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Models;

namespace UniHub.WebApi.BusinessLogic.Services.Contract
{
    public interface IFileService
    {
        Task<ServiceResult<string>> UploadImageAsync(IFormFile imageFile);
        Task<ServiceResult<FileDto>> UploadFileAsync(IFormFile file);
    }
}