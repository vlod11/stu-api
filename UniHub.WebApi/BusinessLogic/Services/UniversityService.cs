using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using UniHub.WebApi.BusinessLogic.Constants;
using UniHub.WebApi.BusinessLogic.Services.Contract;
using UniHub.WebApi.Common.Options;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.Models.Entities;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Models;
using UniHub.WebApi.Models.Requests;

namespace UniHub.WebApi.BusinessLogic.Services
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