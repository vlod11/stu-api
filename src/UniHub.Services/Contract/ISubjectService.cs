using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;
using UniHub.Model.Request;

namespace UniHub.Services.Contract
{
    public interface ISubjectService
    {
         Task<ServiceResult<IEnumerable<SubjectDto>>> GetListOfSubjectsAsync(int facultyId, int skip, int take);
         Task<ServiceResult<SubjectDto>> CreateSubjectAsync(SubjectAddRequest request);
    }
}