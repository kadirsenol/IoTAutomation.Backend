using IoTAutomation.EntityLayer.Abstract;

namespace IoTAutomation.EntityLayer.Concrete
{
    public class CartDetail : BaseEntity<int>
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int SolutionId { get; set; }
        public virtual Solution Solution { get; set; }
        public byte Quantity { get; set; }
    }
}
