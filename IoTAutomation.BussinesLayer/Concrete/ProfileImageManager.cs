using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete;

namespace IoTAutomation.BussinesLayer.Concrete
{
    public class ProfileImageManager : Manager<ProfileImage, int>, IProfileImageManager
    {
        public async Task<ProfileImage> GetByUserIdProfileImage(int userId)
        {
            if (userId == null || userId == 0)
            {
                throw new Exception("Profil fotoğrafı yükleme sırasında bir hata meydana geldi.");
            }

            return await FirstOrDefault(p => p.UserId == userId);

        }

    }
}
