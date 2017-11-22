using AutoMapper;
using MJC.CoreAPI.Template.WebAPI.Data.Entities;

namespace MJC.CoreAPI.Template.WebAPI.Data
{
    public class DummyApiMapperProfile : Profile
    {
        public DummyApiMapperProfile()
        {
            CreateMap<Dummy, DummyDto>().ReverseMap();
            CreateMap<Dummy, DummyDtoForCreation>().ReverseMap();
            CreateMap<Dummy, DummyDtoForUpdate>().ReverseMap();
        }
    }
}
