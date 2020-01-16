using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using UniHub.Common.Constants;
using UniHub.Common.Helpers.Contract;
using UniHub.Common.Options;
using UniHub.Data;
using UniHub.Data.Entities;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;
using UniHub.Model.Request;
using UniHub.Services.Contract;

namespace UniHub.Services
{
    public class UniversityService : IUniversityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateHelper _dateHelper;

        public UniversityService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateHelper dateHelper)
        {
            _dateHelper = dateHelper;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<UniversityDto>> CreateUniversityAsync(UniversityAddRequest request)
        {
            ServiceResult<UniversityDto> result;
            var isExist = await _unitOfWork.CityRepository.IsExistById(request.CityId);
            if (!isExist)
            {
                result = ServiceResult<UniversityDto>.Fail(
                    EOperationResult.EntityNotFound,
                    "City with this Id doesn't exist");
            }
            else
            {
                var utcNow = _dateHelper.GetDateTimeUtcNow();

                var newUniversity = new University()
                {
                    FullTitle = request.FullTitle,
                    ShortTitle = request.ShortTitle,
                    Description = request.Description,
                    CityId = request.CityId,
                    CreatedAtUtc = utcNow,
                    ModifiedAtUtc = utcNow,
                    Avatar = SetRequestOrDefaultAvatar(request.Avatar)
                };

                _unitOfWork.UniversityRepository.Add(newUniversity);

                await _unitOfWork.CommitAsync();

                result = ServiceResult<UniversityDto>.Ok(_mapper.Map<University, UniversityDto>(newUniversity));
            }

            return result;
        }

        public async Task<ServiceResult<IEnumerable<UniversityDto>>> GetListOfUniversitiesAsync(int cityId, int skip, int take)
        {
            ServiceResult<IEnumerable<UniversityDto>> result;

            if (cityId != 0)
            {
                var universities = _mapper.Map<IEnumerable<University>, List<UniversityDto>>(
                    await _unitOfWork.UniversityRepository.GetUniversitiesByCityAsync(cityId, skip, take));
                result = ServiceResult<IEnumerable<UniversityDto>>.Ok(universities);
            }
            else
            {
                result = ServiceResult<IEnumerable<UniversityDto>>.Fail(
                    EOperationResult.EntityNotFound,
                    "City with this Id doesn't exist");
            }

            return result;
        }

        private static string SetRequestOrDefaultAvatar(string avatar)
        {
            return string.IsNullOrEmpty(avatar) ? DefaultImagesConstants.DefaultImage : avatar;
        }
    }
}