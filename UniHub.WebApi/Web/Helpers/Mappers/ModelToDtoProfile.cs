using AutoMapper;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Enums;
using UniHub.WebApi.ModelLayer.ModelDto;

namespace UniHub.WebApi.Helpers.Mapper
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
            .ForMember(f => f.FileType, opt => opt.MapFrom(src => src.FileTypeId));

            CreateMap<Post, PostLongDto>()
            .ForMember(p => p.PostLocationType, opt => opt.MapFrom(src => src.PostLocationTypeId))
            .ForMember(p => p.PostValueType, opt => opt.MapFrom(src => src.PostValueTypeId));

            CreateMap<Post, PostShortDto>()
            .ForMember(p => p.PostLocationType, opt => opt.MapFrom(src => src.PostLocationTypeId))
            .ForMember(p => p.PostValueType, opt => opt.MapFrom(src => src.PostValueTypeId));

            CreateMap<Post, PostProfileDto>()
            .ForMember(p => p.PostLocationType, opt => opt.MapFrom(src => src.PostLocationTypeId))
            .ForMember(p => p.PostValueType, opt => opt.MapFrom(src => src.PostValueTypeId));
        }
    }
}