using AutoMapper;

namespace AppointmentHospitalApp.Shared.Model
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Doctor, DoctorDTO>().ReverseMap();
        }
    }
}
