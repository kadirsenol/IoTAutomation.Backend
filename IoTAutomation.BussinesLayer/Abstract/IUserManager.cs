using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.Concrete.VMs.UserVM;

namespace IoTAutomation.BussinesLayer.Abstract
{
    public interface IUserManager : IManager<User, int>
    {
        //Gerekli is kurallari var ise eklenecek.
        public Task<User> ChackUserLogin(User entity);
        public Task<bool> ChackUserRegister(User entity);

        public Task<bool> ChackConfirmPassword(string password, string confirmpassword);

        public Task<bool> ChackTcNo(UserRegisterVm userRegisterVm);
        public Task<bool> ConfirmEmailAsync(string uid, string code);

        public Task<User> GetByEmailUser(string email);

        public Task<string> CreateEmailConfirmGuidCode(User id);

        public Task<bool> ChackUserEmailConfirm(User user);

        public Task<string> GetByEmailToken(string email);

        public Task<int> GetByEmailUserForCart(string email);

        public Task<bool> ChackSaveChangeNullorEmpty(string name, string surname, string password);

        public Task<int> UsersCount();

        public Task<bool> ChackUserUpdateNullorEmpty(params string[] strings);
        public Task<bool> ChackUserUpdateIsDeleteIsConEmailValid(string isdelete, string isconfirmemail);

        public Task<bool> ChackUserTokenExprationTimeValid(string tokenExpration);


    }
}
