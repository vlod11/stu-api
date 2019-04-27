using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.Model;
using UniHub.WebApi.Extensions;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected int UserId => User.GetUserId();

        protected ERoleType UserRole => User.GetRoleType();

        protected string UserEmail => User.GetEmail();
    }
}