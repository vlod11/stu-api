using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.Controllers
{
    [Route("[controller]")]
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
        public async Task<IActionResult> AddCountryAsync(string countryTitle)
            => _viewMapper.ServiceResultToContentResult(
                await _countryService.CreateCountryAsync(countryTitle));

        [HttpGet]
        public async Task<IActionResult> GetCountriesAsync()
        => _viewMapper.ServiceResultToContentResult(
                await _countryService.GetListOfCountriesAsync());
    }
}
