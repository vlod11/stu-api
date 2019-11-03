using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.ModelLayer.Enums;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.ModelLayer.ModelDto;
using System.Collections.Generic;
using UniHub.WebApi.Web.Helpers.Mappers;

namespace UniHub.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class ComplaintsController : BaseController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly IComplaintService _complaintService;

        public ComplaintsController(
            IServiceResultMapper viewMapper,
            IComplaintService complaintService)
        {
            _viewMapper = viewMapper;
            _complaintService = complaintService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ComplaintDto>> AddComplaintAsync(ComplaintAddRequest request)
            => _viewMapper.ServiceResultToContentResult(
                await _complaintService.CreateComplaintAsync(request, UserId));
    }
}