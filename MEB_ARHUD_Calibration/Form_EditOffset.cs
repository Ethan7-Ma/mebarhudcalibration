using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MEB_ARHUD_Calibration.Common;
using MEB_ARHUD_Calibration.Logic;

namespace MEB_ARHUD_Calibration
{
    public partial class Form_EditOffset : Form
    {
        public Form_EditOffset()
        {
            InitializeComponent();
            InitOffsetValues();
        }

        ConfigLogic cfgL = ConfigLogic.GetInstance();

        private void InitOffsetValues()
        {
            TextBox_ID3_X.Text = Config.Camera_OffsetX[ProjectType.ID3] + "";
            TextBox_ID3_Y.Text = Config.Camera_OffsetY[ProjectType.ID3] + "";

            TextBox_ID4X_X.Text = Config.Camera_OffsetX[ProjectType.ID4X] + "";
            TextBox_ID4X_Y.Text = Config.Camera_OffsetY[ProjectType.ID4X] + "";

            TextBox_ID6X_X.Text = Config.Camera_OffsetX[ProjectType.ID6X] + "";
            TextBox_ID6X_Y.Text = Config.Camera_OffsetY[ProjectType.ID6X] + "";

            TextBox_AUDI_X.Text = Config.Camera_OffsetX[ProjectType.AUDI] + "";
            TextBox_AUDI_Y.Text = Config.Camera_OffsetY[ProjectType.AUDI] + "";
        }

        private void Button_SaveID3_Click(object sender, EventArgs e)
        {
            try
            {
                string X_Str = TextBox_ID3_X.Text;
                string Y_Str = TextBox_ID3_Y.Text;
                if (int.TryParse(X_Str, out int x) && int.TryParse(Y_Str, out int y))
                {
                    cfgL.SaveID3CameraOffset(x, y);
                    MessageBox.Show("保存完成");
                }
            }
            catch
            {

            }
        }

        private void Button_SaveID4X_Click(object sender, EventArgs e)
        {
            try
            {
                string X_Str = TextBox_ID4X_X.Text;
                string Y_Str = TextBox_ID4X_Y.Text;
                if (int.TryParse(X_Str, out int x) && int.TryParse(Y_Str, out int y))
                {
                    cfgL.SaveID4XCameraOffset(x, y);
                    MessageBox.Show("保存完成");
                }
            }
            catch 
            {

            }
        }

        private void Button_SaveID6X_Click(object sender, EventArgs e)
        {
            try
            {
                string X_Str = TextBox_ID6X_X.Text;
                string Y_Str = TextBox_ID6X_Y.Text;
                if (int.TryParse(X_Str, out int x) && int.TryParse(Y_Str, out int y))
                {
                    cfgL.SaveID6XCameraOffset(x, y);
                    MessageBox.Show("保存完成");
                }
            }
            catch
            {

            }
        }

        private void Button_SaveAUDI_Click(object sender, EventArgs e)
        {
            try
            {
                string X_Str = TextBox_AUDI_X.Text;
                string Y_Str = TextBox_AUDI_Y.Text;
                if (int.TryParse(X_Str, out int x) && int.TryParse(Y_Str, out int y))
                {
                    cfgL.SaveAUDICameraOffset(x, y);
                    MessageBox.Show("保存完成");
                }
            }
            catch
            {

            }
        }
    }
}
