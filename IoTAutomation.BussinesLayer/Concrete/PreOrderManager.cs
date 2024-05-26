using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete;

namespace IoTAutomation.BussinesLayer.Concrete
{
    public class PreOrderManager : Manager<PreOrder, int>, IPreOrderManager
    {
        public async Task<int> PreOrdersCount()
        {
            return _repo.dbContext.PreOrders.Count();
        }

    }
}
