using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BusinessLogic.Services.Contract;
using UniHub.WebApi.Models.Enums;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Web.Helpers.Mappers;

namespace UniHub.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class CountriesController : BaseController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly ICountryService _countryService;

        public CountriesController(
            IServiceResultMapper viewMapper,
            ICountryService countryService)
        {
            _viewMapper = viewMapper;
            _countryService = countryService;
        }

        [HttpPost]
        [Authorize(Roles = nameof(ERoleType.Admin))]
        public async Task<ActionResult<CountryDto>> AddCountryAsync(string countryTitle)
            => _viewMapper.ServiceResultToContentResult(
                await _countryService.CreateCountryAsync(countryTitle));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountriesAsync()
        => _viewMapper.ServiceResultToContentResult(
                await _countryService.GetListOfCountriesAsync());
    }
}
