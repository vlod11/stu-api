using UniHub.WebApi.Model;

namespace UniHub.WebApi.Helpers.Contract
{
    public interface IEmailTemplatePicker
    {
         string GetTemplate(EEmailTemplateType emailTemplateType);
    }
}