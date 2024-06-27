namespace MEB_ARHUD_Calibration
{
    partial class Form_Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Button_Login = new System.Windows.Forms.Button();
            this.TextBox_UserPassword = new System.Windows.Forms.TextBox();
            this.TextBox_UserAccount = new System.Windows.Forms.TextBox();
            this.Label_UserPassword = new System.Windows.Forms.Label();
            this.Label_UserAccount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Button_Login
            // 
            this.Button_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Login.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_Login.Location = new System.Drawing.Point(137, 128);
            this.Button_Login.Name = "Button_Login";
            this.Button_Login.Size = new System.Drawing.Size(90, 35);
            this.Button_Login.TabIndex = 20;
            this.Button_Login.Text = "登 录";
            this.Button_Login.UseVisualStyleBackColor = true;
            this.Button_Login.Click += new System.EventHandler(this.Button_Login_Click);
            // 
            // TextBox_UserPassword
            // 
            this.TextBox_UserPassword.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TextBox_UserPassword.Location = new System.Drawing.Point(127, 81);
            this.TextBox_UserPassword.Name = "TextBox_UserPassword";
            this.TextBox_UserPassword.Size = new System.Drawing.Size(172, 26);
            this.TextBox_UserPassword.TabIndex = 19;
            this.TextBox_UserPassword.UseSystemPasswordChar = true;
            // 
            // TextBox_UserAccount
            // 
            this.TextBox_UserAccount.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TextBox_UserAccount.Location = new System.Drawing.Point(127, 45);
            this.TextBox_UserAccount.Name = "TextBox_UserAccount";
            this.TextBox_UserAccount.Size = new System.Drawing.Size(172, 26);
            this.TextBox_UserAccount.TabIndex = 18;
            this.TextBox_UserAccount.Text = "admin";
            // 
            // Label_UserPassword
            // 
            this.Label_UserPassword.AutoSize = true;
            this.Label_UserPassword.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_UserPassword.Location = new System.Drawing.Point(46, 78);
            this.Label_UserPassword.Name = "Label_UserPassword";
            this.Label_UserPassword.Size = new System.Drawing.Size(52, 27);
            this.Label_UserPassword.TabIndex = 17;
            this.Label_UserPassword.Text = "密码";
            // 
            // Label_UserAccount
            // 
            this.Label_UserAccount.AutoSize = true;
            this.Label_UserAccount.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_UserAccount.Location = new System.Drawing.Point(46, 42);
            this.Label_UserAccount.Name = "Label_UserAccount";
            this.Label_UserAccount.Size = new System.Drawing.Size(52, 27);
            this.Label_UserAccount.TabIndex = 16;
            this.Label_UserAccount.Text = "账户";
            // 
            // Form_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 194);
            this.Controls.Add(this.Button_Login);
            this.Controls.Add(this.TextBox_UserPassword);
            this.Controls.Add(this.TextBox_UserAccount);
            this.Controls.Add(this.Label_UserPassword);
            this.Controls.Add(this.Label_UserAccount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_Login";
            this.Text = "登录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Button_Login;
        private System.Windows.Forms.TextBox TextBox_UserPassword;
        private System.Windows.Forms.TextBox TextBox_UserAccount;
        private System.Windows.Forms.Label Label_UserPassword;
        private System.Windows.Forms.Label Label_UserAccount;
    }
}