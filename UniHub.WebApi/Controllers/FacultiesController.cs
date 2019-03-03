using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.Model;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : UserBaseController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly IFacultyService _facultyService;

        public FacultiesController(
            IServiceResultMapper viewMapper,
            IFacultyService facultyService)
        {
            _viewMapper = viewMapper;
            _facultyService = facultyService;
        }

        [HttpPost]
        [Authorize(Roles = nameof(ERoleType.Admin))]
        public async Task<IActionResult> AddFacultyAsync([FromBody] FacultyAddRequest request)
            => _viewMapper.ServiceResultToContentResult(
                await _facultyService.CreateFacultyAsync(request));

        [HttpGet]
        public async Task<IActionResult> GetFacultiesAsync(int universityId, int skip = 0, int take = 10)
        => _viewMapper.ServiceResultToContentResult(
                await _facultyService.GetListOfFacultiesAsync(universityId,  skip, take));
    }
}
