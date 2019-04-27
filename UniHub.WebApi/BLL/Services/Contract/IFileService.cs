using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.ModelDto;

namespace UniHub.WebApi.BLL.Services
{
    public interface IFileService
    {
        Task<ServiceResult<string>> UploadImageAsync(IFormFile imageFile);
        Task<ServiceResult<FileDto>> UploadFileAsync(IFormFile file);
    }
}