using AppointmentHospitalApp.Shared;
using AppointmentHospitalApp.Shared.Model;

namespace AppointmentHospitalApp.Client.Service.ForDoctor
{
    public interface IDoctorService
    {
        public List<Doctor> Doctors { get; set; }
        public string Message { get; set; }

        public event Action doctorChanged;
        public Task CreateDoctor(DoctorDTO doctor);
        public Task GetDoctor(int id);
        public Task GetDoctors();
        public Task DeleteDoctors(int id);

    }
}
