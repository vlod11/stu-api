using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.ModelLayer.Enums;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Requests;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            return _viewMapper.ServiceResultToContentResult(await _universityService.CreateUniversityAsync(request));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UniversityDto>>> GetUniversitiesAsync(int cityId = 0, int skip = 0, int take = 10)
        => _viewMapper.ServiceResultToContentResult(
                await _universityService.GetListOfUniversitiesAsync(cityId, skip, take));
    }
}
