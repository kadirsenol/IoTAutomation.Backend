using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.Concrete.VMs.CartVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IoTAutomation.WebApiLayer.Controllers
{
    [Authorize(Roles = "Admin, Üye")]
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailController(ICartDetailManager cartManager, IUserManager userManager, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        private readonly ICartDetailManager cartManager = cartManager;
        private readonly IUserManager userManager = userManager;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

        [HttpPost("[action]")]
        public async Task<IActionResult> Insert(InsertCartVm insertCartVm)
        {
            try
            {
                string UserEmail = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email).Value.ToString();

                User user = await userManager.GetByEmailUser(UserEmail);

                if (insertCartVm.Quantity == 1)
                {
                    bool solutionExists = await cartManager.Any(p => p.UserId == user.Id && p.SolutionId == insertCartVm.SolutionId);
                    if (solutionExists)
                    {
                        CartDetail cart = await cartManager._repo.dbContext.CartDetails.FirstOrDefaultAsync(p => p.UserId == user.Id && p.SolutionId == insertCartVm.SolutionId);
                        cart.Quantity += 1;
                        await cartManager.update(cart);
                        return Ok("Sepette ki çözümün adedi arttirildi.");
                    }

                    int sonuc = await cartManager.Insert(new CartDetail { UserId = user.Id, SolutionId = insertCartVm.SolutionId, Quantity = insertCartVm.Quantity });
                    if (sonuc > 0)
                    {
                        return Ok("Çözüm başarıyla sepete eklendi !");

                    }
                    return Problem("Çözüm sepete eklenirken hata meydana geldi.");
                }
                else
                {
                    bool solutionExists = await cartManager.Any(p => p.UserId == user.Id && p.SolutionId == insertCartVm.SolutionId);
                    if (solutionExists)
                    {
                        CartDetail cart = await cartManager._repo.dbContext.CartDetails.FirstOrDefaultAsync(p => p.UserId == user.Id && p.SolutionId == insertCartVm.SolutionId);
                        cart.Quantity -= 1;
                        await cartManager.update(cart);
                        return Ok("Sepette ki çözümün adedi azaltildi.");
                    }
                    else
                    {
                        return Problem("Sepetteki çözümün adedi azaltilirken bir hata meydana geldi.");
                    }
                }


            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                string UserEmail = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email).Value.ToString();


                int id = await userManager.GetByEmailUserForCart(UserEmail);

                var cartData = await cartManager._repo.dbContext.CartDetails
                                                 .Where(cart => cart.UserId == id)
                                                 .Include(cart => cart.Solution) // İlgili çözümleri dahil et
                                                 .ToListAsync();

                var solutionsWithQuantities = cartData
                    .GroupBy(cart => cart.SolutionId) // Çözümleri grupla
                    .Select(group => new
                    {
                        Name = group.FirstOrDefault().Solution.Name,
                        Image = group.FirstOrDefault().Solution.Image,
                        Price = group.FirstOrDefault().Solution.Price,
                        Id = group.FirstOrDefault().Solution.Id,
                        Quantity = group.Sum(cart => cart.Quantity)
                    }) // Çözümlerle birlikte adetleri al
                    .ToList();

                return Ok(solutionsWithQuantities);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(DeleteCartVm deleteCartVm)
        {
            try
            {
                string UserEmail = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email).Value.ToString();

                int id = await userManager.GetByEmailUserForCart(UserEmail);

                if (deleteCartVm.TriggerId == 0 || deleteCartVm.TriggerId == null)
                {
                    int sonuc = await cartManager.DeleteAll(p => p.UserId == id);
                    if (sonuc > 0)
                    {
                        return Ok("Sepetiniz boşaltıldı.");
                    }
                    else
                    {
                        return Problem("Sepetiniz silinirken beklenmedik bir hata meydana geldi.");
                    }
                }
                else
                {
                    int sonuc = await cartManager.DeleteAll(p => p.UserId == id && p.SolutionId == deleteCartVm.TriggerId);
                    if (sonuc > 0)
                    {
                        return Ok("İlgili ürün sepetinizden kaldırıldı.");
                    }
                    else
                    {
                        return Problem("İlgili ürün silinirken beklenmedik bir hata meydana geldi.");
                    }
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
