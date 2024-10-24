using attendance_system_backend.DTOs;
using attendance_system_backend.Models;
using attendance_system_backend.Models.Enums;
using AutoMapper;
namespace attendance_system_backend.Data
{
    public class AutoMapperConfigProfile :  Profile
    {
        public AutoMapperConfigProfile()
        {
            //Mapping Employee
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender)) //Convert enum to int
                .ForMember(dest => dest.AddressDTO, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.UserInfoDTO, opt => opt.MapFrom(src => src.UserInfo))
                .ForMember(dest => dest.AttendanceRecordDTOs, opt => opt.MapFrom(src => src.AttendanceRecords));
                
            CreateMap<EmployeeDTO, Employee>()
                 .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (Gender)src.Gender)) //Convert int to enum
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.AddressDTO))
                 .ForMember(dest => dest.UserInfo, opt => opt.MapFrom(src => src.UserInfoDTO))
                 .ForMember(dest => dest.AttendanceRecords, opt => opt.MapFrom(src => src.AttendanceRecordDTOs));

            //Mapping Address
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();

            //Mapping Department
            CreateMap<Department, DepartmentDTO>();
            CreateMap<DepartmentDTO, Department>();

            //Mapping UserInfo
            CreateMap<UserInfo, UserInfoDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (int)src.Role));

            CreateMap<UserInfoDTO, UserInfo>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (UserRole)src.Role));

            //Mapping AttendanceRecord
            CreateMap<AttendanceRecord, AttendanceRecordDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status));

            CreateMap<AttendanceRecordDTO, AttendanceRecord>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (AttendanceStatus)src.Status));
        }
    }
}