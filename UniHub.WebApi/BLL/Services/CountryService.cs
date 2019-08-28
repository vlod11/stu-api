using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;

namespace UniHub.WebApi.BLL.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<CountryDto>> CreateCountryAsync(string countryTitle)
            {
                var newCountry = new Country()
                {
                    Title = countryTitle
                };

                _unitOfWork.CountryRepository.Add(newCountry);

                await _unitOfWork.CommitAsync();

                return ServiceResult<CountryDto>.Ok(_mapper.Map<Country, CountryDto>(newCountry));
            }

        public async Task<ServiceResult<IEnumerable<CountryDto>>> GetListOfCountriesAsync()
            {
                return ServiceResult<IEnumerable<CountryDto>>.Ok(_mapper.Map<IEnumerable<Country>, IEnumerable<CountryDto>>(await _unitOfWork.CountryRepository.GetAllCountriesAsync()));
            }
    }
}