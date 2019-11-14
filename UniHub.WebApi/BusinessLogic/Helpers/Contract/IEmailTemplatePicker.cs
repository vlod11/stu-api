using UniHub.WebApi.Models.Enums;

namespace UniHub.WebApi.BusinessLogic.Helpers.Contract
{
    public interface IEmailTemplatePicker
    {
         string GetTemplate(EEmailTemplateType emailTemplateType);
    }
}