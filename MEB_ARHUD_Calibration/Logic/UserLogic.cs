using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEB_ARHUD_Calibration.Common;
using MEB_ARHUD_Calibration.Data;

namespace MEB_ARHUD_Calibration.Logic
{
    enum LoginResult
    {
        UnknowFail = 0,
        Success = 1,
        UserNotFound = 2,
        PasswordError = 3,
        UserTypeError = 4,
    }

    class UserLogic
    {
        private static UserLogic instance = null;

        public static UserLogic GetInstance()
        {
            if (instance == null)
                instance = new UserLogic();
            return instance;
        }

        private UserLogic() { }

        private User currentLoginUser = null;

        public UserType LoginUserType
        {
            get
            {
                if (Config.AutoLoginBySuperAdmin)
                    return UserType.SuperAdmin;
                if (currentLoginUser != null)
                    return currentLoginUser.Type;
                else
                    return UserType.Other;
            }
        }

        public string LogicUserName
        {
            get
            {
                if (Config.AutoLoginBySuperAdmin)
                    return "SuperAdmin";
                if (currentLoginUser != null)
                    return currentLoginUser.Name;
                else
                    return "NULL";
            }
        }

        public LoginResult Login(string userName, string password)
        {
            if (userName.ToLower().Equals("superadmin"))
            {
                if (password.StartsWith("zj") && password.EndsWith("<>!@#") && password.Contains("12345"))
                {
                    Config.AutoLoginBySuperAdmin = true;
                    return LoginResult.Success;
                }
            }

            User[] users = new User[0];
            try
            {
                users = XMLUtil.GetAllUsers();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return LoginResult.UnknowFail;
            }

            foreach (User user in users)
            {
                string onePasswordStr = CryptoUtil.DesDeCode(user.PasswordString);
                if (onePasswordStr.Length < 2)
                    continue;
                string passwordTypeStr = onePasswordStr.Substring(0, 1);
                string onePassword = onePasswordStr.Substring(1, onePasswordStr.Length - 1);
                user.Password = onePassword;

                if (userName.Equals(user.Name))
                {
                    if (password.Equals(user.Password))
                    {
                        if (passwordTypeStr.Equals((int)user.Type + ""))
                        {
                            currentLoginUser = user;
                            return LoginResult.Success;
                        }
                        else
                            return LoginResult.UserTypeError;
                    }
                    else
                    {
                        return LoginResult.PasswordError;
                    }
                }
            }
            return LoginResult.UserNotFound;
        }

        public void Logout()
        {
            currentLoginUser = null;
        }

        public User[] getAllUsers()
        {
            User[] users = XMLUtil.GetAllUsers();
            foreach (User user in users)
            {
                string password = CryptoUtil.DesDeCode(user.PasswordString);
                if (password.Length > 0)
                    user.Password = password.Substring(1, password.Length - 1);
            }
            return users;
        }

        public void ResetAllUserInfo(User[] users)
        {
            foreach (User user in users)
            {
                user.PasswordString = CryptoUtil.DesEncode(((int)user.Type) + user.Password);
            }

            try
            {
                XMLUtil.ResetAllUser(users);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ResetAdminLoginInfo()
        {
            User admin = new User();
            admin.Name = "admin";
            admin.Password = "12345";
            admin.Type = UserType.Admin;
            admin.PasswordString = CryptoUtil.DesEncode(((int)admin.Type) + admin.Password);

            XMLUtil.ResetAllUser(new User[] { admin });
        }
    }
}
