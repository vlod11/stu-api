using AutoMapper;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.ModelDto;

namespace UniHub.WebApi.Helpers.Mapper
{
    public class ModelToViewMapperProfile : Profile
    {
        public ModelToViewMapperProfile()
        {
             CreateMap<UsersProfile, UsersProfileDto>();
             CreateMap<University, UniversityDto>();
             CreateMap<Country, CountryDto>();
             CreateMap<City, CityDto>();
             CreateMap<Answer, AnswerDto>();
             CreateMap<File, FileDto>();
        }
    }
}