using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.Data.Entities;

namespace UniHub.Data.Interfaces
{
    public interface ISubjectRepository : IBaseRepository<Subject> 
    {
        Task<IEnumerable<Subject>> GetSubjectsByFacultyWithTeachersAsync(int facultyId, int skip, int take);
        //TODO: make a research if it is Ok to name methods in such way
    }
}