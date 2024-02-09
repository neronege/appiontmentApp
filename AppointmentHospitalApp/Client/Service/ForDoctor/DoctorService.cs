using AppointmentHospitalApp.Shared;
using AppointmentHospitalApp.Shared.Model;
using System.Net.Http.Json;

namespace AppointmentHospitalApp.Client.Service.ForDoctor
{
    public class DoctorService : IDoctorService
    {
        private readonly HttpClient _httpClient;
        public DoctorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public string Message { get; set; }

        public event Action doctorChanged;

        public async Task CreateDoctor(DoctorDTO doctor)
        {
            var result = await _httpClient.PostAsJsonAsync("api/doctor/addDoctor", doctor);

            doctorChanged?.Invoke();
        }

        public async Task DeleteDoctors(int id)
        {
            var result = await _httpClient.DeleteAsync("api/doctor/delete/{id}");
            doctorChanged?.Invoke();
        }

        public async Task GetDoctor(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Doctor>>>($"api/doctor/{id}");
            if (result != null && result.Data != null)
            {
                Doctors = result.Data;
            }
            doctorChanged?.Invoke();
        }

        public async Task GetDoctors()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Doctor>>>("api/doctor/getAll");
            if (result != null && result.Data != null)
            {
                Doctors = result.Data;
            }

        }

    }
}
