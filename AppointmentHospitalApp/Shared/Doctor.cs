using System.ComponentModel.DataAnnotations;

namespace AppointmentHospitalApp.Shared
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        public int PoliclinicId { get; set; }
        public string? DoctorName { get; set; }
        public string? Description { get; set; }
        public string? DoctorTitle { get; set; }
    }
}
