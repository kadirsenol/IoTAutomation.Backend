using System.ComponentModel.DataAnnotations;

namespace IoTAutomation.EntityLayer.Concrete.VMs.ProfileVm
{
    public class SaveChangeVm
    {
        [MinLength(3, ErrorMessage = "Minimum 3 karakterden oluşan ad girmelisiniz")]
        public string? Ad { get; set; }

        [MinLength(3, ErrorMessage = "Minimum 3 karakterden oluşan soyad girmelisiniz")]
        public string? Soyad { get; set; }

        [MinLength(4, ErrorMessage = "Minimum 4 karakterden oluşan new password girmelisiniz")]
        public string? NewPassword { get; set; }

        [MinLength(4, ErrorMessage = "Minimum 4 karakterden oluşan confirm password girmelisiniz")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Parola Alanı Boş Geçilemez")]
        [MinLength(4, ErrorMessage = "Minimum 4 karakterden oluşan password girmelisiniz")]
        public string Password { get; set; }
    }
}
