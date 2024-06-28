using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MEB_ARHUD_Calibration.Logic;

namespace MEB_ARHUD_Calibration
{
    public partial class Form_Login : Form
    {
        public Form_Login()
        {
            InitializeComponent();
        }

        UserLogic uL = UserLogic.GetInstance();

        private void Button_Login_Click(object sender, EventArgs e)
        {
            string userAccount = TextBox_UserAccount.Text;
            string userPassword = TextBox_UserPassword.Text;

            if (Login(userAccount, userPassword))
            {
                LoginFormCloseWithSuccess();
            }
        }

        private bool Login(string userName, string password)
        {
            return true;
            LoginResult loginResult = uL.Login(userName, password);
            switch (loginResult)
            {
                case LoginResult.UnknowFail:
                    MessageBox.Show("登录信息异常");
                    break;
                case LoginResult.Success:
                    //MessageBox.Show("登录成功");
                    return true;
                case LoginResult.UserNotFound:
                    MessageBox.Show("用户不存在");
                    break;
                case LoginResult.PasswordError:
                    MessageBox.Show("密码错误");
                    break;
                case LoginResult.UserTypeError:
                    MessageBox.Show("用户类型错误");
                    break;
                default:
                    break;
            }
            return false;
        }

        private void LoginFormCloseWithSuccess()
        {
            

            this.DialogResult = DialogResult.OK;
        }
    }
}
