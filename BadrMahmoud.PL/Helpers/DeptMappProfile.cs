using AutoMapper;
using BadrMahmoud.PL.Models;
using Demo.DAL.Models;

namespace BadrMahmoud.PL.Helpers
{
    public class DeptMappProfile : Profile
    {
        public DeptMappProfile() 
        {
            CreateMap<DeptViewModel, Department>().ReverseMap();
        }
    }
}
