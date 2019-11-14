using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniHub.WebApi.BusinessLogic.Services.Contract;
using UniHub.WebApi.Models.Enums;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Requests;
using UniHub.WebApi.Web.Helpers.Mappers;

namespace UniHub.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class SubjectsController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly ISubjectService _subjectService;

        public SubjectsController(
            IServiceResultMapper viewMapper,
            ISubjectService subjectService)
        {
            _viewMapper = viewMapper;
            _subjectService = subjectService;
        }
        
        [HttpPost]
        [Authorize(Roles = nameof(ERoleType.Admin))]
        public async Task<ActionResult<SubjectDto>> AddSubjectAsync([FromBody] SubjectAddRequest request)
            => _viewMapper.ServiceResultToContentResult(
                await _subjectService.CreateSubjectAsync(request));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjectsAsync(int facultyId, int skip = 0, int take = 10)
        => _viewMapper.ServiceResultToContentResult(
                await _subjectService.GetListOfSubjectsAsync(facultyId, skip, take));
    }
}