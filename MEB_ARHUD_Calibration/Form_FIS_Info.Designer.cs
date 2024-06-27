namespace MEB_ARHUD_Calibration
{
    partial class Form_FIS_Info
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
            this.DataGridView_Infos = new System.Windows.Forms.DataGridView();
            this.Button_SelectAll = new System.Windows.Forms.Button();
            this.Button_SelectBySeq = new System.Windows.Forms.Button();
            this.Label_Start = new System.Windows.Forms.Label();
            this.Label_End = new System.Windows.Forms.Label();
            this.TextBox_Seq_Start = new System.Windows.Forms.TextBox();
            this.TextBox_Seq_End = new System.Windows.Forms.TextBox();
            this.DateTimePicker_Time = new System.Windows.Forms.DateTimePicker();
            this.Button_ExportSelect = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Button_SelectByTime = new System.Windows.Forms.Button();
            this.DateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.Label_StartTime = new System.Windows.Forms.Label();
            this.Label_EndTime = new System.Windows.Forms.Label();
            this.DateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DataGridView_Count = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Infos)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Count)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridView_Infos
            // 
            this.DataGridView_Infos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView_Infos.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView_Infos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_Infos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.DataGridView_Infos.Location = new System.Drawing.Point(3, 3);
            this.DataGridView_Infos.Name = "DataGridView_Infos";
            this.DataGridView_Infos.RowTemplate.Height = 23;
            this.DataGridView_Infos.Size = new System.Drawing.Size(759, 283);
            this.DataGridView_Infos.TabIndex = 0;
            // 
            // Button_SelectAll
            // 
            this.Button_SelectAll.Location = new System.Drawing.Point(12, 12);
            this.Button_SelectAll.Name = "Button_SelectAll";
            this.Button_SelectAll.Size = new System.Drawing.Size(98, 30);
            this.Button_SelectAll.TabIndex = 1;
            this.Button_SelectAll.Text = "查询全部";
            this.Button_SelectAll.UseVisualStyleBackColor = true;
            this.Button_SelectAll.Click += new System.EventHandler(this.Button_SelectAll_Click);
            // 
            // Button_SelectBySeq
            // 
            this.Button_SelectBySeq.Location = new System.Drawing.Point(12, 48);
            this.Button_SelectBySeq.Name = "Button_SelectBySeq";
            this.Button_SelectBySeq.Size = new System.Drawing.Size(98, 30);
            this.Button_SelectBySeq.TabIndex = 2;
            this.Button_SelectBySeq.Text = "查询范围";
            this.Button_SelectBySeq.UseVisualStyleBackColor = true;
            this.Button_SelectBySeq.Click += new System.EventHandler(this.Button_SelectBySeq_Click);
            // 
            // Label_Start
            // 
            this.Label_Start.AutoSize = true;
            this.Label_Start.Location = new System.Drawing.Point(138, 57);
            this.Label_Start.Name = "Label_Start";
            this.Label_Start.Size = new System.Drawing.Size(35, 12);
            this.Label_Start.TabIndex = 3;
            this.Label_Start.Text = " 起始";
            // 
            // Label_End
            // 
            this.Label_End.AutoSize = true;
            this.Label_End.Location = new System.Drawing.Point(303, 57);
            this.Label_End.Name = "Label_End";
            this.Label_End.Size = new System.Drawing.Size(35, 12);
            this.Label_End.TabIndex = 4;
            this.Label_End.Text = " 结束";
            // 
            // TextBox_Seq_Start
            // 
            this.TextBox_Seq_Start.Location = new System.Drawing.Point(179, 54);
            this.TextBox_Seq_Start.Name = "TextBox_Seq_Start";
            this.TextBox_Seq_Start.Size = new System.Drawing.Size(100, 21);
            this.TextBox_Seq_Start.TabIndex = 5;
            this.TextBox_Seq_Start.Text = "0";
            // 
            // TextBox_Seq_End
            // 
            this.TextBox_Seq_End.Location = new System.Drawing.Point(344, 54);
            this.TextBox_Seq_End.Name = "TextBox_Seq_End";
            this.TextBox_Seq_End.Size = new System.Drawing.Size(100, 21);
            this.TextBox_Seq_End.TabIndex = 6;
            this.TextBox_Seq_End.Text = "9999";
            // 
            // DateTimePicker_Time
            // 
            this.DateTimePicker_Time.Location = new System.Drawing.Point(492, 54);
            this.DateTimePicker_Time.Name = "DateTimePicker_Time";
            this.DateTimePicker_Time.Size = new System.Drawing.Size(137, 21);
            this.DateTimePicker_Time.TabIndex = 7;
            // 
            // Button_ExportSelect
            // 
            this.Button_ExportSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_ExportSelect.Location = new System.Drawing.Point(690, 12);
            this.Button_ExportSelect.Name = "Button_ExportSelect";
            this.Button_ExportSelect.Size = new System.Drawing.Size(98, 30);
            this.Button_ExportSelect.TabIndex = 8;
            this.Button_ExportSelect.Text = "导出选择";
            this.Button_ExportSelect.UseVisualStyleBackColor = true;
            this.Button_ExportSelect.Click += new System.EventHandler(this.Button_ExportSelect_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Seq";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "VIN";
            this.Column2.Name = "Column2";
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Type";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "HUD";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "State";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Time";
            this.Column6.Name = "Column6";
            // 
            // Button_SelectByTime
            // 
            this.Button_SelectByTime.Location = new System.Drawing.Point(12, 84);
            this.Button_SelectByTime.Name = "Button_SelectByTime";
            this.Button_SelectByTime.Size = new System.Drawing.Size(98, 30);
            this.Button_SelectByTime.TabIndex = 9;
            this.Button_SelectByTime.Text = "查询时间";
            this.Button_SelectByTime.UseVisualStyleBackColor = true;
            this.Button_SelectByTime.Click += new System.EventHandler(this.Button_SelectByTime_Click);
            // 
            // DateTimePicker_Start
            // 
            this.DateTimePicker_Start.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DateTimePicker_Start.Location = new System.Drawing.Point(179, 87);
            this.DateTimePicker_Start.Name = "DateTimePicker_Start";
            this.DateTimePicker_Start.Size = new System.Drawing.Size(137, 21);
            this.DateTimePicker_Start.TabIndex = 10;
            // 
            // Label_StartTime
            // 
            this.Label_StartTime.AutoSize = true;
            this.Label_StartTime.Location = new System.Drawing.Point(138, 93);
            this.Label_StartTime.Name = "Label_StartTime";
            this.Label_StartTime.Size = new System.Drawing.Size(35, 12);
            this.Label_StartTime.TabIndex = 11;
            this.Label_StartTime.Text = " 起始";
            // 
            // Label_EndTime
            // 
            this.Label_EndTime.AutoSize = true;
            this.Label_EndTime.Location = new System.Drawing.Point(342, 93);
            this.Label_EndTime.Name = "Label_EndTime";
            this.Label_EndTime.Size = new System.Drawing.Size(35, 12);
            this.Label_EndTime.TabIndex = 12;
            this.Label_EndTime.Text = " 结束";
            // 
            // DateTimePicker_End
            // 
            this.DateTimePicker_End.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DateTimePicker_End.Location = new System.Drawing.Point(383, 87);
            this.DateTimePicker_End.Name = "DateTimePicker_End";
            this.DateTimePicker_End.Size = new System.Drawing.Size(137, 21);
            this.DateTimePicker_End.TabIndex = 13;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 120);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 318);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DataGridView_Infos);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 292);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DataGridView_Count);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 292);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "统计";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DataGridView_Count
            // 
            this.DataGridView_Count.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView_Count.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView_Count.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_Count.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11});
            this.DataGridView_Count.Location = new System.Drawing.Point(6, 6);
            this.DataGridView_Count.Name = "DataGridView_Count";
            this.DataGridView_Count.RowTemplate.Height = 23;
            this.DataGridView_Count.Size = new System.Drawing.Size(756, 280);
            this.DataGridView_Count.TabIndex = 0;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "车型";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "总数";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "OK";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "NG";
            this.Column10.Name = "Column10";
            // 
            // Column11
            // 
            this.Column11.HeaderText = "未做";
            this.Column11.Name = "Column11";
            // 
            // Form_FIS_Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.DateTimePicker_End);
            this.Controls.Add(this.Label_EndTime);
            this.Controls.Add(this.Label_StartTime);
            this.Controls.Add(this.DateTimePicker_Start);
            this.Controls.Add(this.Button_SelectByTime);
            this.Controls.Add(this.Button_ExportSelect);
            this.Controls.Add(this.DateTimePicker_Time);
            this.Controls.Add(this.TextBox_Seq_End);
            this.Controls.Add(this.TextBox_Seq_Start);
            this.Controls.Add(this.Label_End);
            this.Controls.Add(this.Label_Start);
            this.Controls.Add(this.Button_SelectBySeq);
            this.Controls.Add(this.Button_SelectAll);
            this.Name = "Form_FIS_Info";
            this.Text = "FIS数据";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Infos)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Count)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridView_Infos;
        private System.Windows.Forms.Button Button_SelectAll;
        private System.Windows.Forms.Button Button_SelectBySeq;
        private System.Windows.Forms.Label Label_Start;
        private System.Windows.Forms.Label Label_End;
        private System.Windows.Forms.TextBox TextBox_Seq_Start;
        private System.Windows.Forms.TextBox TextBox_Seq_End;
        private System.Windows.Forms.DateTimePicker DateTimePicker_Time;
        private System.Windows.Forms.Button Button_ExportSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Button Button_SelectByTime;
        private System.Windows.Forms.DateTimePicker DateTimePicker_Start;
        private System.Windows.Forms.Label Label_StartTime;
        private System.Windows.Forms.Label Label_EndTime;
        private System.Windows.Forms.DateTimePicker DateTimePicker_End;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView DataGridView_Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    }
}