using System.ComponentModel.DataAnnotations;

namespace IoTAutomation.EntityLayer.Concrete.VMs.UserVM
{
    public class UserRegisterVm
    {
        [Required(ErrorMessage = "İsim Alanı Boş Geçilemez")]
        [MinLength(3, ErrorMessage = "Ad alanına en az 3 karakter girmelisiniz")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad Alanı Boş Geçilemez")]
        [MinLength(3, ErrorMessage = "Soyad alanına en az 3 karakter girmelisiniz")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Email Alanı Boş Geçilemez")]
        [RegularExpression(@"^$|^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Geçersiz e-posta adresi.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "TcNo Alanı Boş Geçilemez")]
        [RegularExpression("^[1-9][0-9]{10}$", ErrorMessage = "TcNo 11 rakamdan oluşmalı ve 0 ile başlamamalıdır")]
        public string TcNo { get; set; }

        [Required(ErrorMessage = "Parola Alanı Boş Geçilemez")]
        [MinLength(4, ErrorMessage = "Password alanına en az 4 karakter girmelisiniz")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Parola Doğrulama Alanı Boş Geçilemez")]
        public string ConfirmPassword { get; set; }
    }
}
