using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.BLL.Helpers.Contract
{
    public interface IEmailTemplatePicker
    {
         string GetTemplate(EEmailTemplateType emailTemplateType);
    }
}