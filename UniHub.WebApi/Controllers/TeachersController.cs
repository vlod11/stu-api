using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class TeachersController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly ITeacherService _teacherService;
        
        public TeachersController(ITeacherService teacherService, IServiceResultMapper viewMapper)
        {
            _teacherService = teacherService;
            _viewMapper = viewMapper;
        }
        
        [HttpPost]
        [Authorize(Roles = nameof(ERoleType.Admin))]
        public async Task<ActionResult<SubjectDto>> AddTeacherAsync([FromBody] TeacherAddRequest request)
            => _viewMapper.ServiceResultToContentResult(
                await _teacherService.CreateTeacherAsync(request));
    }
}