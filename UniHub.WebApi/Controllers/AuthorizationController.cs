using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.Shared.Options;

namespace UniHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : UserBaseController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly UrlsOptions _urlsOption;

        public AuthorizationController(
            IServiceResultMapper viewMapper,
            IAuthorizationService authorizationService,
            IOptions<UrlsOptions> urlsOptions)
        {
            _viewMapper = viewMapper;
            _authorizationService = authorizationService;
            _urlsOption = urlsOptions.Value;
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <returns>string - token (or exeption)</returns>
        /// <param name="loginRequest">Login request.</param>
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest loginRequest)
            => _viewMapper.ServiceResultToContentResult(
                await _authorizationService.LoginAsync(loginRequest));

        /// <summary>
        /// Register user.
        /// </summary>
        /// <returns>nothing</returns>
        /// <param name="registerRequest">Register request.</param>
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest registerRequest)
            => _viewMapper.ServiceResultToContentResult(
                await _authorizationService.RegisterStudentAsync(registerRequest));

        [Route("emailConfirmation/{username}/{emailToken}")]
        [HttpGet]
        public async Task<ActionResult> VerifyEmailAsync(string username, string emailToken)
        {
            var result = _viewMapper.ServiceResultToContentResult(
                await _authorizationService.ConfirmPasswordAsync(username, emailToken));
            if (result.StatusCode == 200)
                {
                    return RedirectPermanent(_urlsOption.AppUrl);
                }
            else
            {
                return result;
            }
        } 
    }
}