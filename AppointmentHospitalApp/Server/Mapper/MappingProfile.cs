using AppointmentHospitalApp.Shared;
using AppointmentHospitalApp.Shared.Model;
using AutoMapper;

namespace AppointmentHospitalApp.Server.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Doctor, DoctorDTO>().ReverseMap();
        }
    }
}
