using AppointmentHospitalApp.Shared;
using AppointmentHospitalApp.Shared.Model;

namespace AppointmentHospitalApp.Server.Service.ForDoctor
{
    public interface IDoctorService
    {
        Task<ServiceResponse<DoctorDTO>> AddDoctor(DoctorDTO doctor);
        Task<ServiceResponse<DoctorDTO>> UpdateDoctor(DoctorDTO doctor);
        Task<ServiceResponse<List<Doctor>>> GetAll();
        Task<ServiceResponse<int>> DeleteDoc(int doctorId);
        Task<ServiceResponse<List<Doctor>>> GetByDoctor(int policlinicId);
    }
}
