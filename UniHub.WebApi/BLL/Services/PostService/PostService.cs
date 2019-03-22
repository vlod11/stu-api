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

namespace UniHub.WebApi.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
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
                UserProfileId = userId,
                GroupId = request.GroupId,
                CreatedAt = DateTime.UtcNow
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

        public async Task<ServiceResult<IEnumerable<PostCardDto>>> GetListOfPostCardsAsync(int facultyId, int skip, int take)
        {
            IEnumerable<PostCardDto> postCards =
            (await _unitOfWork.PostRepository.GetAllPostsBySubjectAsync(facultyId, skip, take))
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
                                                    PostLocationType = p.PostLocationTypeId,
                                                    PostValueType = p.PostValueTypeId,
                                                    UserProfileId = p.UserProfileId,
                                                })
                                            });

            return ServiceResult<IEnumerable<PostCardDto>>.Ok(postCards);
        }

        public async Task<ServiceResult<IEnumerable<PostProfileDto>>> GetUsersPostsAsync(int userProfileId, int skip = 0, int take = 0)
        {
            IEnumerable<PostProfileDto> result =
            (await _unitOfWork.PostRepository.GetUsersPostAsync(userProfileId, skip, take))
                                            .Select(p => new PostProfileDto()
                                            {
                                                Id = p.Id,
                                                Title = p.Title,
                                                Description = p.Description,
                                                Semester = p.Semester,
                                                LastVisit = p.LastVisit,
                                                PostLocationType = p.PostLocationTypeId,
                                                PostValueType = p.PostValueTypeId,
                                                GroupId = p.GroupId,
                                                GroupTitle = p.Group.Title,
                                                UserProfileId = p.UserProfileId,
                                                SubjectId = p.SubjectId,
                                                SubjectTitle = p.Subject.Title,
                                                TeacherName = p.Subject.Teacher.LastName
                                            });

            return ServiceResult<IEnumerable<PostProfileDto>>.Ok(result);
        }

        public async Task<ServiceResult<PostLongDto>> GetPostFullInfoAsync(int postId)
        {
            Post post = await _unitOfWork.PostRepository.GetSingleAsync(p => p.Id == postId, 
                                                                            p => p.Files,
                                                                            p => p.Group,
                                                                            p => p.Answers);

            PostLongDto postDto = new PostLongDto()
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Semester = post.Semester,
                PostLocationType = post.PostLocationTypeId,
                PostValueType = post.PostValueTypeId,
                GroupId = post.GroupId,
                GroupTitle = post.Group.Title,
                UserProfileId = post.UserProfileId,
                ModifiedAt = post.ModifiedAt,
                GivenAt = post.GivenAt,
                Answers = _mapper.Map<IEnumerable<Answer>, IEnumerable<AnswerDto>>(post.Answers),
                Files = post.Files.Select(f => new FileDto()
                                                        {
                                                            Name = f.Name,
                                                            Url = f.Path,
                                                            FileType = f.FileTypeId
                                                        })
            };

            return ServiceResult<PostLongDto>.Ok(postDto);
        }
    }
}