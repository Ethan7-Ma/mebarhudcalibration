using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MEB_ARHUD_Calibration.Data
{
    public enum UserType
    {
        Other = 0,
        SuperAdmin = 1,
        Admin = 2,
        User = 3,
    }

    class User
    {
        private string name = "";
        private string password = "";
        private UserType type = UserType.Other;
        private string passwordString = "";

        public string Name { get { return name; } set { name = value; } }
        public string Password { get { return password; } set { password = value; } }
        public UserType Type { get { return type; } set { type = value; } }
        public string PasswordString { get { return passwordString; } set { passwordString = value; } }

        public User() { }

        public User(string name, string password, UserType type)
        {
            this.name = name;
            this.password = password;
            this.type = type;
        }

        public User(string name, string password, string passwordString, UserType type)
        {
            this.name = name;
            this.password = password;
            this.passwordString = passwordString;
            this.type = type;
        }

    }
}
