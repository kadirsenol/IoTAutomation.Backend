using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;

namespace IoTAutomation.BussinesLayer.Concrete
{
    public class SolutionManager : Manager<Solution, int>, ISolutionManager
    {
        public async Task<int> SolutionsCount()
        {
            return _repo.dbContext.Solutions.Count();
        }

        public async Task<bool> ChackSolutionUpdateNullorEmpty(IFormFile formFile, params string[] strings)
        {
            if ((strings.All(string.IsNullOrEmpty)) && formFile == null)
            {
                throw new Exception("Lütfen en az bir değişiklik giriniz");
            }
            return true;

        }

        public async Task<bool> ChackSolutionUpdateIsDeleteValid(string isdelete)
        {
            if ((!string.IsNullOrEmpty(isdelete)) && (isdelete != "false" && isdelete != "true"))
            {
                throw new Exception("Lütfen isDelete değerini 'true' veya 'false' olacak şekilde giriniz ");
            }

            return true;
        }


    }
}
