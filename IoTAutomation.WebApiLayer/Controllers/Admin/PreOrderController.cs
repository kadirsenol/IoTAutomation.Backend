using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete.VMs.AdminVms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoTAutomation.WebApiLayer.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class PreOrderController(IPreOrderManager preOrderManager, IPreOrderDetailManager preOrderDetailManager) : ControllerBase
    {

        private readonly IPreOrderManager preOrderManager = preOrderManager;
        private readonly IPreOrderDetailManager preOrderDetailManager = preOrderDetailManager;

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPreOrders()
        {
            try
            {
                return Ok(await preOrderManager.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetPreOrderDetails()
        {
            try
            {
                return Ok(await preOrderDetailManager.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeletePreOrder(DeleteById deleteById)
        {
            try
            {
                int sonuc = await preOrderManager.DeleteByPK(deleteById.Id);
                if (sonuc > 0)
                {
                    return Ok("İlgili kayıt başarıyla silindi");

                }
                return Problem("İlgili kayıt silinirken bir hata meydana geldi.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeletePreOrderDetail(DeleteById deleteById)
        {
            try
            {
                int sonuc = await preOrderDetailManager.DeleteByPK(deleteById.Id);
                if (sonuc > 0)
                {
                    return Ok("İlgili kayıt başarıyla silindi");

                }
                return Problem("İlgili kayıt silinirken bir hata meydana geldi.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
