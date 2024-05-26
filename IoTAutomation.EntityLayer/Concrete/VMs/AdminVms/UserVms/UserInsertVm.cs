using System.ComponentModel.DataAnnotations;

namespace IoTAutomation.EntityLayer.Concrete.VMs.AdminVms.UserVms
{
    public class UserInsertVm
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

        [Required(ErrorMessage = "Rol Alanı Boş Geçilemez")]
        public string Rol { get; set; }

        [RegularExpression("^(true|false)$", ErrorMessage = "Lütfen sadece 'true' veya 'false' olacak şekilde isConfirmEmail değerini giriniz.")]
        public string? isConfirmEmail { get; set; }
        public string? ConfirmEmailGuid { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }

        [RegularExpression("^(true|false)$", ErrorMessage = "Lütfen sadece 'true' veya 'false' olacak şekilde IsDelete değerini giriniz.")]
        public string? IsDelete { get; set; }
        public string? ExprationToken { get; set; }
    }
}
