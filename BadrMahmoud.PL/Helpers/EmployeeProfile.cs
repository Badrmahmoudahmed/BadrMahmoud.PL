using AutoMapper;
using BadrMahmoud.PL.Models;
using Demo.DAL.Models;

namespace BadrMahmoud.PL.Helpers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmpViewModel, Employee>().ReverseMap();
        }
    }
}
