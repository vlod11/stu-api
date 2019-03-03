using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.Extensions;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.ModelLayer.Enums;
using UniHub.WebApi.ModelLayer.Requests;

namespace UniHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : UserBaseController
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
        public async Task<IActionResult> AddUniversityAsync([FromBody] UniversityAddRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            return _viewMapper.ServiceResultToContentResult(await _universityService.CreateUniversityAsync(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetUniversitiesAsync(int cityId = 0, int skip = 0, int take = 10)
        => _viewMapper.ServiceResultToContentResult(
                await _universityService.GetListOfUniversitiesAsync(cityId, skip, take));
    }
}
