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
            IEnumerable<PostCardDto> result =
            (await _unitOfWork.PostRepository.GetAllPostsBySubjectAsync(facultyId, skip, take))
                                            .Select(p => new PostCardDto
                                            {
                                                GroupId = p.GroupId,
                                                GroupName = p.GroupName,
                                                Posts = _mapper.Map<List<Post>, List<PostShortDto>>(p.Posts)
                                            });

            return ServiceResult<IEnumerable<PostCardDto>>.Ok(result);
        }

        public async Task<ServiceResult<IEnumerable<PostProfileDto>>> GetUsersPosts(int userProfileId, int skip = 0, int take = 0)
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
            PostLongDto result =
            _mapper.Map<Post, PostLongDto>(await _unitOfWork.PostRepository.GetFullPostInfoAsync(postId));

            return ServiceResult<PostLongDto>.Ok(result);
        }
    }
}