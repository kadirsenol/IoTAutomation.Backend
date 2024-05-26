using AutoMapper;
using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.Concrete.VMs.AdminVms;
using IoTAutomation.EntityLayer.Concrete.VMs.AdminVms.SolutionVms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IoTAutomation.WebApiLayer.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class SolutionController(ISolutionManager solutionManager, IMapper mapper) : ControllerBase
    {
        private readonly ISolutionManager solutionManager = solutionManager;
        private readonly IMapper mapper = mapper;

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSolutions()
        {
            try
            {
                return Ok(await solutionManager.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteSolution(DeleteById deleteById)
        {
            try
            {
                int sonuc = await solutionManager.DeleteByPK(deleteById.Id);
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
        public async Task<IActionResult> UpdateSolution([FromForm] UpdateSolutionVm updateSolutionVm) //[FromForm]Gövdesinde form verisi alabilir 
        {
            if (!ModelState.IsValid) // Eger browserde js kapatilmissa buraya takilip yine validleri gonderecek.
            {
                var errorMessage = ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage));
                return BadRequest(errorMessage);
            }
            try
            {
                await solutionManager.ChackSolutionUpdateNullorEmpty(updateSolutionVm.FileImage, updateSolutionVm.Name, updateSolutionVm.Price
                    , updateSolutionVm.Description, updateSolutionVm.DetailedDescription);
                await solutionManager.ChackSolutionUpdateIsDeleteValid(updateSolutionVm.IsDelete);

                Solution solution = mapper.Map<Solution>(updateSolutionVm);
                if (updateSolutionVm.FileImage != null)
                {
                    solution.Image = updateSolutionVm.FileImage.FileName;
                }

                if (string.IsNullOrEmpty(updateSolutionVm.IsDelete))
                {
                    solution.IsDelete = await solutionManager._repo.dbContext.Solutions.Where(p => p.Id == updateSolutionVm.Id).Select(p => p.IsDelete).FirstOrDefaultAsync();
                }
                if (string.IsNullOrEmpty(updateSolutionVm.Price))
                {
                    solution.Price = await solutionManager._repo.dbContext.Solutions.Where(p => p.Id == updateSolutionVm.Id).Select(p => p.Price).FirstOrDefaultAsync();
                }

                if (updateSolutionVm.FileImage != null)
                {
                    // "public" klasörünün fiziksel yolu
                    var publicFolder = Path.Combine(Directory.GetCurrentDirectory(), @$"C:\Users\kdrsn\source\repos\Visual Studio Code\MyGithubProjects\React-Node.js-JQuery\useeffect.map.router.formik.yup.tailwindcss.flowbite.mui.toastify\public");

                    // "public" klasörü yoksa 
                    if (!Directory.Exists(publicFolder))
                    {
                        throw new Exception("Frontend projenin public klasör dizini bulunamadı !");
                    }

                    // Dosyanın tam yolu
                    var filePath = Path.Combine(publicFolder, updateSolutionVm.FileImage.FileName);

                    // Dosyayı "public" klasörüne taşı
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        updateSolutionVm.FileImage.CopyTo(stream);
                    }
                }

                int sonuc = await solutionManager.update(solution);
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
        public async Task<IActionResult> InsertSolution(InsertSolutionVm insertSolutionVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errorMessage = ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage));
                    return BadRequest(errorMessage);
                }

                await solutionManager.ChackSolutionUpdateIsDeleteValid(insertSolutionVm.IsDelete);

                Solution solution = mapper.Map<Solution>(insertSolutionVm);

                solution.Image = insertSolutionVm.FileImage.FileName;

                // "public" klasörünün fiziksel yolu
                var publicFolder = Path.Combine(Directory.GetCurrentDirectory(), @$"C:\Users\kdrsn\source\repos\Visual Studio Code\MyGithubProjects\React-Node.js-JQuery\useeffect.map.router.formik.yup.tailwindcss.flowbite.mui.toastify\public");

                // "public" klasörü yoksa 
                if (!Directory.Exists(publicFolder))
                {
                    throw new Exception("Frontend projenin public klasör dizini bulunamadı !");
                }

                // Dosyanın tam yolu
                var filePath = Path.Combine(publicFolder, insertSolutionVm.FileImage.FileName);

                // Dosyayı "public" klasörüne taşı
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    insertSolutionVm.FileImage.CopyTo(stream);
                }

                int sonuc = await solutionManager.Insert(solution);
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
