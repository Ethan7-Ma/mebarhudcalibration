namespace MEB_ARHUD_Calibration
{
    partial class Form_Config
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            this.GroupBox_EquipmentConfig = new System.Windows.Forms.GroupBox();
            this.TextBox_PLC_Port = new System.Windows.Forms.TextBox();
            this.TextBox_PLC_IP = new System.Windows.Forms.TextBox();
            this.Label_PLC_IP = new System.Windows.Forms.Label();
            this.Label_PLC_Port = new System.Windows.Forms.Label();
            this.Label_PLC = new System.Windows.Forms.Label();
            this.TextBox_Equipment_Port = new System.Windows.Forms.TextBox();
            this.TextBox_Equipment_IP = new System.Windows.Forms.TextBox();
            this.Label_Equipment_IP = new System.Windows.Forms.Label();
            this.Label_Equipment_Port = new System.Windows.Forms.Label();
            this.Label_Equipment = new System.Windows.Forms.Label();
            this.GroupBox_TestConfig = new System.Windows.Forms.GroupBox();
            this.DataGridView_TestConfig = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupBox_UserConfig = new System.Windows.Forms.GroupBox();
            this.DataGridView_UserConfig = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.GroupBox_EquipmentConfig.SuspendLayout();
            this.GroupBox_TestConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_TestConfig)).BeginInit();
            this.GroupBox_UserConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_UserConfig)).BeginInit();
            this.SuspendLayout();
            this.GroupBox_EquipmentConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_EquipmentConfig.Controls.Add(this.TextBox_PLC_Port);
            this.GroupBox_EquipmentConfig.Controls.Add(this.TextBox_PLC_IP);
            this.GroupBox_EquipmentConfig.Controls.Add(this.Label_PLC_IP);
            this.GroupBox_EquipmentConfig.Controls.Add(this.Label_PLC_Port);
            this.GroupBox_EquipmentConfig.Controls.Add(this.Label_PLC);
            this.GroupBox_EquipmentConfig.Controls.Add(this.TextBox_Equipment_Port);
            this.GroupBox_EquipmentConfig.Controls.Add(this.TextBox_Equipment_IP);
            this.GroupBox_EquipmentConfig.Controls.Add(this.Label_Equipment_IP);
            this.GroupBox_EquipmentConfig.Controls.Add(this.Label_Equipment_Port);
            this.GroupBox_EquipmentConfig.Controls.Add(this.Label_Equipment);
            this.GroupBox_EquipmentConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GroupBox_EquipmentConfig.Location = new System.Drawing.Point(12, 12);
            this.GroupBox_EquipmentConfig.Name = "GroupBox_EquipmentConfig";
            this.GroupBox_EquipmentConfig.Size = new System.Drawing.Size(793, 115);
            this.GroupBox_EquipmentConfig.TabIndex = 1;
            this.GroupBox_EquipmentConfig.TabStop = false;
            this.GroupBox_EquipmentConfig.Text = "设备配置";
            
            this.TextBox_PLC_Port.Location = new System.Drawing.Point(343, 67);
            this.TextBox_PLC_Port.Name = "TextBox_PLC_Port";
            this.TextBox_PLC_Port.Size = new System.Drawing.Size(101, 26);
            this.TextBox_PLC_Port.TabIndex = 9;
            this.TextBox_PLC_Port.Text = "3000";
             
            this.TextBox_PLC_IP.Location = new System.Drawing.Point(124, 67);
            this.TextBox_PLC_IP.Name = "TextBox_PLC_IP";
            this.TextBox_PLC_IP.Size = new System.Drawing.Size(149, 26);
            this.TextBox_PLC_IP.TabIndex = 8;
            this.TextBox_PLC_IP.Text = "192.168.1.2";
            
            this.Label_PLC_IP.AutoSize = true;
            this.Label_PLC_IP.Location = new System.Drawing.Point(94, 70);
            this.Label_PLC_IP.Name = "Label_PLC_IP";
            this.Label_PLC_IP.Size = new System.Drawing.Size(24, 16);
            this.Label_PLC_IP.TabIndex = 7;
            this.Label_PLC_IP.Text = "IP";
            
            this.Label_PLC_Port.AutoSize = true;
            this.Label_PLC_Port.Location = new System.Drawing.Point(297, 70);
            this.Label_PLC_Port.Name = "Label_PLC_Port";
            this.Label_PLC_Port.Size = new System.Drawing.Size(40, 16);
            this.Label_PLC_Port.TabIndex = 6;
            this.Label_PLC_Port.Text = "Port";
            
            this.Label_PLC.AutoSize = true;
            this.Label_PLC.Location = new System.Drawing.Point(19, 70);
            this.Label_PLC.Name = "Label_PLC";
            this.Label_PLC.Size = new System.Drawing.Size(32, 16);
            this.Label_PLC.TabIndex = 5;
            this.Label_PLC.Text = "PLC";
            
            this.TextBox_Equipment_Port.Location = new System.Drawing.Point(343, 28);
            this.TextBox_Equipment_Port.Name = "TextBox_Equipment_Port";
            this.TextBox_Equipment_Port.Size = new System.Drawing.Size(101, 26);
            this.TextBox_Equipment_Port.TabIndex = 4;
            this.TextBox_Equipment_Port.Text = "2000";
             
            this.TextBox_Equipment_IP.Location = new System.Drawing.Point(124, 28);
            this.TextBox_Equipment_IP.Name = "TextBox_Equipment_IP";
            this.TextBox_Equipment_IP.Size = new System.Drawing.Size(149, 26);
            this.TextBox_Equipment_IP.TabIndex = 3;
            this.TextBox_Equipment_IP.Text = "192.168.1.1";
            
            this.Label_Equipment_IP.AutoSize = true;
            this.Label_Equipment_IP.Location = new System.Drawing.Point(94, 31);
            this.Label_Equipment_IP.Name = "Label_Equipment_IP";
            this.Label_Equipment_IP.Size = new System.Drawing.Size(24, 16);
            this.Label_Equipment_IP.TabIndex = 2;
            this.Label_Equipment_IP.Text = "IP";
            
            this.Label_Equipment_Port.AutoSize = true;
            this.Label_Equipment_Port.Location = new System.Drawing.Point(297, 31);
            this.Label_Equipment_Port.Name = "Label_Equipment_Port";
            this.Label_Equipment_Port.Size = new System.Drawing.Size(40, 16);
            this.Label_Equipment_Port.TabIndex = 1;
            this.Label_Equipment_Port.Text = "Port";
            
            this.Label_Equipment.AutoSize = true;
            this.Label_Equipment.Location = new System.Drawing.Point(19, 31);
            this.Label_Equipment.Name = "Label_Equipment";
            this.Label_Equipment.Size = new System.Drawing.Size(56, 16);
            this.Label_Equipment.TabIndex = 0;
            this.Label_Equipment.Text = "拧紧枪";
            
            this.GroupBox_TestConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_TestConfig.Controls.Add(this.DataGridView_TestConfig);
            this.GroupBox_TestConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GroupBox_TestConfig.Location = new System.Drawing.Point(12, 133);
            this.GroupBox_TestConfig.Name = "GroupBox_TestConfig";
            this.GroupBox_TestConfig.Size = new System.Drawing.Size(793, 197);
            this.GroupBox_TestConfig.TabIndex = 11;
            this.GroupBox_TestConfig.TabStop = false;
            this.GroupBox_TestConfig.Text = "测试配置";
             
            this.DataGridView_TestConfig.AllowUserToAddRows = false;
            this.DataGridView_TestConfig.AllowUserToDeleteRows = false;
            this.DataGridView_TestConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView_TestConfig.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGridView_TestConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_TestConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.DataGridView_TestConfig.Location = new System.Drawing.Point(22, 25);
            this.DataGridView_TestConfig.Name = "DataGridView_TestConfig";
            this.DataGridView_TestConfig.RowTemplate.Height = 23;
            this.DataGridView_TestConfig.Size = new System.Drawing.Size(753, 155);
            this.DataGridView_TestConfig.TabIndex = 0;
             
            this.Column1.HeaderText = "测试项";
            this.Column1.Name = "Column1";
            this.Column1.Width = 120;
             
            this.Column2.HeaderText = "最小值";
            this.Column2.Name = "Column2";
            
            this.Column3.HeaderText = "最大值";
            this.Column3.Name = "Column3";
            
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "说明";
            this.Column4.Name = "Column4";
             
            this.GroupBox_UserConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_UserConfig.Controls.Add(this.DataGridView_UserConfig);
            this.GroupBox_UserConfig.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GroupBox_UserConfig.Location = new System.Drawing.Point(12, 336);
            this.GroupBox_UserConfig.Name = "GroupBox_UserConfig";
            this.GroupBox_UserConfig.Size = new System.Drawing.Size(793, 169);
            this.GroupBox_UserConfig.TabIndex = 12;
            this.GroupBox_UserConfig.TabStop = false;
            this.GroupBox_UserConfig.Text = "用户配置";
            
            this.DataGridView_UserConfig.AllowUserToAddRows = false;
            this.DataGridView_UserConfig.AllowUserToDeleteRows = false;
            this.DataGridView_UserConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView_UserConfig.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DataGridView_UserConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_UserConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.DataGridView_UserConfig.Location = new System.Drawing.Point(22, 25);
            this.DataGridView_UserConfig.Name = "DataGridView_UserConfig";
            this.DataGridView_UserConfig.RowTemplate.Height = 23;
            this.DataGridView_UserConfig.Size = new System.Drawing.Size(753, 129);
            this.DataGridView_UserConfig.TabIndex = 0;
            
            this.dataGridViewTextBoxColumn1.HeaderText = "用户名";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 120;
            
            this.dataGridViewTextBoxColumn2.HeaderText = "密码";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            
            this.dataGridViewTextBoxColumn3.HeaderText = "权限";
            this.dataGridViewTextBoxColumn3.Items.AddRange(new object[] {
            "管理员",
            "操作工"});
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 516);
            this.Controls.Add(this.GroupBox_UserConfig);
            this.Controls.Add(this.GroupBox_TestConfig);
            this.Controls.Add(this.GroupBox_EquipmentConfig);
            this.Name = "Form_Config";
            this.Text = "配置";
            this.GroupBox_EquipmentConfig.ResumeLayout(false);
            this.GroupBox_EquipmentConfig.PerformLayout();
            this.GroupBox_TestConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_TestConfig)).EndInit();
            this.GroupBox_UserConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_UserConfig)).EndInit();
            this.ResumeLayout(false);

        }


        private System.Windows.Forms.GroupBox GroupBox_EquipmentConfig;
        private System.Windows.Forms.TextBox TextBox_PLC_Port;
        private System.Windows.Forms.TextBox TextBox_PLC_IP;
        private System.Windows.Forms.Label Label_PLC_IP;
        private System.Windows.Forms.Label Label_PLC_Port;
        private System.Windows.Forms.Label Label_PLC;
        private System.Windows.Forms.TextBox TextBox_Equipment_Port;
        private System.Windows.Forms.TextBox TextBox_Equipment_IP;
        private System.Windows.Forms.Label Label_Equipment_IP;
        private System.Windows.Forms.Label Label_Equipment_Port;
        private System.Windows.Forms.Label Label_Equipment;
        private System.Windows.Forms.GroupBox GroupBox_TestConfig;
        private System.Windows.Forms.DataGridView DataGridView_TestConfig;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.GroupBox GroupBox_UserConfig;
        private System.Windows.Forms.DataGridView DataGridView_UserConfig;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn3;
    }
}