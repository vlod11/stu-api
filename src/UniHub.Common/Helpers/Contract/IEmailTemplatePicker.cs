using UniHub.Common.Enums;

namespace UniHub.Common.Helpers.Contract
{
    public interface IEmailTemplatePicker
    {
         string GetTemplate(EEmailTemplateType emailTemplateType);
    }
}