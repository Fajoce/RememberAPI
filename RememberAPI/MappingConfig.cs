using AutoMapper;
using RememberAPI.DTOs;
using RememberAPI.Models;

namespace RememberAPI
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Payroll, PayrollDTO>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.lastName))
               .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
               .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.Days))
               .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName));
            // CreateMap<PayrollDTO, Payroll>();

            CreateMap<Payroll, InsertPaymentDTO>().ReverseMap(); 

            CreateMap<Payroll, UpdatePayrollDTO>().ReverseMap();
          
        }
    }
}
