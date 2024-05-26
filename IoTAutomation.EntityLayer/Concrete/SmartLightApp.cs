using IoTAutomation.EntityLayer.Abstract;

namespace IoTAutomation.EntityLayer.Concrete
{
    public class SmartLightApp : BaseEntity<int>
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public bool LightMode { get; set; } = false;
        public string? EspIp { get; set; }
        public bool Active { get; set; } = false;
    }
}
