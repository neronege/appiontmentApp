using System.ComponentModel.DataAnnotations;

namespace AppointmentHospitalApp.Shared.Model
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Email alanı gereklidir"), EmailAddress]
        public string? Email { get; set; }
        [Required, StringLength(100, MinimumLength = 6)]
        public string? Password { get; set; }
        [Required, Compare("Password", ErrorMessage = "password do not match")]
        public string? ConfirmPassword { get; set; }
    }
}
