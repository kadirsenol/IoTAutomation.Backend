using IoTAutomation.EntityLayer.Abstract;

namespace IoTAutomation.EntityLayer.Concrete
{
    public class PreOrder : BaseEntity<int>
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<PreOrderDetail> PreOrderDetails { get; set; }
    }
}
