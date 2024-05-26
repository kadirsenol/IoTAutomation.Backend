using AutoMapper;
using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.Concrete.VMs.ProfileVm;
using IoTAutomation.WebApiLayer.MyExtensions.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace IoTAutomation.WebApiLayer.Controllers
{
    [Authorize(Roles = "Admin, Üye")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController(IUserManager userManager, IProfileImageManager profileImageManager, ISmartLightAppManager smartLightAppManager, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : ControllerBase
    {
        private readonly IUserManager userManager = userManager;
        private readonly IProfileImageManager profileImageManager = profileImageManager;
        private readonly ISmartLightAppManager smartLightAppManager = smartLightAppManager;
        private readonly IMapper mapper = mapper;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
        private readonly IConfiguration configuration = configuration;

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveChange(SaveChangeVm saveChangeVm)
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

                if (user.Password == saveChangeVm.Password)
                {
                    if (!saveChangeVm.NewPassword.IsNullOrEmpty())
                    {
                        await userManager.ChackConfirmPassword(saveChangeVm.NewPassword, saveChangeVm.ConfirmPassword);
                    }

                    await userManager.ChackSaveChangeNullorEmpty(saveChangeVm.Ad, saveChangeVm.Soyad, saveChangeVm.NewPassword);

                    user.Ad = string.IsNullOrEmpty(saveChangeVm.Ad) ? null : saveChangeVm.Ad;
                    user.Soyad = string.IsNullOrEmpty(saveChangeVm.Soyad) ? null : saveChangeVm.Soyad;
                    user.Password = string.IsNullOrEmpty(saveChangeVm.NewPassword) ? null : saveChangeVm.NewPassword;


                    int sonuc = await userManager.update(user);

                    User updatedUser = await userManager.GetByEmailUser(UserEmail);
                    TokenManager tokenManager = new TokenManager();

                    User userwithToken = await tokenManager.CreateToken(updatedUser, configuration);


                    if (sonuc > 0)
                    {
                        return Ok(new { accessToken = userwithToken.AccessToken, message = "Hesap bilgileriniz güncellendi. !" });
                    }
                    else
                    {
                        return Problem("Hesap bilgileri güncellenirken beklenmedik bir problem meydana geldi.");
                    }
                }
                else
                {
                    return Problem("Parola hatalı !");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveProfileImage(SaveProfileImageVm saveImageVm)
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

                ProfileImage profileImage = await profileImageManager.GetByUserIdProfileImage(user.Id);

                if (profileImage == null)
                {
                    ProfileImage profileimage = mapper.Map<ProfileImage>(saveImageVm);
                    profileimage.UserId = user.Id;
                    int sonuc = await profileImageManager.Insert(profileimage);
                    if (sonuc > 0)
                    {
                        return Ok("Profil resminiz başarıyla yüklendi.");
                    }
                    else
                    {
                        return Problem("Profil resmi yüklenirken bir hata meydana geldi");
                    }
                }
                else
                {
                    profileImage.Image = saveImageVm.Image;
                    int sonuc = await profileImageManager.update(profileImage);
                    if (sonuc > 0)
                    {
                        return Ok("Profil resminiz başarıyla güncellendi.");
                    }
                    else
                    {
                        return Problem("Profil resmi güncellenirken bir hata meydana geldi");
                    }
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProfileData()
        {
            try
            {
                string UserEmail = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email).Value.ToString();
                User user = await userManager.GetByEmailUser(UserEmail);

                ProfileImage profileImage = await profileImageManager.GetByUserIdProfileImage(user.Id);


                //List<int> smartLightAppIds = new List<int>();

                //smartLightAppIds = await smartLightAppManager._repo.dbContext.SmartLightApps.Where(p => p.UserId == user.Id).Select(p =>  p.Id ).ToListAsync();



                var smartLightAppData = await smartLightAppManager._repo.dbContext.SmartLightApps
                                            .Where(p => p.UserId == user.Id)
                                            .Select(p => new { Id = p.Id, Active = p.Active })
                                            .ToListAsync();



                return Ok(new { SmartLightAppIdActive = smartLightAppData, image = profileImage != null ? profileImage.Image : null });

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> DeleteProfileImage()
        {
            try
            {
                string UserEmail = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email).Value.ToString();
                User user = await userManager.GetByEmailUser(UserEmail);

                ProfileImage profileImage = await profileImageManager.GetByUserIdProfileImage(user.Id);

                if (profileImage == null)
                {
                    return Problem("Profil resmi silme işlemi sırasında bir hata meydana geldi.");
                }

                int sonuc = await profileImageManager.Delete(profileImage);
                if (sonuc > 0)
                {
                    return Ok("Profil resminiz kaldırıldı.");
                }
                return Problem("Profil resmi silme işlemi sırasında bir hata meydana geldi.");

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
