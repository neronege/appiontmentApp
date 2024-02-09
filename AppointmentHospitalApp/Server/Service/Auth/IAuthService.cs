using AppointmentHospitalApp.Shared;

namespace AppointmentHospitalApp.Server.Service.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string email, string password);

        Task<ServiceResponse<bool>> SelectAdmin(string email);
        int GetUserId();
        string GetUserEmail();
    }
}
