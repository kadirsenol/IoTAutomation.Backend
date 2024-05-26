using IoTAutomation.EntityLayer.Abstract;

namespace IoTAutomation.EntityLayer.Concrete
{
    public class PreOrderDetail : BaseEntity<int>
    {
        public int PreOrderId { get; set; }
        public PreOrder PreOrder { get; set; }
        public int SolutionId { get; set; }
        public Solution Solution { get; set; }
        public byte Quantity { get; set; }
    }
}
