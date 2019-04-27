using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.BLL.Services.Contract;

namespace UniHub.WebApi.BLL.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubjectService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<SubjectDto>> CreateSubjectAsync(SubjectAddRequest request)
           {
               if (!await _unitOfWork.FacultyRepository.IsExistById(request.FacultyId))
               {
                   return ServiceResult<SubjectDto>.Fail(EOperationResult.EntityNotFound, "Faculty with this Id doesn't exist");
               }

               if (!await _unitOfWork.TeacherRepository.IsExistById(request.TeacherId))
               {
                   return ServiceResult<SubjectDto>.Fail(EOperationResult.EntityNotFound, "Teacher with this Id doesn't exist");
               }

               var newSubject = new Subject()
               {
                   Title = request.Title,
                   TeacherId = request.TeacherId,
                   Description = request.Description,
                   FacultyId = request.FacultyId,
                   Avatar = request.Avatar
               };

               if (string.IsNullOrEmpty(newSubject.Avatar))
               {
                   newSubject.Avatar = Constants.DefaultImage;
               }

               _unitOfWork.SubjectRepository.Create(newSubject);

               await _unitOfWork.CommitAsync();

               return ServiceResult<SubjectDto>.Ok(_mapper.Map<Subject, SubjectDto>(newSubject));
           }

        public async Task<ServiceResult<IEnumerable<SubjectDto>>> GetListOfSubjectsAsync(int facultyId, int skip, int take)
        {
            IEnumerable<SubjectDto> result =
             (await _unitOfWork.SubjectRepository.GetSubjectsByFacultyWithTeachersAsync(facultyId, skip, take))
                                                .Select(s => new SubjectDto()
                                                {
                                                    Id = s.Id,
                                                    Title = s.Title,
                                                    Description = s.Description,
                                                    Avatar = s.Avatar,
                                                    TeacherLastName = s.Teacher.LastName
                                                });

            return ServiceResult<IEnumerable<SubjectDto>>.Ok(result);
        }
    }
}