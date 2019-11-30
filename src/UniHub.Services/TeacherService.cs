using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
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
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UrlsOptions _urlOptions;
        private readonly IDateHelper _dateHelper;

        public TeacherService(
            IUnitOfWork unitOfWork,
            IOptions<UrlsOptions> urlOptions,
            IMapper mapper,
            IDateHelper dateHelper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _urlOptions = urlOptions.Value;
            _dateHelper = dateHelper;
        }

        public async Task<ServiceResult<TeacherDto>> CreateTeacherAsync(TeacherAddRequest request)
        {
            if (!await _unitOfWork.UniversityRepository.IsExistById(request.UniversityId))
            {
                return ServiceResult<TeacherDto>.Fail(EOperationResult.EntityNotFound, "University with this Id doesn't exist");
            }

            var teacher = new Teacher()
            {
                CreatedAtUtc = _dateHelper.GetDateTimeUtcNow(),
                ModifiedAtUtc = _dateHelper.GetDateTimeUtcNow(),
                LastName = request.LastName,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                UniversityId = request.UniversityId
            };

            _unitOfWork.TeacherRepository.Add(teacher);
            await _unitOfWork.CommitAsync();

            return ServiceResult<TeacherDto>.Ok(_mapper.Map<Teacher, TeacherDto>(teacher));
        }
    }
}