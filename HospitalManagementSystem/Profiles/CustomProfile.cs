using AutoMapper;
using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Profiles
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<Hospital, HospitalViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();  

        }
    }
}
