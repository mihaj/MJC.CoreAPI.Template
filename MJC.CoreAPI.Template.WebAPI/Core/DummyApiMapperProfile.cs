using AutoMapper;
using MJC.CoreAPI.Template.WebAPI.Core.Dtos.Dummy;
using MJC.CoreAPI.Template.WebAPI.Core.Entities;

namespace MJC.CoreAPI.Template.WebAPI.Core
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
