using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.BLL.Services.Contract;

namespace UniHub.WebApi.BLL.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacultyService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<FacultyDto>> CreateFacultyAsync(FacultyAddRequest request)
           {
               if (!await _unitOfWork.UniversityRepository.IsExistById(request.UniversityId))
               {
                   return ServiceResult<FacultyDto>.Fail(EOperationResult.EntityNotFound, "University with this Id doesn't exist");
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
                   newFaculty.Avatar = Constants.DefaultImage;
               }

               _unitOfWork.FacultyRepository.Create(newFaculty);

               await _unitOfWork.CommitAsync();

               return ServiceResult<FacultyDto>.Ok(_mapper.Map<Faculty, FacultyDto>(newFaculty));
           }

        public async Task<ServiceResult<IEnumerable<FacultyDto>>> GetListOfFacultiesAsync(int universityId, int skip, int take)
        {
            IEnumerable<FacultyDto> result = _mapper.Map<IEnumerable<Faculty>, IEnumerable<FacultyDto>>(await _unitOfWork.FacultyRepository.GetFacultiesByUniversityAsync(universityId, skip, take));

            return ServiceResult<IEnumerable<FacultyDto>>.Ok(result);
        }
    }
}