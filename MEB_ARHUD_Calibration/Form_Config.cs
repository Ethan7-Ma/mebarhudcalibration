using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MEB_ARHUD_Calibration
{
    public partial class Form_Config : Form
    {
        public Form_Config()
        {
            InitializeComponent();
            InitTestConfig();
            InitUserConfig();
        }

        private void InitTestConfig()
        {
            DataGridView_TestConfig.Rows.Add(new string[] { "旋转角", "-1", "1", "" });
            DataGridView_TestConfig.Rows.Add(new string[] { "偏移", "-50", "50", "" });
        }

        private void InitUserConfig()
        {
            DataGridView_UserConfig.Rows.Add(new string[] { "admin", "admin12345", "管理员" });
            DataGridView_UserConfig.Rows.Add(new string[] { "user1", "12345", "操作工" });
            DataGridView_UserConfig.Rows.Add(new string[] { "user2", "12345", "操作工" });
            DataGridView_UserConfig.Rows.Add(new string[] { "user3", "12345", "操作工" });
        }
    }
}
