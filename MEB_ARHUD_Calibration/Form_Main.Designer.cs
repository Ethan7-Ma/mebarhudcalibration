namespace MEB_ARHUD_Calibration
{
    partial class Form_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.Panel_Top = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.Label_ProjectType = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iD3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iD4XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iD6XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aUDIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标定ID3相机ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标定ID4X相机ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标定ID6X相机ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标定AUDI相机ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.偏差ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ShowOffset = new System.Windows.Forms.ToolStripMenuItem();
            this.偏差设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fIS窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.需做车型ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iD3ToolStripMenuItem_NeedTest = new System.Windows.Forms.ToolStripMenuItem();
            this.iD4XToolStripMenuItem_NeedTest = new System.Windows.Forms.ToolStripMenuItem();
            this.iD6XToolStripMenuItem_NeedTest = new System.Windows.Forms.ToolStripMenuItem();
            this.aUDIToolStripMenuItem_NeedTest = new System.Windows.Forms.ToolStripMenuItem();
            this.Label_Title = new System.Windows.Forms.Label();
            this.Label_VIN = new System.Windows.Forms.Label();
            this.Label_VIN_Title = new System.Windows.Forms.Label();
            this.StatusStrip_State = new System.Windows.Forms.StatusStrip();
            this.ToolStripStatusLabel_TestState = new System.Windows.Forms.ToolStripStatusLabel();
            this.Panel_Bottom = new System.Windows.Forms.Panel();
            this.Panel_Right = new System.Windows.Forms.Panel();
            this.GroupBox_Right = new System.Windows.Forms.GroupBox();
            this.Label_NextCar = new System.Windows.Forms.Label();
            this.Label_Camera_State3 = new System.Windows.Forms.Label();
            this.Label_Camera_State2 = new System.Windows.Forms.Label();
            this.Label_FIS_State = new System.Windows.Forms.Label();
            this.Label_PLC_State = new System.Windows.Forms.Label();
            this.Label_Equipment_State = new System.Windows.Forms.Label();
            this.Label_Camera_State1 = new System.Windows.Forms.Label();
            this.PictureBox_Main = new System.Windows.Forms.PictureBox();
            this.Panel_Left = new System.Windows.Forms.Panel();
            this.GroupBox_Left = new System.Windows.Forms.GroupBox();
            this.Label_Result_Title = new System.Windows.Forms.Label();
            this.Label_Result = new System.Windows.Forms.Label();
            this.Label_EquipmentB_Title = new System.Windows.Forms.Label();
            this.Label_EquipmentA = new System.Windows.Forms.Label();
            this.Label_Car_Title = new System.Windows.Forms.Label();
            this.Label_EquipmentB = new System.Windows.Forms.Label();
            this.Label_EquipmentA_Title = new System.Windows.Forms.Label();
            this.Label_CarType = new System.Windows.Forms.Label();
            this.Label_Rotation_Title = new System.Windows.Forms.Label();
            this.Label_Rotation = new System.Windows.Forms.Label();
            this.Timer_CheckState = new System.Windows.Forms.Timer(this.components);
            this.Timer_CheckPLC = new System.Windows.Forms.Timer(this.components);
            this.Panel_Top.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.StatusStrip_State.SuspendLayout();
            this.Panel_Bottom.SuspendLayout();
            this.Panel_Right.SuspendLayout();
            this.GroupBox_Right.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Main)).BeginInit();
            this.Panel_Left.SuspendLayout();
            this.GroupBox_Left.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Top
            // 
            this.Panel_Top.Controls.Add(this.button1);
            this.Panel_Top.Controls.Add(this.Label_ProjectType);
            this.Panel_Top.Controls.Add(this.menuStrip1);
            this.Panel_Top.Controls.Add(this.Label_Title);
            this.Panel_Top.Controls.Add(this.Label_VIN);
            this.Panel_Top.Controls.Add(this.Label_VIN_Title);
            this.Panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Top.Location = new System.Drawing.Point(0, 0);
            this.Panel_Top.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.Panel_Top.Name = "Panel_Top";
            this.Panel_Top.Size = new System.Drawing.Size(1184, 179);
            this.Panel_Top.TabIndex = 18;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(912, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 66);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Label_ProjectType
            // 
            this.Label_ProjectType.AutoSize = true;
            this.Label_ProjectType.Font = new System.Drawing.Font("微软雅黑", 60F);
            this.Label_ProjectType.ForeColor = System.Drawing.Color.RoyalBlue;
            this.Label_ProjectType.Location = new System.Drawing.Point(10, 23);
            this.Label_ProjectType.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label_ProjectType.Name = "Label_ProjectType";
            this.Label_ProjectType.Size = new System.Drawing.Size(176, 104);
            this.Label_ProjectType.TabIndex = 5;
            this.Label_ProjectType.Text = "ID3";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.配置ToolStripMenuItem,
            this.标定ToolStripMenuItem,
            this.偏差ToolStripMenuItem,
            this.测试ToolStripMenuItem,
            this.需做车型ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iD3ToolStripMenuItem,
            this.iD4XToolStripMenuItem,
            this.iD6XToolStripMenuItem,
            this.aUDIToolStripMenuItem});
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.配置ToolStripMenuItem.Text = "配置";
            // 
            // iD3ToolStripMenuItem
            // 
            this.iD3ToolStripMenuItem.Name = "iD3ToolStripMenuItem";
            this.iD3ToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.iD3ToolStripMenuItem.Text = "ID3";
            this.iD3ToolStripMenuItem.Click += new System.EventHandler(this.iD3ToolStripMenuItem_Click);
            // 
            // iD4XToolStripMenuItem
            // 
            this.iD4XToolStripMenuItem.Name = "iD4XToolStripMenuItem";
            this.iD4XToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.iD4XToolStripMenuItem.Text = "ID4X";
            this.iD4XToolStripMenuItem.Click += new System.EventHandler(this.iD4XToolStripMenuItem_Click);
            // 
            // iD6XToolStripMenuItem
            // 
            this.iD6XToolStripMenuItem.Name = "iD6XToolStripMenuItem";
            this.iD6XToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.iD6XToolStripMenuItem.Text = "ID6X";
            this.iD6XToolStripMenuItem.Click += new System.EventHandler(this.iD6XToolStripMenuItem_Click);
            // 
            // aUDIToolStripMenuItem
            // 
            this.aUDIToolStripMenuItem.Name = "aUDIToolStripMenuItem";
            this.aUDIToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.aUDIToolStripMenuItem.Text = "AUDI";
            this.aUDIToolStripMenuItem.Click += new System.EventHandler(this.aUDIToolStripMenuItem_Click);
            // 
            // 标定ToolStripMenuItem
            // 
            this.标定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.标定ID3相机ToolStripMenuItem,
            this.标定ID4X相机ToolStripMenuItem,
            this.标定ID6X相机ToolStripMenuItem,
            this.标定AUDI相机ToolStripMenuItem});
            this.标定ToolStripMenuItem.Name = "标定ToolStripMenuItem";
            this.标定ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.标定ToolStripMenuItem.Text = "标定";
            // 
            // 标定ID3相机ToolStripMenuItem
            // 
            this.标定ID3相机ToolStripMenuItem.Name = "标定ID3相机ToolStripMenuItem";
            this.标定ID3相机ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.标定ID3相机ToolStripMenuItem.Text = "标定ID3相机";
            this.标定ID3相机ToolStripMenuItem.Click += new System.EventHandler(this.标定ID3相机ToolStripMenuItem_Click);
            // 
            // 标定ID4X相机ToolStripMenuItem
            // 
            this.标定ID4X相机ToolStripMenuItem.Name = "标定ID4X相机ToolStripMenuItem";
            this.标定ID4X相机ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.标定ID4X相机ToolStripMenuItem.Text = "标定ID4X相机";
            this.标定ID4X相机ToolStripMenuItem.Click += new System.EventHandler(this.标定ID4X相机ToolStripMenuItem_Click);
            // 
            // 标定ID6X相机ToolStripMenuItem
            // 
            this.标定ID6X相机ToolStripMenuItem.Name = "标定ID6X相机ToolStripMenuItem";
            this.标定ID6X相机ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.标定ID6X相机ToolStripMenuItem.Text = "标定ID6X相机";
            this.标定ID6X相机ToolStripMenuItem.Click += new System.EventHandler(this.标定ID6X相机ToolStripMenuItem_Click);
            // 
            // 标定AUDI相机ToolStripMenuItem
            // 
            this.标定AUDI相机ToolStripMenuItem.Name = "标定AUDI相机ToolStripMenuItem";
            this.标定AUDI相机ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.标定AUDI相机ToolStripMenuItem.Text = "标定AUDI相机";
            this.标定AUDI相机ToolStripMenuItem.Click += new System.EventHandler(this.标定AUDI相机ToolStripMenuItem_Click);
            // 
            // 偏差ToolStripMenuItem
            // 
            this.偏差ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_ShowOffset,
            this.偏差设置ToolStripMenuItem});
            this.偏差ToolStripMenuItem.Name = "偏差ToolStripMenuItem";
            this.偏差ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.偏差ToolStripMenuItem.Text = "偏差";
            this.偏差ToolStripMenuItem.Visible = false;
            // 
            // ToolStripMenuItem_ShowOffset
            // 
            this.ToolStripMenuItem_ShowOffset.Name = "ToolStripMenuItem_ShowOffset";
            this.ToolStripMenuItem_ShowOffset.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_ShowOffset.Text = "隐藏偏差";
            this.ToolStripMenuItem_ShowOffset.Click += new System.EventHandler(this.ToolStripMenuItem_ShowOffset_Click);
            // 
            // 偏差设置ToolStripMenuItem
            // 
            this.偏差设置ToolStripMenuItem.Name = "偏差设置ToolStripMenuItem";
            this.偏差设置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.偏差设置ToolStripMenuItem.Text = "偏差设置";
            this.偏差设置ToolStripMenuItem.Click += new System.EventHandler(this.偏差设置ToolStripMenuItem_Click);
            // 
            // 测试ToolStripMenuItem
            // 
            this.测试ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.测试窗口ToolStripMenuItem,
            this.fIS窗口ToolStripMenuItem});
            this.测试ToolStripMenuItem.Name = "测试ToolStripMenuItem";
            this.测试ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.测试ToolStripMenuItem.Text = "测试";
            // 
            // 测试窗口ToolStripMenuItem
            // 
            this.测试窗口ToolStripMenuItem.Name = "测试窗口ToolStripMenuItem";
            this.测试窗口ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.测试窗口ToolStripMenuItem.Text = "测试窗口";
            this.测试窗口ToolStripMenuItem.Click += new System.EventHandler(this.测试窗口ToolStripMenuItem_Click);
            // 
            // fIS窗口ToolStripMenuItem
            // 
            this.fIS窗口ToolStripMenuItem.Name = "fIS窗口ToolStripMenuItem";
            this.fIS窗口ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fIS窗口ToolStripMenuItem.Text = "FIS窗口";
            this.fIS窗口ToolStripMenuItem.Click += new System.EventHandler(this.fIS窗口ToolStripMenuItem_Click);
            // 
            // 需做车型ToolStripMenuItem
            // 
            this.需做车型ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iD3ToolStripMenuItem_NeedTest,
            this.iD4XToolStripMenuItem_NeedTest,
            this.iD6XToolStripMenuItem_NeedTest,
            this.aUDIToolStripMenuItem_NeedTest});
            this.需做车型ToolStripMenuItem.Name = "需做车型ToolStripMenuItem";
            this.需做车型ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.需做车型ToolStripMenuItem.Text = "需做车型";
            // 
            // iD3ToolStripMenuItem_NeedTest
            // 
            this.iD3ToolStripMenuItem_NeedTest.Name = "iD3ToolStripMenuItem_NeedTest";
            this.iD3ToolStripMenuItem_NeedTest.Size = new System.Drawing.Size(106, 22);
            this.iD3ToolStripMenuItem_NeedTest.Text = "ID3";
            this.iD3ToolStripMenuItem_NeedTest.Click += new System.EventHandler(this.iD3ToolStripMenuItem_NeedTest_Click);
            // 
            // iD4XToolStripMenuItem_NeedTest
            // 
            this.iD4XToolStripMenuItem_NeedTest.Name = "iD4XToolStripMenuItem_NeedTest";
            this.iD4XToolStripMenuItem_NeedTest.Size = new System.Drawing.Size(106, 22);
            this.iD4XToolStripMenuItem_NeedTest.Text = "ID4X";
            this.iD4XToolStripMenuItem_NeedTest.Click += new System.EventHandler(this.iD4XToolStripMenuItem_NeedTest_Click);
            // 
            // iD6XToolStripMenuItem_NeedTest
            // 
            this.iD6XToolStripMenuItem_NeedTest.Name = "iD6XToolStripMenuItem_NeedTest";
            this.iD6XToolStripMenuItem_NeedTest.Size = new System.Drawing.Size(106, 22);
            this.iD6XToolStripMenuItem_NeedTest.Text = "ID6X";
            this.iD6XToolStripMenuItem_NeedTest.Click += new System.EventHandler(this.iD6XToolStripMenuItem_NeedTest_Click);
            // 
            // aUDIToolStripMenuItem_NeedTest
            // 
            this.aUDIToolStripMenuItem_NeedTest.Name = "aUDIToolStripMenuItem_NeedTest";
            this.aUDIToolStripMenuItem_NeedTest.Size = new System.Drawing.Size(106, 22);
            this.aUDIToolStripMenuItem_NeedTest.Text = "AUDI";
            this.aUDIToolStripMenuItem_NeedTest.Click += new System.EventHandler(this.aUDIToolStripMenuItem_NeedTest_Click);
            // 
            // Label_Title
            // 
            this.Label_Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Title.Font = new System.Drawing.Font("微软雅黑", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Title.ForeColor = System.Drawing.Color.Black;
            this.Label_Title.Location = new System.Drawing.Point(12, 23);
            this.Label_Title.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(1161, 60);
            this.Label_Title.TabIndex = 0;
            this.Label_Title.Text = "ARHUD调整设备";
            this.Label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_VIN
            // 
            this.Label_VIN.AutoSize = true;
            this.Label_VIN.Font = new System.Drawing.Font("微软雅黑", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_VIN.ForeColor = System.Drawing.Color.RoyalBlue;
            this.Label_VIN.Location = new System.Drawing.Point(312, 87);
            this.Label_VIN.Name = "Label_VIN";
            this.Label_VIN.Size = new System.Drawing.Size(530, 88);
            this.Label_VIN.TabIndex = 3;
            this.Label_VIN.Text = "-----------------";
            // 
            // Label_VIN_Title
            // 
            this.Label_VIN_Title.AutoSize = true;
            this.Label_VIN_Title.Font = new System.Drawing.Font("微软雅黑", 30F);
            this.Label_VIN_Title.Location = new System.Drawing.Point(151, 116);
            this.Label_VIN_Title.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label_VIN_Title.Name = "Label_VIN_Title";
            this.Label_VIN_Title.Size = new System.Drawing.Size(151, 52);
            this.Label_VIN_Title.TabIndex = 2;
            this.Label_VIN_Title.Text = "RFID：";
            // 
            // StatusStrip_State
            // 
            this.StatusStrip_State.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel_TestState});
            this.StatusStrip_State.Location = new System.Drawing.Point(0, 726);
            this.StatusStrip_State.Name = "StatusStrip_State";
            this.StatusStrip_State.Size = new System.Drawing.Size(1184, 35);
            this.StatusStrip_State.TabIndex = 19;
            this.StatusStrip_State.Text = "statusStrip1";
            // 
            // ToolStripStatusLabel_TestState
            // 
            this.ToolStripStatusLabel_TestState.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F);
            this.ToolStripStatusLabel_TestState.Name = "ToolStripStatusLabel_TestState";
            this.ToolStripStatusLabel_TestState.Size = new System.Drawing.Size(57, 30);
            this.ToolStripStatusLabel_TestState.Text = "状态";
            // 
            // Panel_Bottom
            // 
            this.Panel_Bottom.Controls.Add(this.Panel_Right);
            this.Panel_Bottom.Controls.Add(this.Panel_Left);
            this.Panel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Bottom.Location = new System.Drawing.Point(0, 179);
            this.Panel_Bottom.Name = "Panel_Bottom";
            this.Panel_Bottom.Size = new System.Drawing.Size(1184, 547);
            this.Panel_Bottom.TabIndex = 20;
            // 
            // Panel_Right
            // 
            this.Panel_Right.Controls.Add(this.GroupBox_Right);
            this.Panel_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Right.Location = new System.Drawing.Point(446, 0);
            this.Panel_Right.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.Panel_Right.Name = "Panel_Right";
            this.Panel_Right.Size = new System.Drawing.Size(738, 547);
            this.Panel_Right.TabIndex = 2;
            // 
            // GroupBox_Right
            // 
            this.GroupBox_Right.Controls.Add(this.Label_NextCar);
            this.GroupBox_Right.Controls.Add(this.Label_Camera_State3);
            this.GroupBox_Right.Controls.Add(this.Label_Camera_State2);
            this.GroupBox_Right.Controls.Add(this.Label_FIS_State);
            this.GroupBox_Right.Controls.Add(this.Label_PLC_State);
            this.GroupBox_Right.Controls.Add(this.Label_Equipment_State);
            this.GroupBox_Right.Controls.Add(this.Label_Camera_State1);
            this.GroupBox_Right.Controls.Add(this.PictureBox_Main);
            this.GroupBox_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox_Right.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GroupBox_Right.Location = new System.Drawing.Point(0, 0);
            this.GroupBox_Right.Name = "GroupBox_Right";
            this.GroupBox_Right.Size = new System.Drawing.Size(738, 547);
            this.GroupBox_Right.TabIndex = 1;
            this.GroupBox_Right.TabStop = false;
            // 
            // Label_NextCar
            // 
            this.Label_NextCar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_NextCar.AutoSize = true;
            this.Label_NextCar.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_NextCar.Location = new System.Drawing.Point(6, 515);
            this.Label_NextCar.Name = "Label_NextCar";
            this.Label_NextCar.Size = new System.Drawing.Size(109, 25);
            this.Label_NextCar.TabIndex = 5;
            this.Label_NextCar.Text = "Next Car : ";
            // 
            // Label_Camera_State3
            // 
            this.Label_Camera_State3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Camera_State3.AutoSize = true;
            this.Label_Camera_State3.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Camera_State3.ForeColor = System.Drawing.Color.Green;
            this.Label_Camera_State3.Location = new System.Drawing.Point(595, 515);
            this.Label_Camera_State3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label_Camera_State3.Name = "Label_Camera_State3";
            this.Label_Camera_State3.Size = new System.Drawing.Size(137, 25);
            this.Label_Camera_State3.TabIndex = 19;
            this.Label_Camera_State3.Text = "相机3连接正常";
            // 
            // Label_Camera_State2
            // 
            this.Label_Camera_State2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Camera_State2.AutoSize = true;
            this.Label_Camera_State2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Camera_State2.ForeColor = System.Drawing.Color.Green;
            this.Label_Camera_State2.Location = new System.Drawing.Point(595, 490);
            this.Label_Camera_State2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label_Camera_State2.Name = "Label_Camera_State2";
            this.Label_Camera_State2.Size = new System.Drawing.Size(137, 25);
            this.Label_Camera_State2.TabIndex = 18;
            this.Label_Camera_State2.Text = "相机2连接正常";
            // 
            // Label_FIS_State
            // 
            this.Label_FIS_State.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_FIS_State.AutoSize = true;
            this.Label_FIS_State.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_FIS_State.ForeColor = System.Drawing.Color.Red;
            this.Label_FIS_State.Location = new System.Drawing.Point(617, 390);
            this.Label_FIS_State.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label_FIS_State.Name = "Label_FIS_State";
            this.Label_FIS_State.Size = new System.Drawing.Size(115, 25);
            this.Label_FIS_State.TabIndex = 17;
            this.Label_FIS_State.Text = "FIS连接正常";
            // 
            // Label_PLC_State
            // 
            this.Label_PLC_State.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_PLC_State.AutoSize = true;
            this.Label_PLC_State.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_PLC_State.ForeColor = System.Drawing.Color.Green;
            this.Label_PLC_State.Location = new System.Drawing.Point(609, 415);
            this.Label_PLC_State.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label_PLC_State.Name = "Label_PLC_State";
            this.Label_PLC_State.Size = new System.Drawing.Size(123, 25);
            this.Label_PLC_State.TabIndex = 16;
            this.Label_PLC_State.Text = "PLC连接正常";
            // 
            // Label_Equipment_State
            // 
            this.Label_Equipment_State.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Equipment_State.AutoSize = true;
            this.Label_Equipment_State.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Equipment_State.ForeColor = System.Drawing.Color.Green;
            this.Label_Equipment_State.Location = new System.Drawing.Point(587, 440);
            this.Label_Equipment_State.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label_Equipment_State.Name = "Label_Equipment_State";
            this.Label_Equipment_State.Size = new System.Drawing.Size(145, 25);
            this.Label_Equipment_State.TabIndex = 15;
            this.Label_Equipment_State.Text = "拧紧枪连接正常";
            // 
            // Label_Camera_State1
            // 
            this.Label_Camera_State1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Camera_State1.AutoSize = true;
            this.Label_Camera_State1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Camera_State1.ForeColor = System.Drawing.Color.Green;
            this.Label_Camera_State1.Location = new System.Drawing.Point(595, 465);
            this.Label_Camera_State1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label_Camera_State1.Name = "Label_Camera_State1";
            this.Label_Camera_State1.Size = new System.Drawing.Size(137, 25);
            this.Label_Camera_State1.TabIndex = 14;
            this.Label_Camera_State1.Text = "相机1连接正常";
            // 
            // PictureBox_Main
            // 
            this.PictureBox_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox_Main.Location = new System.Drawing.Point(6, 21);
            this.PictureBox_Main.Name = "PictureBox_Main";
            this.PictureBox_Main.Size = new System.Drawing.Size(726, 347);
            this.PictureBox_Main.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox_Main.TabIndex = 0;
            this.PictureBox_Main.TabStop = false;
            // 
            // Panel_Left
            // 
            this.Panel_Left.Controls.Add(this.GroupBox_Left);
            this.Panel_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel_Left.Location = new System.Drawing.Point(0, 0);
            this.Panel_Left.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.Panel_Left.Name = "Panel_Left";
            this.Panel_Left.Size = new System.Drawing.Size(446, 547);
            this.Panel_Left.TabIndex = 1;
            // 
            // GroupBox_Left
            // 
            this.GroupBox_Left.Controls.Add(this.Label_Result_Title);
            this.GroupBox_Left.Controls.Add(this.Label_Result);
            this.GroupBox_Left.Controls.Add(this.Label_EquipmentB_Title);
            this.GroupBox_Left.Controls.Add(this.Label_EquipmentA);
            this.GroupBox_Left.Controls.Add(this.Label_Car_Title);
            this.GroupBox_Left.Controls.Add(this.Label_EquipmentB);
            this.GroupBox_Left.Controls.Add(this.Label_EquipmentA_Title);
            this.GroupBox_Left.Controls.Add(this.Label_CarType);
            this.GroupBox_Left.Controls.Add(this.Label_Rotation_Title);
            this.GroupBox_Left.Controls.Add(this.Label_Rotation);
            this.GroupBox_Left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBox_Left.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GroupBox_Left.Location = new System.Drawing.Point(0, 0);
            this.GroupBox_Left.Name = "GroupBox_Left";
            this.GroupBox_Left.Size = new System.Drawing.Size(446, 547);
            this.GroupBox_Left.TabIndex = 14;
            this.GroupBox_Left.TabStop = false;
            // 
            // Label_Result_Title
            // 
            this.Label_Result_Title.AutoSize = true;
            this.Label_Result_Title.Font = new System.Drawing.Font("微软雅黑", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Result_Title.Location = new System.Drawing.Point(30, 47);
            this.Label_Result_Title.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label_Result_Title.Name = "Label_Result_Title";
            this.Label_Result_Title.Size = new System.Drawing.Size(222, 52);
            this.Label_Result_Title.TabIndex = 12;
            this.Label_Result_Title.Text = "测试结果：";
            // 
            // Label_Result
            // 
            this.Label_Result.AutoSize = true;
            this.Label_Result.Font = new System.Drawing.Font("微软雅黑", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Result.ForeColor = System.Drawing.Color.Green;
            this.Label_Result.Location = new System.Drawing.Point(21, 114);
            this.Label_Result.Name = "Label_Result";
            this.Label_Result.Size = new System.Drawing.Size(115, 106);
            this.Label_Result.TabIndex = 13;
            this.Label_Result.Text = "--";
            // 
            // Label_EquipmentB_Title
            // 
            this.Label_EquipmentB_Title.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_EquipmentB_Title.AutoSize = true;
            this.Label_EquipmentB_Title.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_EquipmentB_Title.Location = new System.Drawing.Point(33, 380);
            this.Label_EquipmentB_Title.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label_EquipmentB_Title.Name = "Label_EquipmentB_Title";
            this.Label_EquipmentB_Title.Size = new System.Drawing.Size(194, 35);
            this.Label_EquipmentB_Title.TabIndex = 6;
            this.Label_EquipmentB_Title.Text = "拧紧枪B角度：";
            // 
            // Label_EquipmentA
            // 
            this.Label_EquipmentA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_EquipmentA.AutoSize = true;
            this.Label_EquipmentA.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_EquipmentA.ForeColor = System.Drawing.Color.RoyalBlue;
            this.Label_EquipmentA.Location = new System.Drawing.Point(239, 317);
            this.Label_EquipmentA.Name = "Label_EquipmentA";
            this.Label_EquipmentA.Size = new System.Drawing.Size(51, 35);
            this.Label_EquipmentA.TabIndex = 5;
            this.Label_EquipmentA.Text = "---";
            // 
            // Label_Car_Title
            // 
            this.Label_Car_Title.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_Car_Title.AutoSize = true;
            this.Label_Car_Title.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Car_Title.Location = new System.Drawing.Point(32, 258);
            this.Label_Car_Title.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label_Car_Title.Name = "Label_Car_Title";
            this.Label_Car_Title.Size = new System.Drawing.Size(150, 35);
            this.Label_Car_Title.TabIndex = 0;
            this.Label_Car_Title.Text = "当前车型：";
            // 
            // Label_EquipmentB
            // 
            this.Label_EquipmentB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_EquipmentB.AutoSize = true;
            this.Label_EquipmentB.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_EquipmentB.ForeColor = System.Drawing.Color.RoyalBlue;
            this.Label_EquipmentB.Location = new System.Drawing.Point(239, 380);
            this.Label_EquipmentB.Name = "Label_EquipmentB";
            this.Label_EquipmentB.Size = new System.Drawing.Size(51, 35);
            this.Label_EquipmentB.TabIndex = 7;
            this.Label_EquipmentB.Text = "---";
            // 
            // Label_EquipmentA_Title
            // 
            this.Label_EquipmentA_Title.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_EquipmentA_Title.AutoSize = true;
            this.Label_EquipmentA_Title.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_EquipmentA_Title.Location = new System.Drawing.Point(33, 317);
            this.Label_EquipmentA_Title.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label_EquipmentA_Title.Name = "Label_EquipmentA_Title";
            this.Label_EquipmentA_Title.Size = new System.Drawing.Size(196, 35);
            this.Label_EquipmentA_Title.TabIndex = 4;
            this.Label_EquipmentA_Title.Text = "拧紧枪A角度：";
            // 
            // Label_CarType
            // 
            this.Label_CarType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_CarType.AutoSize = true;
            this.Label_CarType.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_CarType.ForeColor = System.Drawing.Color.RoyalBlue;
            this.Label_CarType.Location = new System.Drawing.Point(238, 258);
            this.Label_CarType.Name = "Label_CarType";
            this.Label_CarType.Size = new System.Drawing.Size(51, 35);
            this.Label_CarType.TabIndex = 1;
            this.Label_CarType.Text = "---";
            // 
            // Label_Rotation_Title
            // 
            this.Label_Rotation_Title.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_Rotation_Title.AutoSize = true;
            this.Label_Rotation_Title.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Rotation_Title.Location = new System.Drawing.Point(33, 441);
            this.Label_Rotation_Title.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label_Rotation_Title.Name = "Label_Rotation_Title";
            this.Label_Rotation_Title.Size = new System.Drawing.Size(157, 35);
            this.Label_Rotation_Title.TabIndex = 8;
            this.Label_Rotation_Title.Text = "图像旋转角:";
            // 
            // Label_Rotation
            // 
            this.Label_Rotation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_Rotation.AutoSize = true;
            this.Label_Rotation.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Rotation.ForeColor = System.Drawing.Color.Green;
            this.Label_Rotation.Location = new System.Drawing.Point(239, 441);
            this.Label_Rotation.Name = "Label_Rotation";
            this.Label_Rotation.Size = new System.Drawing.Size(42, 35);
            this.Label_Rotation.TabIndex = 9;
            this.Label_Rotation.Text = "0°";
            // 
            // Timer_CheckState
            // 
            this.Timer_CheckState.Enabled = true;
            this.Timer_CheckState.Interval = 1000;
            this.Timer_CheckState.Tick += new System.EventHandler(this.Timer_CheckState_Tick);
            // 
            // Timer_CheckPLC
            // 
            this.Timer_CheckPLC.Enabled = true;
            this.Timer_CheckPLC.Interval = 2000;
            this.Timer_CheckPLC.Tick += new System.EventHandler(this.Timer_CheckPLC_Tick);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.Panel_Bottom);
            this.Controls.Add(this.StatusStrip_State);
            this.Controls.Add(this.Panel_Top);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.Text = "ARHUD";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Main_FormClosed);
            this.Shown += new System.EventHandler(this.Form_Main_Shown);
            this.Panel_Top.ResumeLayout(false);
            this.Panel_Top.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.StatusStrip_State.ResumeLayout(false);
            this.StatusStrip_State.PerformLayout();
            this.Panel_Bottom.ResumeLayout(false);
            this.Panel_Right.ResumeLayout(false);
            this.GroupBox_Right.ResumeLayout(false);
            this.GroupBox_Right.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Main)).EndInit();
            this.Panel_Left.ResumeLayout(false);
            this.GroupBox_Left.ResumeLayout(false);
            this.GroupBox_Left.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Top;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.Label Label_VIN;
        private System.Windows.Forms.Label Label_VIN_Title;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip StatusStrip_State;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel_TestState;
        private System.Windows.Forms.Panel Panel_Bottom;
        private System.Windows.Forms.Panel Panel_Right;
        private System.Windows.Forms.GroupBox GroupBox_Right;
        private System.Windows.Forms.Label Label_FIS_State;
        private System.Windows.Forms.Label Label_PLC_State;
        private System.Windows.Forms.Label Label_Equipment_State;
        private System.Windows.Forms.Label Label_Camera_State1;
        private System.Windows.Forms.PictureBox PictureBox_Main;
        private System.Windows.Forms.Panel Panel_Left;
        private System.Windows.Forms.GroupBox GroupBox_Left;
        private System.Windows.Forms.Label Label_Result_Title;
        private System.Windows.Forms.Label Label_Result;
        private System.Windows.Forms.Label Label_EquipmentB_Title;
        private System.Windows.Forms.Label Label_EquipmentA;
        private System.Windows.Forms.Label Label_Car_Title;
        private System.Windows.Forms.Label Label_EquipmentB;
        private System.Windows.Forms.Label Label_EquipmentA_Title;
        private System.Windows.Forms.Label Label_CarType;
        private System.Windows.Forms.Label Label_Rotation_Title;
        private System.Windows.Forms.Label Label_Rotation;
        private System.Windows.Forms.Label Label_Camera_State2;
        private System.Windows.Forms.Label Label_Camera_State3;
        private System.Windows.Forms.Timer Timer_CheckState;
        private System.Windows.Forms.Timer Timer_CheckPLC;
        private System.Windows.Forms.ToolStripMenuItem iD3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iD4XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iD6XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aUDIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 标定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 标定ID3相机ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 标定ID4X相机ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 标定ID6X相机ToolStripMenuItem;
        private System.Windows.Forms.Label Label_NextCar;
        private System.Windows.Forms.ToolStripMenuItem 标定AUDI相机ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 偏差ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ShowOffset;
        private System.Windows.Forms.ToolStripMenuItem 偏差设置ToolStripMenuItem;
        private System.Windows.Forms.Label Label_ProjectType;
        private System.Windows.Forms.ToolStripMenuItem 测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 需做车型ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iD3ToolStripMenuItem_NeedTest;
        private System.Windows.Forms.ToolStripMenuItem iD4XToolStripMenuItem_NeedTest;
        private System.Windows.Forms.ToolStripMenuItem iD6XToolStripMenuItem_NeedTest;
        private System.Windows.Forms.ToolStripMenuItem aUDIToolStripMenuItem_NeedTest;
        private System.Windows.Forms.ToolStripMenuItem fIS窗口ToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}

