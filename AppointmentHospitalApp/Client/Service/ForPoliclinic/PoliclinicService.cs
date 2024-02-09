using AppointmentHospitalApp.Shared;
using System.Net.Http.Json;

namespace AppointmentHospitalApp.Client.Service.ForPoliclinic
{
    public class PoliclinicService : IPoliclinicService
    {
        private readonly HttpClient _httpClient;

        public PoliclinicService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Policlinic> Policlinics { get; set; } = new List<Policlinic>();

        public event Action OnChange;
        public async Task CreatePoliclinics(Policlinic policlinic)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Policlinic/add", policlinic);
            OnChange?.Invoke();
        }

        public async Task DeletePoliclinics(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Policlinic/delete/{id}");
            OnChange?.Invoke();
        }

        public async Task<List<Policlinic>> GetPoliclinics()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Policlinic>>>("api/Policlinic/getAll");
            if (result != null)
            {
                Policlinics = result.Data;
            }
            OnChange?.Invoke();
            return Policlinics;
        }
    }
}
