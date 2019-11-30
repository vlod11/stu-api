using Microsoft.AspNetCore.Mvc;
using UniHub.Common.Enums;
using UniHub.Web.Extensions;

namespace UniHub.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        protected int UserId => User.GetUserId();

        protected ERoleType UserRole => User.GetRoleType();

        protected string UserEmail => User.GetEmail();
    }
}