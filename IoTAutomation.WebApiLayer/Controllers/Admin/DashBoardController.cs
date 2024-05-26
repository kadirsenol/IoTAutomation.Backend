using IoTAutomation.BussinesLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoTAutomation.WebApiLayer.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class DashBoardController(IPreOrderManager preOrderManager, IUserManager userManager, ISmartLightAppManager smartLightAppManager, ISolutionManager solutionManager) : ControllerBase
    {
        private readonly IPreOrderManager preOrderManager = preOrderManager;
        private readonly IUserManager userManager = userManager;
        private readonly ISmartLightAppManager smartLightAppManager = smartLightAppManager;
        private readonly ISolutionManager solutionManager = solutionManager;

        [HttpGet("[action]")]
        public async Task<IActionResult> GetData()
        {
            try
            {
                int preOrdersCount = await preOrderManager.PreOrdersCount();
                int userCount = await userManager.UsersCount();
                int notActiveSmartLightAppCount = await smartLightAppManager.NotActiveAppsCount();
                int activeSmartLightAppCount = await smartLightAppManager.ActiveAppsCount();
                int solutionsCount = await solutionManager.SolutionsCount();


                return Ok(new { PreOrdersCount = preOrdersCount, UsersCount = userCount, NotActiveSmartLightAppsCount = notActiveSmartLightAppCount, ActiveSmartLightAppsCount = activeSmartLightAppCount, SolutionsCount = solutionsCount });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
