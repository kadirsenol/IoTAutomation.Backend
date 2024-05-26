using IoTAutomation.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;

namespace IoTAutomation.BussinesLayer.Abstract
{
    public interface ISolutionManager : IManager<Solution, int>
    {
        public Task<int> SolutionsCount();
        public Task<bool> ChackSolutionUpdateNullorEmpty(IFormFile formFile, params string[] strings);
        public Task<bool> ChackSolutionUpdateIsDeleteValid(string isdelete);



    }
}
