using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;

namespace UniHub.WebApi.BLL.Services.Contract
{
    public interface IFileService
    {
        Task<ServiceResult<string>> UploadImageAsync(IFormFile imageFile);
        Task<ServiceResult<FileDto>> UploadFileAsync(IFormFile file);
    }
}