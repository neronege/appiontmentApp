using AppointmentHospitalApp.Shared;

namespace AppointmentHospitalApp.Server.Service.ForPoliclinic
{
    public interface IPoliclinicService
    {
        Task<ServiceResponse<Policlinic>> AddPoly(Policlinic policlinic);
        Task<ServiceResponse<Policlinic>> UpdatePoly(Policlinic policlinic);
        Task<ServiceResponse<List<Policlinic>>> GetAll();
        Task<ServiceResponse<int>> DeletePoly(int policlinicId);
        Task<ServiceResponse<Policlinic>> GetById(int policlinicId);
    }
}
