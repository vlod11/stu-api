using System.Collections.Generic;
using System.Linq;
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
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UrlsOptions _urlOptions;

        public SubjectService(
            IUnitOfWork unitOfWork,
            IOptions<UrlsOptions> urlOptions,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _urlOptions = urlOptions.Value;
        }

        public async Task<ServiceResult<SubjectDto>> CreateSubjectAsync(SubjectAddRequest request)
           {
               if (!await _unitOfWork.FacultyRepository.IsExistById(request.FacultyId))
               {
                   return ServiceResult<SubjectDto>.Fail(EOperationResult.EntityNotFound,
                       "Faculty with this Id doesn't exist");
               }

               if (!await _unitOfWork.TeacherRepository.IsExistById(request.TeacherId))
               {
                   return ServiceResult<SubjectDto>.Fail(EOperationResult.EntityNotFound,
                       "Teacher with this Id doesn't exist");
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
                   newSubject.Avatar = _urlOptions.ServerUrl + DefaultImagesConstants.DefaultImage;
               }

               _unitOfWork.SubjectRepository.Add(newSubject);

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