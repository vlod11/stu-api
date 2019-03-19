using System.Threading.Tasks;
using AutoMapper;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.Helpers;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;

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

        public async Task<ServiceResult<UsersProfileDto>> UpdateUsersInfo(int usersProfileId, UpdateUserRequest request)
        {
            UsersProfile usersProfile = await _unitOfWork.UsersProfileRepository.GetSingleAsync(up => up.Id == usersProfileId, up => up.Credentional);

            if (!string.IsNullOrEmpty(request.NewUsername))
            {
                if (await _unitOfWork.UsersProfileRepository.AnyAsync(u => u.Username == request.NewUsername))
                {
                    return ServiceResult<UsersProfileDto>.Fail(EOperationResult.ValidationError,
                        "User with this username already exist");
                }

                usersProfile.Username = request.NewUsername;
            }

            usersProfile.Avatar = request.NewAvatar;

            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                if (!Authenticate.Verify(request.CurrentPassword, usersProfile.Credentional.PasswordHash))
                {
                    return ServiceResult<UsersProfileDto>.Fail(EOperationResult.ValidationError,
                        "Current password is incorrect");
                }

                usersProfile.Credentional.PasswordHash = Authenticate.Hash(request.NewPassword);
            }

            await _unitOfWork.CommitAsync();

            return ServiceResult<UsersProfileDto>.Ok(_mapper.Map<UsersProfile, UsersProfileDto>(usersProfile));
        }
    }
}