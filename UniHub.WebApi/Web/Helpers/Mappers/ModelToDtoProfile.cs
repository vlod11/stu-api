using AutoMapper;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.ModelDto;

namespace UniHub.WebApi.Helpers.Mapper
{
    public class ModelToViewMapperProfile : Profile
    {
        public ModelToViewMapperProfile()
        {
             CreateMap<User, UserDto>();
             CreateMap<University, UniversityDto>();
             CreateMap<Country, CountryDto>();
             CreateMap<Faculty, FacultyDto>();
             CreateMap<Subject, SubjectDto>();
             CreateMap<City, CityDto>();
             CreateMap<Answer, AnswerDto>();
             CreateMap<File, FileDto>();
             CreateMap<Post, PostLongDto>();
             CreateMap<Post, PostShortDto>();
             CreateMap<Post, PostProfileDto>();
        }
    }
}