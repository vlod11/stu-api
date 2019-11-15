using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BusinessLogic.Services.Contract;
using UniHub.WebApi.Models.Enums;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Requests;
using UniHub.WebApi.Web.Extensions;
using UniHub.WebApi.Web.Helpers.Mappers;

namespace UniHub.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class UniversitiesController : BaseController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly IUniversityService _universityService;

        public UniversitiesController(
            IServiceResultMapper viewMapper,
            IUniversityService universityService)
        {
            _viewMapper = viewMapper;
            _universityService = universityService;
        }

        [HttpPost]
        [Authorize(Roles = nameof(ERoleType.Admin))]
        public async Task<ActionResult<UniversityDto>> AddUniversityAsync([FromBody] UniversityAddRequest request)
        {
            return _viewMapper.ServiceResultToContentResult(await _universityService.CreateUniversityAsync(request));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UniversityDto>>> GetUniversitiesAsync(int cityId = 0, int skip = 0, int take = 10)
        => _viewMapper.ServiceResultToContentResult(
                await _universityService.GetListOfUniversitiesAsync(cityId, skip, take));
    }
}
