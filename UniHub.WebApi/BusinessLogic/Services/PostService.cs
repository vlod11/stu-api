using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UniHub.WebApi.BusinessLogic.Constants;
using UniHub.WebApi.BusinessLogic.Helpers.Contract;
using UniHub.WebApi.BusinessLogic.Services.Contract;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.Models.Entities;
using UniHub.WebApi.Models.Enums;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Models;
using UniHub.WebApi.Models.Requests;

namespace UniHub.WebApi.BusinessLogic.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateHelper _dateHelper;

        public PostService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateHelper dateHelper)
        {
            _dateHelper = dateHelper;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<PostLongDto>> CreatePostAsync(PostAddRequest request, int userId)
        {
            var user = await _unitOfWork.UserRepository.GetSingleAsync(u => u.Id == userId);

            if (!user.IsValidated)
            {
                return ServiceResult<PostLongDto>.Fail(EOperationResult.ValidationError,
                    "Please, validate your email first");
            }

            var utcNow = _dateHelper.GetDateTimeUtcNow();

            var newPost = new Post(title: request.Title, description: request.Description, subjectId: request.SubjectId,
                semester: request.Semester, postLocationTypeId: (int)request.PostLocationType,
                postValueTypeId: (int)request.PostValueType, givenAt: request.GivenAt, userId: userId,
                groupId: request.GroupId, createdAtUtc: utcNow, modifiedAtUtc: utcNow, lastVisit: utcNow);

            _unitOfWork.PostRepository.Add(newPost);

            var newFiles = CreateFilesForPost(newPost, request.FileInfoRequests, utcNow);
            _unitOfWork.FileRepository.AddRange(newFiles);

            var userAvailablePost = new UserAvailablePost(post: newPost, userId: userId);

            _unitOfWork.UserAvailablePostRepository.Add(userAvailablePost);

            // TODO: move rest of method to separate func or class
            user.CurrencyCount = TradingConstants.NewPostUnicoinsBonus + user.CurrencyCount;

            var newPostVote = new PostVote(post: newPost, userId: userId, voteTypeId: (int)EPostVoteType.Upvote);
            _unitOfWork.PostVoteRepository.Add(newPostVote);

            newPost.VotesCount = CountNewVotes(EPostVoteType.Upvote, newPost.VotesCount);

            await _unitOfWork.CommitAsync();

            return ServiceResult<PostLongDto>.Ok(_mapper.Map<Post, PostLongDto>(newPost));
        }

        public async Task<ServiceResult<IEnumerable<PostShortDto>>> GetPostsAsync(int subjectId, int userId,
            string title = "", int groupId = 0, int? semester = 0, EPostValueType? valueType = null,
            EPostLocationType? locationType = null,
            DateTimeOffset? givenDateFrom = null, DateTimeOffset? givenDateTo = null, int skip = 0, int take = 0)
        {
            IEnumerable<PostShortDto> postCards =
                (await _unitOfWork.PostRepository.GetPostsBySubjectAsync(subjectId,
                    title, groupId, semester, valueType, locationType, givenDateFrom, givenDateTo, skip, take))
                .Select(p => new PostShortDto()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Semester = p.Semester,
                    ModifiedAt = p.ModifiedAtUtc,
                    GivenAt = p.GivenAt,
                    PointsCount = p.VotesCount,
                    PostLocationType = p.PostLocationTypeId,
                    PostValueType = p.PostValueTypeId,
                    UserId = p.UserId,
                    UserVote = (EPostVoteType?)p.Votes
                                   ?.FirstOrDefault(v => v.UserId == userId)
                                   ?.VoteTypeId ??
                               EPostVoteType.None,
                    IsUnlocked = p.UserAvailablePosts.Any(uap => uap.UserId == userId)
                });

            return ServiceResult<IEnumerable<PostShortDto>>.Ok(postCards);
        }

        public async Task<ServiceResult<IEnumerable<PostBySemesterGroupDto>>> GetListOfInitialPostsAsync(int subjectId,
            int userId,
            string title = "", int groupId = 0, int? semester = 0, EPostValueType? valueType = null,
            EPostLocationType? locationType = null,
            DateTimeOffset? givenDateFrom = null, DateTimeOffset? givenDateTo = null)
        {
            IEnumerable<PostBySemesterGroupDto> posts =
                (await _unitOfWork.PostRepository.GetInitialGroupedPostsBySubjectAsync(subjectId,
                    title, groupId, semester, valueType, locationType))
                .Select(pg => new PostBySemesterGroupDto
                {
                    GroupId = pg.GroupId,
                    GroupName = pg.GroupName,
                    Posts = pg.Posts.Select(p => new PostShortDto()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Semester = p.Semester,
                        GroupId = p.GroupId,
                        GroupName = pg.GroupName,
                        Description = p.Description,

                        PointsCount = p.VotesCount,
                        ModifiedAt = p.ModifiedAtUtc,
                        GivenAt = p.GivenAt,
                        PostLocationType = p.PostLocationTypeId,
                        PostValueType = p.PostLocationTypeId,
                        UserId = p.UserId,
                        UserVote = (EPostVoteType?)p.Votes
                                       .FirstOrDefault(v => v.UserId == userId)
                                       ?.VoteTypeId ?? EPostVoteType.None,
                        IsUnlocked = p.UserAvailablePosts.Any(uap => uap.UserId == userId)
                    }),
                    Semester = pg.Semester
                });

            return ServiceResult<IEnumerable<PostBySemesterGroupDto>>.Ok(posts);
        }

        public async Task<ServiceResult<PostLongDto>> VoteOnPostAsync(int postId, EPostVoteType postVoteType,
            int userId, ERoleType userRole)
        {
            var post = await _unitOfWork.PostRepository
                .GetSingleAsync(p => p.Id == postId,
                    p => p.User,
                    p => p.Files,
                    p => p.Votes,
                    p => p.Group,
                    p => p.Answers);

            var existingVote = await _unitOfWork.PostVoteRepository
                .GetSingleAsync(p => p.UserId == userId && p.PostId == postId);

            var validationResult = await PerformVoteRequestValidationAsync(post, userId, userRole, existingVote, postVoteType);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            var postVote = new PostVote()
            {
                Post = post,
                UserId = userId,
                VoteTypeId = (int)postVoteType
            };

            post.VotesCount = CountNewVotes(postVoteType, post.VotesCount, existingVote);

            await _unitOfWork.PostVoteRepository.AddAsync(postVote);

            if (existingVote != null)
            {
                _unitOfWork.PostVoteRepository.Delete(existingVote);
            }

            post.User.CurrencyCount = CountUserCurrencyCount(postVoteType, post.User.CurrencyCount);

            await _unitOfWork.CommitAsync();

            var postDto = _mapper.Map<Post, PostLongDto>(post);
            postDto.UserVoteType = postVoteType;

            return ServiceResult<PostLongDto>.Ok(postDto);
        }

        public async Task<ServiceResult<IEnumerable<PostProfileDto>>> GetUsersPostsAsync(int userId, int skip = 0,
            int take = 0)
        {
            var posts =
                (await _unitOfWork.PostRepository.GetFullUsersPostAsync(userId, skip, take));

            var postProfileDtos = posts.Select(p => new PostProfileDto()
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Semester = p.Semester,
                LastVisit = p.LastVisit,
                PostLocationType = (EPostLocationType)p.PostLocationTypeId,
                PostValueType = (EPostValueType)p.PostValueTypeId,
                VotesCount = p.VotesCount,
                GroupId = p.GroupId,
                GroupTitle = p.Group.Title,
                UserId = p.UserId,
                SubjectId = p.SubjectId,
                SubjectTitle = p.Subject.Title,
                TeacherName = p.Subject.Teacher.LastName,
                UserVote = (EPostVoteType?)p.Votes.FirstOrDefault(v => v.UserId == userId)?.VoteTypeId ??
                           EPostVoteType.None
            });

            return ServiceResult<IEnumerable<PostProfileDto>>.Ok(postProfileDtos);
        }

        public async Task<ServiceResult<PostLongDto>> GetPostFullInfoAsync(int postId, int userId, ERoleType roleType)
        {
            Post post = await _unitOfWork.PostRepository.GetSingleAsync(p => p.Id == postId,
                p => p.Files,
                p => p.Votes,
                p => p.Group,
                p => p.Answers);

            post.LastVisit = _dateHelper.GetDateTimeUtcNow();

            var postVoteType = (EPostVoteType?)post.Votes.FirstOrDefault(v => v.UserId == userId)?.VoteTypeId ??
                               EPostVoteType.None;

            if (roleType == ERoleType.Student)
            {
                bool isPostAvailable =
                    await _unitOfWork.UserAvailablePostRepository.AnyAsync(up =>
                        up.UserId == userId && up.PostId == postId);

                if (!isPostAvailable)
                {
                    return ServiceResult<PostLongDto>.Fail(EOperationResult.ValidationError,
                        "You need to unlock the post first!");
                }
            }

            var postDto = _mapper.Map<Post, PostLongDto>(post);
            postDto.UserVoteType = postVoteType;

            await _unitOfWork.CommitAsync();

            return ServiceResult<PostLongDto>.Ok(postDto);
        }

        // TODO: refactor to different classes with BaseClass or something
        private int CountNewVotes(EPostVoteType postVoteType, int oldPostVotesCount, PostVote existingVoteAction = null)
        {
            int postVotesCount = oldPostVotesCount;
            switch (postVoteType)
            {
                case EPostVoteType.Upvote:
                    if (existingVoteAction?.VoteTypeId == (int)EPostVoteType.Downvote)
                    {
                        postVotesCount = ++postVotesCount;
                    }

                    postVotesCount = ++postVotesCount;

                    break;
                case EPostVoteType.Downvote:
                    if (existingVoteAction?.VoteTypeId == (int)EPostVoteType.Upvote)
                    {
                        postVotesCount = --postVotesCount;
                    }

                    postVotesCount = --postVotesCount;

                    break;
                case EPostVoteType.None:
                    if (existingVoteAction?.VoteTypeId == (int)EPostVoteType.Downvote)
                    {
                        postVotesCount = ++postVotesCount;
                    }
                    else if (existingVoteAction?.VoteTypeId == (int)EPostVoteType.Upvote)
                    {
                        postVotesCount = --postVotesCount;
                    }

                    break;

                default:
                    throw new InvalidEnumArgumentException();
            }

            return postVotesCount;
        }

        private decimal CountUserCurrencyCount(EPostVoteType postVoteType, decimal postAuthorOldCurrencyCount,
            PostVote existingVoteAction = null)
        {
            decimal postAuthorCurrencyCount = postAuthorOldCurrencyCount;
            switch (postVoteType)
            {
                case EPostVoteType.Upvote:
                    if (existingVoteAction?.VoteTypeId == (int)EPostVoteType.Downvote)
                    {
                        postAuthorCurrencyCount = postAuthorCurrencyCount - TradingConstants.DownvoteUnicoinsFee;
                    }

                    postAuthorCurrencyCount = TradingConstants.UpvoteUnicoinsBonus + postAuthorCurrencyCount;

                    break;

                case EPostVoteType.Downvote:
                    if (existingVoteAction?.VoteTypeId == (int)EPostVoteType.Upvote)
                    {
                        postAuthorCurrencyCount = postAuthorCurrencyCount - TradingConstants.UpvoteUnicoinsBonus;
                    }

                    postAuthorCurrencyCount = TradingConstants.DownvoteUnicoinsFee + postAuthorCurrencyCount;

                    break;

                case EPostVoteType.None:
                    if (existingVoteAction?.VoteTypeId == (int)EPostVoteType.Downvote)
                    {
                        postAuthorCurrencyCount = postAuthorCurrencyCount - TradingConstants.DownvoteUnicoinsFee;
                    }
                    else if (existingVoteAction?.VoteTypeId == (int)EPostVoteType.Upvote)
                    {
                        postAuthorCurrencyCount = postAuthorCurrencyCount - TradingConstants.UpvoteUnicoinsBonus;
                    }

                    break;

                default:
                    throw new InvalidEnumArgumentException();
            }

            return postAuthorOldCurrencyCount;
        }

        private IEnumerable<File> CreateFilesForPost(Post post, IEnumerable<FileInfoRequest> fileInfoRequests,
            DateTime utcNow)
        {
            return fileInfoRequests.Select(fileInfo => new File(path: fileInfo.Url,
                    fileTypeId: (int)fileInfo.FileType,
                    name: fileInfo.Name,
                    post: post,
                    createdAtUtc: utcNow))
                .ToList();
        }

        private async Task<ServiceResult<PostLongDto>> PerformVoteRequestValidationAsync(Post post, int userId,
            ERoleType userRole, PostVote existingVote, EPostVoteType newPostVoteType)
        {
            ServiceResult<PostLongDto> validationResult = null;
            if (post == null)
            {
                validationResult = ServiceResult<PostLongDto>.Fail(EOperationResult.EntityNotFound, "Post not found");
            }

            var isUserUnlockedPost =
                await _unitOfWork.UserAvailablePostRepository.AnyAsync(up =>
                    up.UserId == userId && up.PostId == post.Id);

            if (!isUserUnlockedPost && userRole != ERoleType.Admin)
            {
                validationResult = ServiceResult<PostLongDto>.Fail(EOperationResult.ValidationError,
                    "You need to unlock the post before voting");
            }
            
            if (existingVote?.VoteTypeId == (int)newPostVoteType)
            {
                validationResult = ServiceResult<PostLongDto>.Fail(EOperationResult.AlreadyExist, "You already voted on this post");
            }

            return validationResult ?? (validationResult = ServiceResult<PostLongDto>.Ok());
        }
    }
}