using IoTAutomation.EntityLayer.Concrete.VMs.MailVm;
using IoTAutomation.WebApiLayer.MyExtensions.Email;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace IoTAutomation.WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : Controller
    {

        [HttpPost("[action]")]
        public async Task<IActionResult> SendEmail([FromForm] sendMailVm sendMailVm) //[FromForm]Gövdesinde form verisi alabilir 
        {
            if (!ModelState.IsValid) // Eger browserde js kapatilmissa buraya takilip yine validleri gonderecek.
            {
                var errorMessage = ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage));
                return BadRequest(errorMessage);
            }
            try
            {
                if (sendMailVm.Attachment != null && sendMailVm.Attachment.Length > 3000000)
                {
                    return Problem("Please upload a file of maximum 3mb in size.");
                }
                EmailHelper emailHelper = new EmailHelper();

                StringBuilder message = new();
                message.AppendLine("<html>");
                message.AppendLine("<head>")
                    .AppendLine("<meta charset='UTF-8'")
                    .AppendLine("</head>");
                message.AppendLine("<body>");
                message.AppendLine($"<p> Kadir Bey merhaba, {sendMailVm.SenderMailAdress} kullanıcısından mesajınız var;</p> <br>");
                message.AppendLine($"<p style='margin-left: 5px;'>{sendMailVm.Message}</p>");
                message.AppendLine("</body>");
                message.AppendLine("</html>");

                bool sonuc = await emailHelper.SendEmail("kdrsnl_61@hotmail.com", message.ToString(), "Kullanıcı Mesajı", sendMailVm.Attachment);
                if (sonuc)
                {
                    return Ok($"Mesajınız bana ulaştı, en kısa sürede geri döneceğim. Teşekkür ederim.");
                }
                return Problem();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }


        }
    }
}
