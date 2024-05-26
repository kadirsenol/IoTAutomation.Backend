using IoTAutomation.EntityLayer.Concrete;

namespace IoTAutomation.BussinesLayer.Abstract
{
    public interface ISmartLightAppManager : IManager<SmartLightApp, int>
    {
        public Task<bool> GetLightModeByUserAndAppNo(int userId, int SmartLigstAppId);

        public Task<SmartLightApp> CheckSmartLight(int userId, int SmartLigstAppId);

        public Task<int> ActiveAppsCount();
        public Task<int> NotActiveAppsCount();

        public Task<bool> CheckSmartLightAppUpdateNullorEmpty(params string[] strings);
        public Task<bool> CheckSmartLightAppBoolsValid(string isdelete, string active, string lightmode);

        public Task<bool> CheckOfUsedEspIp(string espIp);
        public Task<bool> CheckOfAnyUserId(string userId);

    }
}
