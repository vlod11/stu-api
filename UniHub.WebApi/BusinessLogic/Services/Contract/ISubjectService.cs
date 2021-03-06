using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Models;
using UniHub.WebApi.Models.Requests;

namespace UniHub.WebApi.BusinessLogic.Services.Contract
{
    public interface ISubjectService
    {
         Task<ServiceResult<IEnumerable<SubjectDto>>> GetListOfSubjectsAsync(int facultyId, int skip, int take);
         Task<ServiceResult<SubjectDto>> CreateSubjectAsync(SubjectAddRequest request);
    }
}