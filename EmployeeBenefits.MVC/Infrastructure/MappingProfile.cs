using AutoMapper;
using EmployeeBenefits.MVC.Models;
using EmployeeBenefits.MVC.ViewModels;

namespace EmployeeBenefits.MVC.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DependantViewModel, Dependant>().ReverseMap();   
        }
    }
}
