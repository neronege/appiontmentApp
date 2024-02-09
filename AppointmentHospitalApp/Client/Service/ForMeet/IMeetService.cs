using AppointmentHospitalApp.Shared;

namespace AppointmentHospitalApp.Client.Service.ForMeet
{
    public interface IMeetService
    {
        List<Meet> Meets { get; set; }
        Task<ServiceResponse<List<Meet>>> GetMeet();
        Task<ServiceResponse<Meet>> CreateMeet(Meet meet);
        Task<ServiceResponse<Meet>> CancelMeet(int id);
    }
}