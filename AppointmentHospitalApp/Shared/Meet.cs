using System.ComponentModel.DataAnnotations;

namespace AppointmentHospitalApp.Shared
{
    public class Meet
    {
        [Key]
        public int Id { get; set; }
        public int? DoctorId { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedMeet { get; set; }
        public string? DoctorName { get; set; }
        public string? PoliclinicName { get; set; }
        public DateTime MeetDate { get; set; }
        [Required(ErrorMessage = "Lütfen randevu zamanızı seçiniz")]
        public TimeSpan MeetTime { get; set; }
        public bool Status { get; set; } = true;
    }
}
