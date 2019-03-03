using System.Threading.Tasks;
using FluentEmail.Core.Models;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Models;

namespace UniHub.WebApi.BLL.Services
{
    public interface IEmailService
    {
        Task<ServiceResult<SendResponse>> UserRegistrationAsync(string email, string username, string link);
    }
}