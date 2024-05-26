using IoTAutomation.EntityLayer.Abstract;

namespace IoTAutomation.EntityLayer.Concrete
{
    public class ProfileImage : BaseEntity<int>
    {
        public string Image { get; set; } //Base64
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
