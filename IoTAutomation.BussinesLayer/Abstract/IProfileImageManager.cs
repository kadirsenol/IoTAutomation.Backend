using IoTAutomation.EntityLayer.Concrete;

namespace IoTAutomation.BussinesLayer.Abstract
{
    public interface IProfileImageManager : IManager<ProfileImage, int>
    {
        public Task<ProfileImage> GetByUserIdProfileImage(int userId);
    }
}
