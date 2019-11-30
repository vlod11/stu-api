using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;

namespace UniHub.Services.Contract
{
    public interface IFileService
    {
        Task<ServiceResult<string>> UploadImageAsync(IFormFile imageFile);
        Task<ServiceResult<FileDto>> UploadFileAsync(IFormFile file);
    }
}