using IoTAutomation.EntityLayer.Abstract;

namespace IoTAutomation.EntityLayer.Concrete
{
    public class User : BaseEntity<int>
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string TcNo { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public bool isConfirmEmail { get; set; } = false;
        public string? ConfirmEmailGuid { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExprationToken { get; set; } // Tokenin gecerlilik suresi
        public virtual ProfileImage ProfileImage { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual ICollection<SmartLightApp> SmartLightSolutionApps { get; set; }
        public virtual ICollection<PreOrder> PreOrders { get; set; }



    }
}
