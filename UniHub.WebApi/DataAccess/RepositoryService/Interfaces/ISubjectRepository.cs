using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public interface ISubjectRepository : IRepositoryBase<Subject> 
    {
        Task<IEnumerable<Subject>> GetSubjectsByFacultyWithTeachersAsync(int facultyId, int skip, int take);
        //TODO: make a research if it is Ok to name methods in such way
    }
}