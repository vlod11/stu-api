using System.Threading.Tasks;
using FluentEmail.Core.Models;
using UniHub.Model.Models;

namespace UniHub.Services.Shared.Contract
{
    public interface IEmailService
    {
        Task<ServiceResult<SendResponse>> SendUserValidationEmailAsync(string email, string username, string link);
    }
}