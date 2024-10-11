using AutoMapper;
using Company.DAL.Models;
using Company.PL.ViewModels;

namespace Company.PL.Helpers
{
    public class MappSetting : Profile
    {
        public MappSetting() 
        {
            CreateMap<EmployeeViewModel,Employee>().ReverseMap();
            CreateMap<DepartmentViewModel,Department>().ReverseMap();
        }
    }
}
