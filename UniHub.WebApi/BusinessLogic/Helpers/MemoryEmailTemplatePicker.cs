using System.ComponentModel;
using UniHub.WebApi.BusinessLogic.Helpers.Contract;
using UniHub.WebApi.Models.Enums;

namespace UniHub.WebApi.BusinessLogic.Helpers
{
    public class MemoryEmailTemplatePicker : IEmailTemplatePicker
    {
        public string GetTemplate(EEmailTemplateType emailTemplateType)
        {
            string template;

            switch (emailTemplateType)
            {
                case EEmailTemplateType.Registration:
                    {
                        template =
                            @"Hi, young reptiloid @Model.Username!<br>
                                <br>
                                You have been registered in <b>UniHub</b>. <br>
                                <br>
                                To confirm your email <a href =""@Model.Link"" >click here</a>
                                <br>
                                If you believe this is an error, you need to change your beliefs.<br>
                                <br>
                                <b>Best wishes,</b><br>
                                <b>Reptiloids team</b><br>";
                        break;
                    }

                default: throw new InvalidEnumArgumentException(emailTemplateType.ToString(), (int)emailTemplateType, emailTemplateType.GetType());
            }

            return template;
        }
    }
}