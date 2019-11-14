using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UniHub.WebApi.Common.Options;
using Microsoft.Extensions.Logging;
using UniHub.WebApi.BusinessLogic.Services.Contract;
using UniHub.WebApi.Common.Token;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Requests.Authorization;
using UniHub.WebApi.Web.Helpers.Mappers;

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
        public async Task<ActionResult<AuthDto>> LoginAsync([FromBody] LoginUserRequest loginRequest)
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
        public async Task<ActionResult<object>> RegisterAsync([FromBody] RegisterUserRequest registerRequest)
            => _viewMapper.ServiceResultToContentResult(
                await _authorizationService.RegisterStudentAsync(registerRequest));

        [HttpPost("Refresh-Token")]
        public async Task<ActionResult<TokenModel>> UpdateTokenAsync([FromBody] RefreshTokenRequest refreshToken)
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
                return RedirectPermanent(_urlsOption.ClientUrl);
            }
            else
            {
                return result;
            }
        }
    }
}