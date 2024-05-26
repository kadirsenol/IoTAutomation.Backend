using AutoMapper;
using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.Concrete.VMs.AdminVms;
using IoTAutomation.EntityLayer.Concrete.VMs.AdminVms.SmartLightVms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IoTAutomation.WebApiLayer.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class SmartLightAppController(ISmartLightAppManager smartLightAppManager, IMapper mapper) : ControllerBase
    {
        private readonly ISmartLightAppManager smartLightAppManager = smartLightAppManager;
        private readonly IMapper mapper = mapper;

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSmartLightApps()
        {
            try
            {
                return Ok(await smartLightAppManager.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteSmartLightApp(DeleteById deleteById)
        {
            try
            {
                int sonuc = await smartLightAppManager.DeleteByPK(deleteById.Id);
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
        public async Task<IActionResult> UpdateSmartLightApp(UpdateSmartLightAppVm updateSmartLightAppVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorMessage = ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage));
                    return BadRequest(errorMessage);
                }

                await smartLightAppManager.CheckSmartLightAppUpdateNullorEmpty(updateSmartLightAppVm.UserId, updateSmartLightAppVm.LightMode, updateSmartLightAppVm.EspIp
                    , updateSmartLightAppVm.Active, updateSmartLightAppVm.IsDelete);


                await smartLightAppManager.CheckSmartLightAppBoolsValid(updateSmartLightAppVm.IsDelete, updateSmartLightAppVm.Active, updateSmartLightAppVm.LightMode);

                SmartLightApp smartLightApp = mapper.Map<SmartLightApp>(updateSmartLightAppVm);
                if (string.IsNullOrEmpty(updateSmartLightAppVm.IsDelete))
                {
                    smartLightApp.IsDelete = await smartLightAppManager._repo.dbContext.SmartLightApps.Where(p => p.Id == updateSmartLightAppVm.Id).Select(p => p.IsDelete).FirstOrDefaultAsync();
                }
                if (string.IsNullOrEmpty(updateSmartLightAppVm.LightMode))
                {
                    smartLightApp.LightMode = await smartLightAppManager._repo.dbContext.SmartLightApps.Where(p => p.Id == updateSmartLightAppVm.Id).Select(p => p.LightMode).FirstOrDefaultAsync();
                }
                if (string.IsNullOrEmpty(updateSmartLightAppVm.Active))
                {
                    smartLightApp.Active = await smartLightAppManager._repo.dbContext.SmartLightApps.Where(p => p.Id == updateSmartLightAppVm.Id).Select(p => p.Active).FirstOrDefaultAsync();
                }
                if (string.IsNullOrEmpty(updateSmartLightAppVm.UserId))
                {
                    smartLightApp.UserId = await smartLightAppManager._repo.dbContext.SmartLightApps.Where(p => p.Id == updateSmartLightAppVm.Id).Select(p => p.UserId).FirstOrDefaultAsync();
                }
                if (!string.IsNullOrEmpty(updateSmartLightAppVm.EspIp))
                {
                    await smartLightAppManager.CheckOfUsedEspIp(updateSmartLightAppVm.EspIp);
                }

                int sonuc = await smartLightAppManager.update(smartLightApp);
                if (sonuc > 0)
                {
                    return Ok("Güncelleme İşlemi Başarılı.");
                }
                return Problem("Güncelleme sırasında bir hata meydana geldi.");

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> InsertSmartLightApp(InsertSmartLightAppVm insertSmartLightAppVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorMessage = ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage));
                    return BadRequest(errorMessage);
                }

                await smartLightAppManager.CheckSmartLightAppBoolsValid(insertSmartLightAppVm.IsDelete, insertSmartLightAppVm.Active, insertSmartLightAppVm.LightMode);

                SmartLightApp smartLightApp = mapper.Map<SmartLightApp>(insertSmartLightAppVm);

                if (!string.IsNullOrEmpty(insertSmartLightAppVm.EspIp))
                {
                    await smartLightAppManager.CheckOfUsedEspIp(insertSmartLightAppVm.EspIp);
                }

                await smartLightAppManager.CheckOfAnyUserId(insertSmartLightAppVm.UserId);

                int sonuc = await smartLightAppManager.Insert(smartLightApp);
                if (sonuc > 0)
                {
                    return Ok("Kayıt İşlemi Başarılı.");
                }
                return Problem("Kayıt sırasında bir hata meydana geldi.");

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
