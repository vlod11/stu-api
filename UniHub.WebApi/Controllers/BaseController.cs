using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Enums;
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