using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.ModelLayer.ModelDto;

namespace UniHub.WebApi.BLL.Services
{
    public interface ISubjectService
    {
         Task<ServiceResult<IEnumerable<SubjectDto>>> GetListOfSubjectsAsync(int facultyId, int skip, int take);
         Task<ServiceResult<SubjectDto>> CreateSubjectAsync(SubjectAddRequest request);
    }
}