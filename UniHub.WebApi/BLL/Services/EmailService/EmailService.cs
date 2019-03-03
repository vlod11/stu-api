using System.ComponentModel;
using System.Threading.Tasks;
using System.Web;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Razor;
using FluentEmail.SendGrid;
using Microsoft.Extensions.Options;
using UniHub.WebApi.Helpers.Email;
using UniHub.WebApi.Model;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.Shared.Options;

namespace UniHub.WebApi.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly SendGridOptions _sendGridOptions;
        private readonly IEmailTemplatePicker _emailTemplatePicker;

        public EmailService(IOptions<SendGridOptions> sendGridOptions,
            IEmailTemplatePicker emailTemplatePicker)
        {
            _emailTemplatePicker = emailTemplatePicker;
            _sendGridOptions = sendGridOptions.Value;
            Email.DefaultSender = new SendGridSender(_sendGridOptions.ApiKey);
            Email.DefaultRenderer = new RazorRenderer();
        }

        public async Task<ServiceResult<SendResponse>> UserRegistrationAsync(string email, string username, string link)
        {
            string tempalate = _emailTemplatePicker.GetTemplate(EEmailTemplateType.Registration);

            var sendEmail = Email.From(_sendGridOptions.EmailFrom)
                .To(email)
                .Subject($"UniHub Email confirmation")
                .UsingTemplate(tempalate, new { Username = username, Link = link });

            SendResponse emailSendResponse = await sendEmail.SendAsync();

            if (emailSendResponse.Successful)
            {
                return ServiceResult<SendResponse>.Ok();
            }
            else
            {
                return ServiceResult<SendResponse>.Fail(EOperationResult.SendEmailError, string.Join(", ", emailSendResponse.ErrorMessages.ToArray()));
            }
        }
    }
}