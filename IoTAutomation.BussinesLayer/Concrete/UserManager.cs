using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.Concrete.VMs.UserVM;
using System.Globalization;

namespace IoTAutomation.BussinesLayer.Concrete
{
    public class UserManager : Manager<User, int>, IUserManager
    {

        //IUserManagerde is kurallari olusturursan ilgili manager metodunu override edip configure edebilirsin.
        public async Task<User> ChackUserLogin(User entity)
        {
            User user = await FirstOrDefault(p => p.Email == entity.Email && p.Password == entity.Password);
            if (user == null)
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı");
            }
            else
            {
                return user;
            }

        }
        public async Task<bool> ChackUserRegister(User entity)
        {
            User user = await FirstOrDefault(p => p.Email == entity.Email);
            if (user == null)
            {
                return true;
            }
            else
            {
                throw new Exception("Kullanıcı zaten mevcut. !");
            }

        }

        public async Task<bool> ChackConfirmPassword(string password, string confirmpassword)
        {
            if (password == confirmpassword)
            {
                return true;
            }
            else
            {
                throw new Exception("Girilen parolalar eşleşmiyor. !");
            }

        }

        public async Task<bool> ChackTcNo(UserRegisterVm userRegisterVm)
        {

            if (!long.TryParse(userRegisterVm.TcNo, out long sonuc))
            {
                throw new Exception("Lütfen TcNo alanını sadece rakamdan oluşacak şekilde giriniz.");
            }
            else if (userRegisterVm.TcNo.Length != 11)
            {
                throw new Exception("Lütfen TcNo alanını 11 karakterden oluşacak şekilde giriniz.");
            }

            return true;
        }

        public async Task<User> GetByEmailUser(string email)
        {
            User user = await FirstOrDefault(p => p.Email == email);
            if (user == null)
            {
                throw new Exception("Kayıt işlemi sırasında beklenmedik bir hata meydana geldi, lütfen tekrar kayıt olunuz.");
            }
            return user;
        }


        public async Task<bool> ConfirmEmailAsync(string uid, string code)
        {
            User user = await GetByPK(byte.Parse(uid));

            if (user == null || user.ConfirmEmailGuid != code)
            {
                throw new Exception("Email confirm sırasında bir hata meydana geldi, lütfen tekrar deneyin");
            }

            user.isConfirmEmail = true;
            await update(user);

            return true;
        }

        public async Task<string> CreateEmailConfirmGuidCode(User user)
        {
            Guid guid = Guid.NewGuid();
            string code = guid.ToString();
            user.ConfirmEmailGuid = code;
            await update(user);
            return code;
        }

        public async Task<bool> ChackUserEmailConfirm(User user)
        {
            User myuser = await GetByPK(user.Id);
            if (myuser.isConfirmEmail)
            {
                return true;
            }
            else
            {
                throw new Exception("Lütfen email adresinizi, mail adresinize gönderdiğim linkten onaylayınız.");
            }

        }

        public async Task<string> GetByEmailToken(string email)
        {
            User user = await FirstOrDefault(p => p.Email == email);
            if (user == null)
            {
                return null;
            }
            return user.AccessToken;
        }

        public async Task<int> GetByEmailUserForCart(string email)
        {
            User user = await FirstOrDefault(p => p.Email == email);
            if (user == null)
            {
                throw new Exception("Beklenmedik bir durum meydana geldi. Hesabinizdan cikis yapiliyor lütfen tekrar giris yapiniz.");
            }
            return user.Id;
        }

        public async Task<bool> ChackSaveChangeNullorEmpty(string name, string surname, string password)
        {
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(surname) && string.IsNullOrEmpty(password))
            {
                throw new Exception("Lütfen en az bir değişiklik giriniz");
            }
            return true;
        }
        public async Task<bool> ChackUserUpdateNullorEmpty(params string[] strings)
        {
            if (strings.All(string.IsNullOrEmpty))
            {
                throw new Exception("Lütfen en az bir değişiklik giriniz");
            }
            return true;

        }

        public async Task<bool> ChackUserUpdateIsDeleteIsConEmailValid(string isdelete, string isconfirmemail)
        {
            if ((!string.IsNullOrEmpty(isdelete)) && (isdelete != "false" && isdelete != "true"))
            {
                throw new Exception("Lütfen isDelete değerini 'true' veya 'false' olacak şekilde giriniz ");
            }
            if ((!string.IsNullOrEmpty(isconfirmemail)) && (isconfirmemail != "false" && isconfirmemail != "true"))
            {
                throw new Exception("Lütfen isConfirmEmail değerini 'true' veya 'false' olacak şekilde giriniz ");
            }

            return true;
        }

        public async Task<bool> ChackUserTokenExprationTimeValid(string tokenExpration)
        {
            if (DateTime.TryParseExact(tokenExpration, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
            {
                return true;
            }
            throw new Exception("Lütfen geçerli bir tarih ve saat formatında giriş yapınız (YYYY-MM-DD HH:MM:SS).");

        }



        public async Task<int> UsersCount()
        {
            return _repo.dbContext.Users.Count();
        }

    }
}
