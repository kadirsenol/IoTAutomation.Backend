using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.Concrete.VMs.SmartLightAppVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IoTAutomation.WebApiLayer.Controllers
{
    [Authorize(Roles = "Admin, Üye")]
    [Route("api/[controller]")]
    [ApiController]
    public class SmartLightAppController(ISmartLightAppManager smartLightAppManager, IUserManager userManager, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        private readonly ISmartLightAppManager smartLightAppManager = smartLightAppManager;
        private readonly IUserManager userManager = userManager;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

        [HttpPost("[action]")]
        public async Task<IActionResult> GetLightModeAndEspIp(GetLightModeAndEspIpVm getLightModeAndEspIpVm)
        {
            try
            {
                string UserEmail = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email).Value.ToString();
                User user = await userManager.GetByEmailUser(UserEmail);

                if (await smartLightAppManager.CheckSmartLight(user.Id, getLightModeAndEspIpVm.SmartLightAppId) != null)
                {
                    bool lightmode = await smartLightAppManager.GetLightModeByUserAndAppNo(user.Id, getLightModeAndEspIpVm.SmartLightAppId);
                    string espIp = await smartLightAppManager._repo.dbContext.SmartLightApps.Where(p => p.UserId == user.Id && p.Id == getLightModeAndEspIpVm.SmartLightAppId).Select(p => p.EspIp).FirstOrDefaultAsync();
                    return Ok(new { LightMode = lightmode, EspIp = espIp });
                }

                return Problem("Beklenmedik bir hata meydana geldi, lütfen tekrar deneyin.");

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SetLightMode(SetLightModeVm setLightModeVm)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage));
                return BadRequest(errorMessage);
            }
            try
            {
                string UserEmail = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email).Value.ToString();
                User user = await userManager.GetByEmailUser(UserEmail);

                SmartLightApp smartLightApp = await smartLightAppManager.CheckSmartLight(user.Id, setLightModeVm.SmartLightAppId);
                if (smartLightApp != null)
                {
                    smartLightApp.LightMode = setLightModeVm.LightMode;
                    int response = await smartLightAppManager.update(smartLightApp);
                    if (response > 0)
                    {
                        return Ok();
                    }
                    return Problem("Beklenmedik bir hata meydana geldi. Lütfen sayfayı yenileyerek tekrar deneyiniz.");
                }

                return Problem("Beklenmedik bir hata meydana geldi. Lütfen sayfayı yenileyerek tekrar deneyiniz.");


            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
