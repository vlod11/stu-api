using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> AddSubjectAsync([FromBody] SubjectAddRequest request)
            => _viewMapper.ServiceResultToContentResult(
                await _subjectService.CreateSubjectAsync(request));

        [HttpGet]
        public async Task<IActionResult> GetSubjectsAsync(int facultyId, int skip = 0, int take = 10)
        => _viewMapper.ServiceResultToContentResult(
                await _subjectService.GetListOfSubjectsAsync(facultyId,  skip, take));
    }
}