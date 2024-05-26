using AutoMapper;
using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.Concrete.VMs.AdminVms;
using IoTAutomation.EntityLayer.Concrete.VMs.AdminVms.UserVms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IoTAutomation.WebApiLayer.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class UserController(IUserManager userManager, IMapper mapper) : ControllerBase
    {
        private readonly IUserManager userManager = userManager;
        private readonly IMapper mapper = mapper;

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(await userManager.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteUser(DeleteById deleteById)
        {
            try
            {
                int sonuc = await userManager.DeleteByPK(deleteById.Id);
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
        public async Task<IActionResult> UpdateUser(UserUpdateVm userUpdateVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorMessage = ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage));
                    return BadRequest(errorMessage);
                }

                await userManager.ChackUserUpdateNullorEmpty(userUpdateVm.Ad, userUpdateVm.Soyad, userUpdateVm.TcNo, userUpdateVm.Email, userUpdateVm.Password
                     , userUpdateVm.Rol, userUpdateVm.isConfirmEmail, userUpdateVm.ConfirmEmailGuid, userUpdateVm.AccessToken, userUpdateVm.RefreshToken,
                     userUpdateVm.ExprationToken, userUpdateVm.IsDelete);

                await userManager.ChackUserUpdateIsDeleteIsConEmailValid(userUpdateVm.IsDelete, userUpdateVm.isConfirmEmail);

                if (!string.IsNullOrEmpty(userUpdateVm.ExprationToken))
                {
                    await userManager.ChackUserTokenExprationTimeValid(userUpdateVm.ExprationToken);
                }

                User user = mapper.Map<User>(userUpdateVm);
                if (string.IsNullOrEmpty(userUpdateVm.isConfirmEmail))
                {
                    user.isConfirmEmail = await userManager._repo.dbContext.Users.Where(p => p.Id == userUpdateVm.Id).Select(p => p.isConfirmEmail).FirstOrDefaultAsync();
                }
                if (string.IsNullOrEmpty(userUpdateVm.IsDelete))
                {
                    user.IsDelete = await userManager._repo.dbContext.Users.Where(p => p.Id == userUpdateVm.Id).Select(p => p.IsDelete).FirstOrDefaultAsync();
                }

                int sonuc = await userManager.update(user);
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
        public async Task<IActionResult> InsertUser(UserInsertVm userInsertVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorMessage = ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage));
                    return BadRequest(errorMessage);
                }


                await userManager.ChackUserUpdateIsDeleteIsConEmailValid(userInsertVm.IsDelete, userInsertVm.isConfirmEmail);

                if (!string.IsNullOrEmpty(userInsertVm.ExprationToken))
                {
                    await userManager.ChackUserTokenExprationTimeValid(userInsertVm.ExprationToken);
                }

                User user = mapper.Map<User>(userInsertVm);

                int sonuc = await userManager.Insert(user);

                if (sonuc > 0)
                {
                    return Ok("Kullanıcı oluşturuldu.");
                }
                return Problem("Kullanıcı oluşturulurken bir hata meydana geldi.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
