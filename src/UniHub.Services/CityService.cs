using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using UniHub.Data;
using UniHub.Data.Entities;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;
using UniHub.Model.Request;
using UniHub.Services.Contract;

namespace UniHub.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<CityDto>> CreateCityAsync(CityAddRequest request)
           {
               if (!await _unitOfWork.CountryRepository.IsExistById(request.CountryId))
               {
                   return ServiceResult<CityDto>.Fail(EOperationResult.EntityNotFound, "Country with this Id doesn't exist");
               }

               if (await _unitOfWork.CityRepository.IsCityExistAsync(request.Title, request.CountryId))
               {
                   return ServiceResult<CityDto>.Fail(EOperationResult.AlreadyExist,
                       "City with this title already exist in this country");
               }

               var newCity = new City()
               {
                   Title = request.Title,
                   CountryId = request.CountryId
               };

               _unitOfWork.CityRepository.Add(newCity);

               await _unitOfWork.CommitAsync();

               return ServiceResult<CityDto>.Ok(_mapper.Map<City, CityDto>(newCity));
           }

        public async Task<ServiceResult<IEnumerable<CityDto>>> GetListOfCitiesAsync(int countryId)
            {
                return ServiceResult<IEnumerable<CityDto>>.Ok(
                    _mapper.Map<IEnumerable<City>, IEnumerable<CityDto>>(
                        await _unitOfWork.CityRepository.GetCitiesByCountryAsync(countryId)));
            }
    }
}