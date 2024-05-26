using System.ComponentModel.DataAnnotations;

namespace IoTAutomation.EntityLayer.Concrete.VMs.ProfileVm
{
    public class SaveProfileImageVm
    {
        [Required(ErrorMessage = "Lütfen bir resim seçiniz.")]
        public string Image { get; set; }
    }
}
