using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.ModelLayer.Enums;
using UniHub.WebApi.BLL.Services.Contract;
using System.Collections.Generic;
using UniHub.WebApi.ModelLayer.ModelDto;
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