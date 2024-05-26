using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete.VMs.AdminVms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoTAutomation.WebApiLayer.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProfileImageController(IProfileImageManager profileImageManager) : ControllerBase
    {
        private readonly IProfileImageManager profileImageManager = profileImageManager;

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProfileImages()
        {
            try
            {
                return Ok(await profileImageManager.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteProfileImage(DeleteById deleteById)
        {
            try
            {
                int sonuc = await profileImageManager.DeleteByPK(deleteById.Id);
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
