using UniHub.WebApi.Model;

namespace UniHub.WebApi.Helpers.Email
{
    public interface IEmailTemplatePicker
    {
         string GetTemplate(EEmailTemplateType emailTemplateType);
    }
}