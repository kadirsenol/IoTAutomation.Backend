using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.Concrete.VMs.CartVm;
using IoTAutomation.WebApiLayer.MyExtensions.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace IoTAutomation.WebApiLayer.Controllers
{
    [Authorize(Roles = "Admin, Üye")]
    [Route("api/[controller]")]
    [ApiController]
    public class PreOrderController(IHttpContextAccessor httpContextAccessor, ISolutionManager solutionManager, ISmartLightAppManager smartLightAppManager, IUserManager userManager, IPreOrderManager preOrderManager) : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
        private readonly ISolutionManager solutionManager = solutionManager;
        private readonly ISmartLightAppManager smartLightAppManager = smartLightAppManager;
        private readonly IUserManager userManager = userManager;
        private readonly IPreOrderManager preOrderManager = preOrderManager;

        [HttpPost("[action]")]
        public async Task<IActionResult> Insert(AcceptCartVm acceptCartVm)
        {
            try
            {
                string UserEmail = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email).Value.ToString();
                User user = await userManager.GetByEmailUser(UserEmail);

                List<string> solutionNames = new List<string>();
                for (int i = 0; i < acceptCartVm.Id.Count; i++)
                {
                    solutionNames.Add(await solutionManager._repo.dbContext.Solutions.Where(p => p.Id == acceptCartVm.Id[i]).Select(p => p.Name).FirstOrDefaultAsync());

                    if (solutionNames[i].Contains("Light"))
                    {
                        SmartLightApp smartLightApp = new SmartLightApp();
                        smartLightApp.UserId = user.Id;
                        await smartLightAppManager.Insert(smartLightApp);
                    }
                }


                PreOrder preOrder = new PreOrder();
                preOrder.UserId = user.Id;

                List<PreOrderDetail> preOrderDetails = new List<PreOrderDetail>();
                for (int i = 0; i < solutionNames.Count; i++)
                {
                    preOrderDetails.Add(new PreOrderDetail
                    {
                        SolutionId = acceptCartVm.Id[i],
                        Quantity = acceptCartVm.Quantity[i]
                    });
                }

                preOrder.PreOrderDetails = preOrderDetails;

                int response = await preOrderManager.Insert(preOrder);

                if (response > 0)
                {

                    EmailHelper emailHelper = new EmailHelper();
                    StringBuilder message = new();
                    message.AppendLine("<html>");
                    message.AppendLine("<head>")
                        .AppendLine("<meta charset='UTF-8'")
                        .AppendLine("</head>");
                    message.AppendLine("<body>");
                    message.AppendLine($"<p> Kadir Bey merhaba, {UserEmail} kullanıcısından ön sipariş talebi;</p> <br>");
                    message.AppendLine($@"<table style='margin-left: 5px; border-collapse: collapse;'>
                                                  <thead>
                                                      <tr>           
                                                          <th style='border: 1px solid #ddd; text-align: center;'>Solution Name</th>
                                                          <th style='border: 1px solid #ddd; text-align: center;'>Quantity</th>
                                                      </tr>
                                                  </thead>
                                                  <tbody>");

                    for (int i = 0; i < solutionNames.Count; i++)
                    {
                        message.AppendLine($@"<tr>
                                     <td style='border: 1px solid #ddd; text-align: center;'>{solutionNames[i]}</td>
                                     <td style='border: 1px solid #ddd; text-align: center;'>{acceptCartVm.Quantity[i]}</td>");
                        message.AppendLine("</tr>");
                    }

                    message.AppendLine($@"</tbody></table>");
                    message.AppendLine("</body>");
                    message.AppendLine("</html>");

                    bool sonuc = await emailHelper.SendEmail("kdrsnl_61@hotmail.com", message.ToString(), "Ön Sipariş Talebi");


                    if (sonuc)
                    {
                        return Ok($"Ön sipariş talebinizi aldım.! En kısa sürede geri döneceğim.");
                    }
                    return Problem("Ön sipariş gönderimi sırasında beklenmedik bir hata meydana geldi, lütfen tekrar deneyin.");
                }
                return Problem("Ön sipariş gönderimi sırasında beklenmedik bir hata meydana geldi, lütfen tekrar deneyin.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
