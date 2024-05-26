using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.Concrete.VMs.SolutionVm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IoTAutomation.WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionController(ISolutionManager SolutionManager) : ControllerBase
    {
        private readonly ISolutionManager solutionManager = SolutionManager;


        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage));
                return BadRequest(errorMessage);
            }
            try
            {

                ICollection<Solution> solutions = await solutionManager.GetAll();

                return Ok(solutions);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Search(SearchSolutionVm solutionname)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.SelectMany(p => p.Errors.Select(e => e.ErrorMessage));
                return BadRequest(errorMessage);
            }
            try
            {
                if (solutionname.SolutionName == null || solutionname.SolutionName == "" || solutionname.SolutionName == " ")
                {
                    return Ok();
                }
                var result = await solutionManager._repo.dbContext.Solutions.Where(p => p.Name.Contains(solutionname.SolutionName)).ToListAsync();
                return Ok(result);


            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

    }
}
