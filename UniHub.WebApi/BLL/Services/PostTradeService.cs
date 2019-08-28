using System.Threading.Tasks;
using AutoMapper;
using UniHub.WebApi.BLL.Constants;
using UniHub.WebApi.BLL.Helpers.Contract;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;

namespace UniHub.WebApi.BLL.Services
{
    public class PostTradeService : IPostTradeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateHelper _dateHelper;

        public PostTradeService(
                    IUnitOfWork unitOfWork,
                    IMapper mapper,
                    IDateHelper dateHelper)
        {
            _dateHelper = dateHelper;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<PostLongDto>> UnlockPostAsync(int postId, int userId)
        {
            Post post = await _unitOfWork.PostRepository
                                            .GetSingleAsync(p => p.Id == postId,
                                                                 p => p.User,
                                                                 p => p.Files,
                                                                 p => p.Votes,
                                                                 p => p.Group,
                                                                 p => p.Answers);

            if (post == null)
            {
                return ServiceResult<PostLongDto>.Fail(EOperationResult.EntityNotFound, "Post not found");
            }

            var userBuyer = await _unitOfWork.UserRepository.GetSingleAsync(u => u.Id == userId);

            if (userBuyer == null)
            {
                return ServiceResult<PostLongDto>.Fail(EOperationResult.EntityNotFound, "User with this Id was not found");
            }

            var userAvailablePost = new UserAvailablePost()
            {
                Post = post,
                UserId = userId
            };

            userBuyer.CurrencyCount = TradingConstants.UnlockMaterialUnicoinsFee + userBuyer.CurrencyCount;

            if (userBuyer.CurrencyCount < 0)
            {
                return ServiceResult<PostLongDto>.Fail(EOperationResult.NotEnoughUniCoins,
                         "You do not have enough UniCoins to unlock this material. Please, upload some materials.");
            }

            await _unitOfWork.UserAvailablePostRepository.AddAsync(userAvailablePost);

            await _unitOfWork.CommitAsync();

            var postDto = _mapper.Map<Post, PostLongDto>(post);

            return ServiceResult<PostLongDto>.Ok(postDto);
        }
    }
}