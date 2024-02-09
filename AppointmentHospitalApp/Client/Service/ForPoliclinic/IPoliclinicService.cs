using AppointmentHospitalApp.Shared;

namespace AppointmentHospitalApp.Client.Service.ForPoliclinic
{
    public interface IPoliclinicService
    {
        public event Action OnChange;
        List<Policlinic> Policlinics { get; set; }
        Task<List<Policlinic>> GetPoliclinics();
        Task CreatePoliclinics(Policlinic policlinic);
        Task DeletePoliclinics(int id);


    }
}
