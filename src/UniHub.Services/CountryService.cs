using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using UniHub.Data;
using UniHub.Data.Entities;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;
using UniHub.Services.Contract;

namespace UniHub.Services
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
                return ServiceResult<IEnumerable<CountryDto>>.Ok(
                    _mapper.Map<IEnumerable<Country>, IEnumerable<CountryDto>>(
                        await _unitOfWork.CountryRepository.GetAllCountriesAsync()));
            }
    }
}