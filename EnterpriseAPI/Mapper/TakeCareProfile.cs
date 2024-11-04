using AutoMapper;
using EnterpriseAPI.Dto;
using EnterpriseAPI.Enum;
using EnterpriseAPI.Models;
using System.Globalization;

namespace EnterpriseAPI.Mapper
{
    public class TakeCareProfile : Profile
    {
        public TakeCareProfile()
        {
            CreateMap<Patient, PatientDto>().ForMember(x => x.DateOfBirth, y => y.MapFrom(z => z.DateOfBirth.ToString()));            
            CreateMap<PatientAddUpdateDto, Patient>().ForMember(x => x.DateOfBirth, y => y.MapFrom(z => DateTime.ParseExact(z.DateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture)));

            CreateMap<Appointment, AppointmentDto>().ForMember(x => x.StartTime, y => y.MapFrom(z => z.StartTime.ToString()))
                .ForMember(x => x.EndTime, y => y.MapFrom(z => z.EndTime.ToString()))
                .ForMember(x => x.Status, y => y.MapFrom(z => (AppointmentStatus)z.Status));            
            CreateMap<AppointmentAddUpdateDto, Appointment>().ForMember(x => x.StartTime, y => y.MapFrom(z => DateTime.ParseExact(z.StartTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)))
                .ForMember(x => x.EndTime, y => y.MapFrom(z => DateTime.ParseExact(z.StartTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).AddMinutes(15)))
                .ForMember(x => x.Status , y => y.MapFrom(z => (short)AppointmentStatus.Confirmed));
        }

    }
}
