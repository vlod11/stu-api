using System.Threading.Tasks;
using FluentEmail.Core.Models;
using UniHub.WebApi.Models.Models;

namespace UniHub.WebApi.BusinessLogic.Services.Shared.Contract
{
    public interface IEmailService
    {
        Task<ServiceResult<SendResponse>> SendUserValidationEmailAsync(string email, string username, string link);
    }
}