using MEB_ARHUD_Calibration.Common;
using MEB_ARHUD_Calibration.Models;

namespace MEB_ARHUD_Calibration.Logic {
    enum LoginResult {
        UnknowFail = 0,
        Success = 1,
        UserNotFound = 2,
        PasswordError = 3,
        UserTypeError = 4,
    }

    class UserLogic {
        private static UserLogic? instance = null;
        public static UserLogic GetInstance() => instance ??= new();

        public LoginResult Login(string userName, string password) {
            User[] users = XMLUtil.GetAllUsers();

            foreach (User user in users) {
                string onePasswordStr = CryptoUtil.DesDeCode(user.PasswordString);
                if (onePasswordStr.Length < 2)
                    continue;
                string passwordTypeStr = onePasswordStr.Substring(0, 1);
                string onePassword = onePasswordStr.Substring(1, onePasswordStr.Length - 1);
                user.Password = onePassword;

                if (userName.Equals(user.Name)) {
                    if (password.Equals(user.Password)) {
                        if (passwordTypeStr.Equals((int)user.Type + "")) {
                            return LoginResult.Success;
                        }
                        else
                            return LoginResult.UserTypeError;
                    }
                    else {
                        return LoginResult.PasswordError;
                    }
                }
            }
            return LoginResult.UserNotFound;
        }


    }
}
