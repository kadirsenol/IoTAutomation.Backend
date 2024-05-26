using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace IoTAutomation.EntityLayer.Concrete.VMs.MailVm
{
    public class sendMailVm
    {
        [Required(ErrorMessage = "Mesaj Alanı Boş Bırakılamaz.")]  // tip yanına ? koymazsan kendisi required mesaji gonderir. Bu sekilde mesaji sen belirliyorsun
        public string Message { get; set; }

        [Required(ErrorMessage = "Email Alanı Boş Bırakılamaz.")]
        [RegularExpression(@"^$|^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Geçersiz e-posta adresi.")]
        public string SenderMailAdress { get; set; }

        public IFormFile? Attachment { get; set; } // List<IFormFile>? ile birden fazla veriyi alıp foreach ile donebilirsin
    }
}
