using IoTAutomation.EntityLayer.Abstract;

namespace IoTAutomation.EntityLayer.Concrete
{
    public class Solution : BaseEntity<int>
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; } //Path
        public string Description { get; set; }
        public string DetailedDescription { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual ICollection<PreOrderDetail> PreOrderDetails { get; set; }

    }
}
