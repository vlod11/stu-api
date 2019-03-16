using System.Threading.Tasks;
using AutoMapper;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;

namespace UniHub.WebApi.BLL.Services
{
    public class UsersProfileService : IUsersProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersProfileService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<UsersProfileDto>> GetUsersProfileAsync(int usersProfileId)
        {
            UsersProfileDto result = _mapper.Map<UsersProfile, UsersProfileDto>(await _unitOfWork.UsersProfileRepository.GetByIdAsync(usersProfileId));

            return ServiceResult<UsersProfileDto>.Ok(result);
        }
    }
}