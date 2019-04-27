using System.Threading.Tasks;
using FluentEmail.Core.Models;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Models;

namespace UniHub.WebApi.BLL.Services.Shared.Contract
{
    public interface IEmailService
    {
        Task<ServiceResult<SendResponse>> SendUserValidationEmailAsync(string email, string username, string link);
    }
}