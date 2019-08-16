using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Enums;
using System.ComponentModel;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.BLL.Helpers.Contract;

namespace UniHub.WebApi.BLL.Services
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
            var newPost = new Post()
            {
                Title = request.Title,
                Description = request.Description,
                SubjectId = request.SubjectId,
                Semester = request.Semester,
                PostLocationTypeId = (int)request.PostLocationType,
                PostValueTypeId = (int)request.PostValueType,
                GivenAt = request.GivenAt,
                UserId = userId,
                GroupId = request.GroupId,
                CreatedAt = _dateHelper.GetDateTimeUtcNow()
            };

            _unitOfWork.PostRepository.Create(newPost);

            foreach (var fileInfo in request.FileInfoRequests)
            {
                var file = new File()
                {
                    Path = fileInfo.Url,
                    FileTypeId = (int)fileInfo.FileType,
                    Name = fileInfo.Name,
                    Post = newPost
                };

                _unitOfWork.FileRepository.Create(file);
            }

            await _unitOfWork.CommitAsync();

            return ServiceResult<PostLongDto>.Ok(_mapper.Map<Post, PostLongDto>(newPost));
        }

        public async Task<ServiceResult<IEnumerable<PostCardDto>>> GetListOfPostCardsAsync(int facultyId, int userId, int skip, int take)
        {
            IEnumerable<PostCardDto> postCards =
            (await _unitOfWork.PostRepository.GetAllPostsFullBySubjectAsync(facultyId, skip, take))
                                            .Select(pc => new PostCardDto
                                            {
                                                GroupId = pc.GroupId,
                                                GroupName = pc.GroupName,
                                                Posts = pc.Posts.Select(p => new PostShortDto()
                                                {
                                                    Id = p.Id,
                                                    Title = p.Title,
                                                    Description = p.Description,
                                                    Semester = p.Semester,
                                                    ModifiedAt = p.ModifiedAt,
                                                    PointsCount = p.VotesCount,
                                                    PostLocationType = p.PostLocationTypeId,
                                                    PostValueType = p.PostValueTypeId,
                                                    UserId = p.UserId,
                                                    UserVote = (EPostVoteType?)p.Votes.FirstOrDefault(v => v.UserId == userId)?.VoteTypeId ?? EPostVoteType.None,
                                                })
                                            });

            return ServiceResult<IEnumerable<PostCardDto>>.Ok(postCards);
        }

        public async Task<ServiceResult<PostLongDto>> VoteOnPostAsync(int postId, EPostVoteType postVoteType, int userId)
        {
            Post post = await _unitOfWork.PostRepository
                                            .GetSingleAsync(p => p.Id == postId,
                                                                 p => p.Files,
                                                                 p => p.Votes,
                                                                 p => p.Group,
                                                                 p => p.Answers);

            PostVote postVote = new PostVote();

            var existingVote = await _unitOfWork.PostVoteRepository
                                    .GetSingleAsync(p => p.UserId == userId && p.PostId == postId);

            if (existingVote?.VoteTypeId == (int)postVoteType)
            {
                return ServiceResult<PostLongDto>.Fail(EOperationResult.AlreadyExist, "You already voted on this post");
            }

            postVote = new PostVote()
            {
                Post = post,
                UserId = userId,
                VoteTypeId = (int)postVoteType
            };

            post.VotesCount = CountNewVotes(postVoteType, existingVote, post.VotesCount);

            await _unitOfWork.PostVoteRepository.AddAsync(postVote);

            if (existingVote != null)
            {
                _unitOfWork.PostVoteRepository.Delete(existingVote);
            }

            await _unitOfWork.CommitAsync();

            var postDto = _mapper.Map<Post, PostLongDto>(post);
            postDto.UserVoteType = postVoteType;

            return ServiceResult<PostLongDto>.Ok(postDto);
        }

        public async Task<ServiceResult<IEnumerable<PostProfileDto>>> GetUsersPostsAsync(int userId, int skip = 0, int take = 0)
        {
            IEnumerable<PostProfileDto> result =
            (await _unitOfWork.PostRepository.GetFullUsersPostAsync(userId, skip, take))
                                            .Select(p => new PostProfileDto()
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
                                                UserVote = (EPostVoteType?)p.Votes.FirstOrDefault(v => v.UserId == userId)?.VoteTypeId ?? EPostVoteType.None
                                            });

            return ServiceResult<IEnumerable<PostProfileDto>>.Ok(result);
        }

        public async Task<ServiceResult<PostLongDto>> GetPostFullInfoAsync(int postId, int userId)
        {
            Post post = await _unitOfWork.PostRepository.GetSingleAsync(p => p.Id == postId,
                                                                            p => p.Files,
                                                                            p => p.Votes,
                                                                            p => p.Group,
                                                                            p => p.Answers);

            var postVoteType = (EPostVoteType?)post.Votes.FirstOrDefault(v => v.UserId == userId)?.VoteTypeId ?? EPostVoteType.None;

            var postDto = _mapper.Map<Post, PostLongDto>(post);
            postDto.UserVoteType = postVoteType;

            return ServiceResult<PostLongDto>.Ok(postDto);
        }

        private int CountNewVotes(EPostVoteType postVoteType, PostVote existingVoteAction, int oldPostVotesCount)
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
            }

            return postVotesCount;
        }
    }
}