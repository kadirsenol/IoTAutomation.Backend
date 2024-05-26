using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace IoTAutomation.EntityLayer.Concrete.VMs.AdminVms.SolutionVms
{
    public class InsertSolutionVm
    {
        [Required(ErrorMessage = "Çözüm adı boş bırakılamaz.")]
        [MinLength(3, ErrorMessage = "Çözüm adı alanına en az 3 karakter girmelisiniz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Çözüm fiyatı boş bırakılamaz.")]
        [RegularExpression("^(?!0)\\d{1,5}$", ErrorMessage = "Lütfen sadece rakamdan ve 0 ile başlamayan, en fazla 99999 olacak şekilde Price giriniz.")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Description alanı boş bırakılamaz.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Detailed Description alanı boş bırakılamaz.")]
        public string DetailedDescription { get; set; }

        [RegularExpression("^(true|false)$", ErrorMessage = "Lütfen sadece 'true' veya 'false' olacak şekilde IsDelete değerini giriniz.")]
        public string? IsDelete { get; set; }

        [Required(ErrorMessage = "Çözüm resmi boş bırakılamaz.")]
        public IFormFile FileImage { get; set; }
    }
}
