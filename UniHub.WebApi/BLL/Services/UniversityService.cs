using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.Common.Options;
using Microsoft.Extensions.Options;
using UniHub.WebApi.BLL.Constants;

namespace UniHub.WebApi.BLL.Services
{
    public class UniversityService : IUniversityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UrlsOptions _urlOptions;

        public UniversityService(
            IUnitOfWork unitOfWork,
            IOptions<UrlsOptions> urlOptions,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _urlOptions = urlOptions.Value;
        }

        public async Task<ServiceResult<UniversityDto>> CreateUniversityAsync(UniversityAddRequest request)
           {
               if (!await _unitOfWork.CityRepository.IsExistById(request.CityId))
               {
                   return ServiceResult<UniversityDto>.Fail(EOperationResult.EntityNotFound, "City with this Id doesn't exist");
               }

               var newUniversity = new University()
               {
                   FullTitle = request.FullTitle,
                   ShortTitle = request.ShortTitle,
                   Description = request.Description,
                   CityId = request.CityId,
                   Avatar = DefaultImagesConstants.DefaultImage
               };

               if (string.IsNullOrEmpty(newUniversity.Avatar))
               {
                   newUniversity.Avatar = DefaultImagesConstants.DefaultImage;
               }

               _unitOfWork.UniversityRepository.Add(newUniversity);

               await _unitOfWork.CommitAsync();

               return ServiceResult<UniversityDto>.Ok(_mapper.Map<University, UniversityDto>(newUniversity));
           }

        public async Task<ServiceResult<IEnumerable<UniversityDto>>> GetListOfUniversitiesAsync(int cityId, int skip, int take)
        {
            IEnumerable<UniversityDto> result;

            if (cityId != 0)
            {
                result = _mapper.Map<IEnumerable<University>, List<UniversityDto>>(
                            await _unitOfWork.UniversityRepository.GetUniversitiesByCityAsync(cityId, skip, take));
            }
            else
            {
                result = _mapper.Map<IEnumerable<University>, List<UniversityDto>>(
                            await _unitOfWork.UniversityRepository.GetAllUniversitiesAsync(skip, take));
            }

            return ServiceResult<IEnumerable<UniversityDto>>.Ok(result);
        }
    }
}