using System.ComponentModel.DataAnnotations;

namespace IoTAutomation.EntityLayer.Concrete.VMs.AdminVms.SmartLightVms
{
    public class UpdateSmartLightAppVm
    {
        [Required(ErrorMessage = "Id Alanı Boş Bırakılamaz.")]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Lütfen sadece rakamlardan oluşan ve 0 ile başlamayan bir Id giriniz.")]
        public int Id { get; set; }

        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Lütfen sadece rakamlardan oluşan ve 0 ile başlamayan bir UserId giriniz.")]
        public string? UserId { get; set; }

        [RegularExpression("^(true|false)$", ErrorMessage = "Lütfen sadece 'true' veya 'false' olacak şekilde Light Mode değerini giriniz.")]
        public string? LightMode { get; set; }

        [RegularExpression(
        @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)|(([0-9a-fA-F]{1,4}:){7}([0-9a-fA-F]{1,4}|:)|(([0-9a-fA-F]{1,4}:){1,6}|:)((:[0-9a-fA-F]{1,4}){1,7}|:)))$",
        ErrorMessage = "Lütfen geçerli bir IPv4 veya IPv6 adresi giriniz."
        )]
        public string? EspIp { get; set; }

        [RegularExpression("^(true|false)$", ErrorMessage = "Lütfen sadece 'true' veya 'false' olacak şekilde Active değerini giriniz.")]
        public string? Active { get; set; }

        [RegularExpression("^(true|false)$", ErrorMessage = "Lütfen sadece 'true' veya 'false' olacak şekilde IsDelete değerini giriniz.")]
        public string? IsDelete { get; set; }
    }
}
