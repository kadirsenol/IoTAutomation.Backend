using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace IoTAutomation.EntityLayer.Concrete.VMs.AdminVms.SolutionVms
{
    public class UpdateSolutionVm
    {
        [Required(ErrorMessage = "Id Alanı Boş Bırakılamaz.")]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Lütfen sadece rakamlardan oluşan ve 0 ile başlamayan bir Id giriniz.")]
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "Çözüm adı alanına en az 3 karakter girmelisiniz")]
        public string? Name { get; set; }

        [RegularExpression("^(?!0)\\d{1,5}$", ErrorMessage = "Lütfen sadece rakamdan ve 0 ile başlamayan, en fazla 99999 olacak şekilde Price giriniz.")]
        public string? Price { get; set; }
        public string? Description { get; set; }
        public string? DetailedDescription { get; set; }

        [RegularExpression("^(true|false)$", ErrorMessage = "Lütfen sadece 'true' veya 'false' olacak şekilde IsDelete değerini giriniz.")]
        public string? IsDelete { get; set; }
        public IFormFile? FileImage { get; set; } // List<IFormFile>? ile birden fazla veriyi alıp foreach ile donebilirsin
    }
}
