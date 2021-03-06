using System.Threading.Tasks;
using AutoMapper;
using UniHub.WebApi.BusinessLogic.Services.Contract;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.Models.Entities;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Models;
using UniHub.WebApi.Models.Requests;

namespace UniHub.WebApi.BusinessLogic.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ComplaintService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ServiceResult<ComplaintDto>> CreateComplaintAsync(ComplaintAddRequest request, int userId)
        {
            if (!await _unitOfWork.PostRepository.IsExistById(request.PostId))
            {
                return ServiceResult<ComplaintDto>.Fail(EOperationResult.EntityNotFound, "Post with this Id doesn't exist");
            }

            var complaint =
               await _unitOfWork.ComplaintRepository.GetSingleAsync(c => c.PostId == request.PostId && c.UserId == userId);

            if (complaint == null)
            {
                complaint = new Complaint()
                {
                    UserId = userId,
                    PostId = request.PostId
                };
                _unitOfWork.ComplaintRepository.Add(complaint);
                await _unitOfWork.CommitAsync();
            }

            return ServiceResult<ComplaintDto>.Ok(_mapper.Map<Complaint, ComplaintDto>(complaint));
        }
    }
}