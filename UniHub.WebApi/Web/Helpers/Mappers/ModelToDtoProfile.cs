using AutoMapper;
using UniHub.WebApi.Models.Entities;
using UniHub.WebApi.Models.Enums;
using UniHub.WebApi.Models.ModelDto;

namespace UniHub.WebApi.Web.Helpers.Mappers
{
    public class ModelToViewMapperProfile : Profile
    {
        public ModelToViewMapperProfile()
        {
            CreateMap<int, EFileType>().ConstructUsing(i => (EFileType)i);
            CreateMap<int, EPostLocationType>().ConstructUsing(i => (EPostLocationType)i);
            CreateMap<int, EPostValueType>().ConstructUsing(i => (EPostValueType)i);
            CreateMap<int, EPostVoteType>().ConstructUsing(i => (EPostVoteType)i);

            CreateMap<User, UserDto>();
            CreateMap<University, UniversityDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Faculty, FacultyDto>();
            CreateMap<Subject, SubjectDto>();
            CreateMap<City, CityDto>();
            CreateMap<Answer, AnswerDto>();

            CreateMap<File, FileDto>()
            .ForMember(f => f.FileType, opt => opt.MapFrom(src => src.FileTypeId))
            .ForMember(f => f.Url, opt => opt.MapFrom(src => src.Path));

            CreateMap<Post, PostLongDto>()
            .ForMember(p => p.PostLocationType, opt => opt.MapFrom(src => src.PostLocationTypeId))
            .ForMember(p => p.PostValueType, opt => opt.MapFrom(src => src.PostValueTypeId));

            CreateMap<Post, PostShortDto>()
            .ForMember(p => p.PostLocationType, opt => opt.MapFrom(src => src.PostLocationTypeId))
            .ForMember(p => p.PostValueType, opt => opt.MapFrom(src => src.PostValueTypeId));

            CreateMap<Post, PostProfileDto>()
            .ForMember(p => p.PostLocationType, opt => opt.MapFrom(src => src.PostLocationTypeId))
            .ForMember(p => p.PostValueType, opt => opt.MapFrom(src => src.PostValueTypeId));

            CreateMap<Complaint, ComplaintDto>();
            
            CreateMap<Teacher, TeacherDto>();
            
            CreateMap<Subject, SubjectDto>()
                .ForMember(s => s.TeacherLastName, opt => opt.MapFrom(src => src.Teacher.LastName));
        }
    }
}