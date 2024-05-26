using IoTAutomation.EntityLayer.Concrete;

namespace IoTAutomation.BussinesLayer.Abstract
{
    public interface IPreOrderManager : IManager<PreOrder, int>
    {
        public Task<int> PreOrdersCount();
    }
}
