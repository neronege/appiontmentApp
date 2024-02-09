using System.ComponentModel.DataAnnotations;

namespace AppointmentHospitalApp.Shared
{
    public class Policlinic
    {
        [Key]
        public int Id { get; set; }
        public string? PoliclinicName { get; set; }


    }
}
