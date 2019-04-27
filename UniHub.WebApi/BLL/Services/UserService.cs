using System.Threading.Tasks;
using AutoMapper;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.Helpers;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.ModelLayer.Requests.User;

namespace UniHub.WebApi.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<UserDto>> GetUserAsync(int userId)
        {
            UserDto result = _mapper.Map<User, UserDto>(await _unitOfWork.UserRepository.GetByIdAsync(userId));

            return ServiceResult<UserDto>.Ok(result);
        }

        public async Task<ServiceResult<UserDto>> UpdateUsersInfoAsync(int userId, UpdateUserInfoRequest request)
        {
            User user = await _unitOfWork.UserRepository.GetSingleAsync(up => up.Id == userId);

            if (user == null)
            {
                return ServiceResult<UserDto>.Fail(EOperationResult.EntityNotFound,
                        "User doesn't exist");
            }

            if (!string.IsNullOrEmpty(request.NewUsername))
            {
                if (await _unitOfWork.UserRepository.AnyAsync(u => u.Username == request.NewUsername))
                {
                    return ServiceResult<UserDto>.Fail(EOperationResult.ValidationError,
                        "User with this username already exist");
                }

                user.Username = request.NewUsername;
            }

            if (!string.IsNullOrEmpty(request.NewAvatar))
            {
                user.Avatar = request.NewAvatar;
            }

            await _unitOfWork.CommitAsync();

            return ServiceResult<UserDto>.Ok(_mapper.Map<User, UserDto>(user));
        }

        public async Task<ServiceResult<UserDto>> UpdatePasswordAsync(int userId, UpdatePasswordRequest request)
        {
            User user = await _unitOfWork.UserRepository.GetSingleAsync(up => up.Id == userId);

            if (user == null)
            {
                return ServiceResult<UserDto>.Fail(EOperationResult.EntityNotFound,
                        "User doesn't exist");
            }

            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                if (!Authenticate.Verify(request.CurrentPassword, user.PasswordHash))
                {
                    return ServiceResult<UserDto>.Fail(EOperationResult.ValidationError,
                        "Current password is incorrect");
                }

                user.PasswordHash = Authenticate.Hash(request.NewPassword);
            }

            await _unitOfWork.CommitAsync();

            return ServiceResult<UserDto>.Ok(_mapper.Map<User, UserDto>(user));
        }
    }
}