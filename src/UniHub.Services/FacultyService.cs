using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using UniHub.Common.Constants;
using UniHub.Common.Options;
using UniHub.Data;
using UniHub.Data.Entities;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;
using UniHub.Model.Request;
using UniHub.Services.Contract;

namespace UniHub.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UrlsOptions _urlOptions;

        public FacultyService(
            IUnitOfWork unitOfWork,
            IOptions<UrlsOptions> urlOptions,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _urlOptions = urlOptions.Value;
        }

        public async Task<ServiceResult<FacultyDto>> CreateFacultyAsync(FacultyAddRequest request)
           {
               if (!await _unitOfWork.UniversityRepository.IsExistById(request.UniversityId))
               {
                   return ServiceResult<FacultyDto>.Fail(EOperationResult.EntityNotFound,
                       "University with this Id doesn't exist");
               }

               var newFaculty = new Faculty()
               {
                   FullTitle = request.FullTitle,
                   ShortTitle = request.ShortTitle,
                   Description = request.Description,
                   UniversityId = request.UniversityId,
                   Avatar = request.Avatar
               };

               if (string.IsNullOrEmpty(newFaculty.Avatar))
               {
                   newFaculty.Avatar = _urlOptions.ServerUrl + DefaultImagesConstants.DefaultImage;
               }

               _unitOfWork.FacultyRepository.Add(newFaculty);

               await _unitOfWork.CommitAsync();

               return ServiceResult<FacultyDto>.Ok(_mapper.Map<Faculty, FacultyDto>(newFaculty));
           }

        public async Task<ServiceResult<IEnumerable<FacultyDto>>> GetListOfFacultiesAsync(int universityId, int skip, int take)
        {
            IEnumerable<FacultyDto> result = _mapper.Map<IEnumerable<Faculty>, IEnumerable<FacultyDto>>(
                await _unitOfWork.FacultyRepository.GetFacultiesByUniversityAsync(universityId, skip, take));

            return ServiceResult<IEnumerable<FacultyDto>>.Ok(result);
        }
    }
}