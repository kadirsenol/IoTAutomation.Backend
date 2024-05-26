using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace IoTAutomation.BussinesLayer.Concrete
{
    public class SmartLightAppManager : Manager<SmartLightApp, int>, ISmartLightAppManager
    {

        public async Task<SmartLightApp> CheckSmartLight(int userId, int SmartLigstAppId)
        {
            SmartLightApp smartLightApp = await _repo.dbContext.SmartLightApps.Where(p => p.UserId == userId && p.Id == SmartLigstAppId).FirstOrDefaultAsync();

            if (smartLightApp == null)
            {
                return null;
            }
            return smartLightApp;
        }
        public async Task<bool> GetLightModeByUserAndAppNo(int userId, int SmartLigstAppId)
        {
            return await _repo.dbContext.SmartLightApps.Where(p => p.UserId == userId && p.Id == SmartLigstAppId).Select(p => p.LightMode).FirstOrDefaultAsync();
        }

        public async Task<int> ActiveAppsCount()
        {
            return _repo.dbContext.SmartLightApps.Where(p => p.Active == true).Count();
        }

        public async Task<int> NotActiveAppsCount()
        {
            return _repo.dbContext.SmartLightApps.Where(p => p.Active == false).Count();
        }

        public async Task<bool> CheckSmartLightAppUpdateNullorEmpty(params string[] strings)
        {
            if (strings.All(string.IsNullOrEmpty))
            {
                throw new Exception("Lütfen en az bir değişiklik giriniz");
            }
            return true;
        }

        public async Task<bool> CheckSmartLightAppBoolsValid(string isdelete, string active, string lightmode)
        {
            if ((!string.IsNullOrEmpty(isdelete)) && (isdelete != "false" && isdelete != "true"))
            {
                throw new Exception("Lütfen isDelete değerini 'true' veya 'false' olacak şekilde giriniz ");
            }
            if ((!string.IsNullOrEmpty(active)) && (active != "false" && active != "true"))
            {
                throw new Exception("Lütfen Active değerini 'true' veya 'false' olacak şekilde giriniz ");
            }
            if ((!string.IsNullOrEmpty(lightmode)) && (lightmode != "false" && lightmode != "true"))
            {
                throw new Exception("Lütfen Light Mode değerini 'true' veya 'false' olacak şekilde giriniz ");
            }

            return true;
        }
        public async Task<bool> CheckOfUsedEspIp(string espIp)
        {

            if (await Any(p => p.EspIp == espIp))
            {
                throw new Exception("Girmiş olduğunuz Esp Ip kullanılıyor.");
            }
            return true;
        }

        public async Task<bool> CheckOfAnyUserId(string userId)
        {
            if (await _repo.dbContext.Users.AnyAsync(p => p.Id == int.Parse(userId)))
            {
                return true;
            }

            throw new Exception("Girmiş olduğunuz UserId ile eşleşen kullanıcı yok.");

        }


    }
}
