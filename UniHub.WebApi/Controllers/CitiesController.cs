using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.ModelLayer.Enums;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.ModelLayer.ModelDto;
using System.Collections.Generic;
using UniHub.WebApi.Web.Helpers.Mappers;

namespace UniHub.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class CitiesController : BaseController
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
        public async Task<ActionResult<CityDto>> AddCityAsync([FromBody] CityAddRequest request)
            => _viewMapper.ServiceResultToContentResult(
                await _cityService.CreateCityAsync(request));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCititesAsync(int countryId)
        => _viewMapper.ServiceResultToContentResult(
                await _cityService.GetListOfCitiesAsync(countryId));
    }
}
