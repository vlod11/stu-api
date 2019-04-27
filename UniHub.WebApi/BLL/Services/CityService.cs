using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.Model;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;

namespace UniHub.WebApi.BLL.Services
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
                   return ServiceResult<CityDto>.Fail(EOperationResult.AlreadyExist, "City with this title already exist in this country");
               }

               var newCity = new City()
               {
                   Title = request.Title,
                   CountryId = request.CountryId
               };

               _unitOfWork.CityRepository.Create(newCity);

               await _unitOfWork.CommitAsync();

               return ServiceResult<CityDto>.Ok(_mapper.Map<City, CityDto>(newCity));
           }

        public async Task<ServiceResult<IEnumerable<CityDto>>> GetListOfCitiesAsync(int countryId)
            {
                return ServiceResult<IEnumerable<CityDto>>.Ok(_mapper.Map<IEnumerable<City>, IEnumerable<CityDto>>(await _unitOfWork.CityRepository.GetCitiesByCountryAsync(countryId)));
            }
    }
}