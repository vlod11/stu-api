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