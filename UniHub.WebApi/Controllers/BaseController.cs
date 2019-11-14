using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.Models.Enums;
using UniHub.WebApi.Web.Extensions;

namespace UniHub.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected int UserId => User.GetUserId();

        protected ERoleType UserRole => User.GetRoleType();

        protected string UserEmail => User.GetEmail();
    }
}