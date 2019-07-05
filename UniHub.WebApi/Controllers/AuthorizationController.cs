using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.Shared.Options;
using UniHub.WebApi.BLL.Services.Contract;
using Microsoft.Extensions.Logging;

namespace UniHub.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthorizationController : BaseController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly UrlsOptions _urlsOption;
        ILogger<AuthorizationController> _logger;

        public AuthorizationController(
            IServiceResultMapper viewMapper,
            IAuthorizationService authorizationService,
            IOptions<UrlsOptions> urlsOptions,
            ILogger<AuthorizationController> logger)
        {
            _logger = logger;
            _viewMapper = viewMapper;
            _authorizationService = authorizationService;
            _urlsOption = urlsOptions.Value;
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <returns>string - token (or exeption)</returns>
        /// <param name="loginRequest">Login request.</param>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest loginRequest)
        {
            return _viewMapper.ServiceResultToContentResult(
                await _authorizationService.LoginAsync(loginRequest));
        }

        /// <summary>
        /// Register user.
        /// </summary>
        /// <returns>nothing</returns>
        /// <param name="registerRequest">Register request.</param>
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest registerRequest)
            => _viewMapper.ServiceResultToContentResult(
                await _authorizationService.RegisterStudentAsync(registerRequest));

        [HttpPost("Refresh-Token")]
        public async Task<IActionResult> UpdateTokenAsync([FromBody] RefreshTokenRequest refreshToken)
            => _viewMapper.ServiceResultToContentResult(
                await _authorizationService.UpdateTokenAsync(refreshToken));

        [Route("email-Confirmation/{emailToken}")]
        [HttpGet]
        public async Task<ActionResult> VerifyEmailAsync(string emailToken)
        {
            var result = _viewMapper.ServiceResultToContentResult(
                await _authorizationService.ConfirmEmailAsync(emailToken));
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