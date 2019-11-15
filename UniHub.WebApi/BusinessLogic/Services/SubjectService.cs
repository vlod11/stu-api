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
            var validationResult = await PerformSubjectValidation(request.FacultyId, request.TeacherId);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
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

        public async Task<ServiceResult<IEnumerable<SubjectDto>>> GetListOfSubjectsAsync(int facultyId, int skip,
            int take)
        {
            var subjects =
                await _unitOfWork.SubjectRepository.GetSubjectsByFacultyWithTeachersAsync(facultyId, skip, take);
            var subjectDtos = _mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectDto>>(subjects);

            return ServiceResult<IEnumerable<SubjectDto>>.Ok(subjectDtos);
        }

        private async Task<ServiceResult<SubjectDto>> PerformSubjectValidation(int teacherId, int facultyId)
        {
            if (!await _unitOfWork.FacultyRepository.IsExistById(facultyId))
            {
                return ServiceResult<SubjectDto>.Fail(EOperationResult.EntityNotFound,
                    "Faculty with this Id doesn't exist");
            }

            if (!await _unitOfWork.TeacherRepository.IsExistById(teacherId))
            {
                return ServiceResult<SubjectDto>.Fail(EOperationResult.EntityNotFound,
                    "Teacher with this Id doesn't exist");
            }
            
            return ServiceResult<SubjectDto>.Ok();
        }
    }
}