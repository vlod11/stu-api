using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : UserBaseController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly ICityService _cityService;

        public CitiesController(
            IServiceResultMapper viewMapper,
            ICityService cityService)
        {
            _viewMapper = viewMapper;
            _cityService = cityService;
        }

        [HttpPost]
        [Authorize(Roles = nameof(ERoleType.Admin))]
        public async Task<IActionResult> AddCityAsync([FromBody] CityAddRequest request)
            => _viewMapper.ServiceResultToContentResult(
                await _cityService.CreateCityAsync(request));

        [HttpGet]
        public async Task<IActionResult> GetCititesAsync(int countryId)
        => _viewMapper.ServiceResultToContentResult(
                await _cityService.GetListOfCitiesAsync(countryId));
    }
}
