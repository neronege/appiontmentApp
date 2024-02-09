using AppointmentHospitalApp.Shared;

namespace AppointmentHospitalApp.Server.Service.ForMeet
{
    public interface IMeetService
    {
        Task<ServiceResponse<Meet>> CreateMeet(Meet meet);
        Task<ServiceResponse<Meet>> UpdateMeet(Meet meet);
        Task<ServiceResponse<Meet>> Cancel(int id);
        Task<ServiceResponse<List<Meet>>> GetMeets();
        Task<ServiceResponse<bool>> ExistMeet();
    }
}
