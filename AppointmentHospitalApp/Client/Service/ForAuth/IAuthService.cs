using AppointmentHospitalApp.Shared;
using AppointmentHospitalApp.Shared.Model;

namespace AppointmentHospitalApp.Client.Service.ForAuth
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);
        Task<ServiceResponse<string>> Login(UserLogin user);
        Task<bool> IsUserAuthenticated();
    }
}
