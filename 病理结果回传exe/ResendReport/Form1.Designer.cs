namespace ResendReport
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbBlk = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSqlWhere = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnShowLog = new System.Windows.Forms.Button();
            this.btnResend = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dteShsj2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBrlb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dteShsj1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.上传状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fBLKDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fBRLBDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fBLHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fXMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fBRBHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fSPARE5DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fSQXHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fXBDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fNLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fZYHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fMZHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.上传失败原因 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tJCXXBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnClearLog = new System.Windows.Forms.Button();
            this.chkSearchOnlyUnupload = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tJCXXBindingSource)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkSearchOnlyUnupload);
            this.panel1.Controls.Add(this.btnClearLog);
            this.panel1.Controls.Add(this.cmbBlk);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtSqlWhere);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnShowLog);
            this.panel1.Controls.Add(this.btnResend);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dteShsj2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtBrlb);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dteShsj1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2188, 275);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cmbBlk
            // 
            this.cmbBlk.FormattingEnabled = true;
            this.cmbBlk.Location = new System.Drawing.Point(165, 28);
            this.cmbBlk.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.cmbBlk.Name = "cmbBlk";
            this.cmbBlk.Size = new System.Drawing.Size(239, 38);
            this.cmbBlk.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(869, 207);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(628, 30);
            this.label6.TabIndex = 11;
            this.label6.Text = "SQL条件必须以and或者or开头,查询的表是JCXX";
            // 
            // txtSqlWhere
            // 
            this.txtSqlWhere.Location = new System.Drawing.Point(165, 152);
            this.txtSqlWhere.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSqlWhere.Name = "txtSqlWhere";
            this.txtSqlWhere.Size = new System.Drawing.Size(1332, 42);
            this.txtSqlWhere.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 162);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 30);
            this.label5.TabIndex = 9;
            this.label5.Text = "SQL条件:";
            // 
            // btnShowLog
            // 
            this.btnShowLog.Location = new System.Drawing.Point(1545, 25);
            this.btnShowLog.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnShowLog.Name = "btnShowLog";
            this.btnShowLog.Size = new System.Drawing.Size(182, 65);
            this.btnShowLog.TabIndex = 8;
            this.btnShowLog.Text = "日志";
            this.btnShowLog.UseVisualStyleBackColor = true;
            this.btnShowLog.Click += new System.EventHandler(this.btnShowLog_Click);
            // 
            // btnResend
            // 
            this.btnResend.Location = new System.Drawing.Point(1320, 25);
            this.btnResend.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnResend.Name = "btnResend";
            this.btnResend.Size = new System.Drawing.Size(182, 65);
            this.btnResend.TabIndex = 8;
            this.btnResend.Text = "重传";
            this.btnResend.UseVisualStyleBackColor = true;
            this.btnResend.Click += new System.EventHandler(this.btnResend_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1090, 25);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(182, 65);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(448, 100);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 30);
            this.label4.TabIndex = 7;
            this.label4.Text = "审核时间2:";
            // 
            // dteShsj2
            // 
            this.dteShsj2.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dteShsj2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dteShsj2.Location = new System.Drawing.Point(618, 90);
            this.dteShsj2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dteShsj2.Name = "dteShsj2";
            this.dteShsj2.Size = new System.Drawing.Size(334, 42);
            this.dteShsj2.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(448, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 30);
            this.label3.TabIndex = 5;
            this.label3.Text = "审核时间1:";
            // 
            // txtBrlb
            // 
            this.txtBrlb.Location = new System.Drawing.Point(165, 90);
            this.txtBrlb.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBrlb.Name = "txtBrlb";
            this.txtBrlb.Size = new System.Drawing.Size(239, 42);
            this.txtBrlb.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "病人类别:";
            // 
            // dteShsj1
            // 
            this.dteShsj1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dteShsj1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dteShsj1.Location = new System.Drawing.Point(618, 28);
            this.dteShsj1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dteShsj1.Name = "dteShsj1";
            this.dteShsj1.Size = new System.Drawing.Size(334, 42);
            this.dteShsj1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "病例库:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblStatus.Location = new System.Drawing.Point(665, 30);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(178, 30);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "等待上传...";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 275);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2188, 1010);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.上传状态,
            this.fBLKDataGridViewTextBoxColumn,
            this.fBRLBDataGridViewTextBoxColumn,
            this.fBLHDataGridViewTextBoxColumn,
            this.fXMDataGridViewTextBoxColumn,
            this.fBRBHDataGridViewTextBoxColumn,
            this.fSPARE5DataGridViewTextBoxColumn,
            this.fSQXHDataGridViewTextBoxColumn,
            this.fXBDataGridViewTextBoxColumn,
            this.fNLDataGridViewTextBoxColumn,
            this.fZYHDataGridViewTextBoxColumn,
            this.fMZHDataGridViewTextBoxColumn,
            this.上传失败原因});
            this.dataGridView1.DataSource = this.tJCXXBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 40;
            this.dataGridView1.Size = new System.Drawing.Size(2188, 928);
            this.dataGridView1.TabIndex = 0;
            // 
            // 上传状态
            // 
            this.上传状态.DataPropertyName = "上传状态";
            this.上传状态.HeaderText = "上传状态";
            this.上传状态.Name = "上传状态";
            this.上传状态.ReadOnly = true;
            // 
            // fBLKDataGridViewTextBoxColumn
            // 
            this.fBLKDataGridViewTextBoxColumn.DataPropertyName = "F_BLK";
            this.fBLKDataGridViewTextBoxColumn.HeaderText = "病例库";
            this.fBLKDataGridViewTextBoxColumn.Name = "fBLKDataGridViewTextBoxColumn";
            this.fBLKDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fBRLBDataGridViewTextBoxColumn
            // 
            this.fBRLBDataGridViewTextBoxColumn.DataPropertyName = "F_BRLB";
            this.fBRLBDataGridViewTextBoxColumn.HeaderText = "病人类别";
            this.fBRLBDataGridViewTextBoxColumn.Name = "fBRLBDataGridViewTextBoxColumn";
            this.fBRLBDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fBLHDataGridViewTextBoxColumn
            // 
            this.fBLHDataGridViewTextBoxColumn.DataPropertyName = "F_BLH";
            this.fBLHDataGridViewTextBoxColumn.HeaderText = "病理号";
            this.fBLHDataGridViewTextBoxColumn.Name = "fBLHDataGridViewTextBoxColumn";
            this.fBLHDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fXMDataGridViewTextBoxColumn
            // 
            this.fXMDataGridViewTextBoxColumn.DataPropertyName = "F_XM";
            this.fXMDataGridViewTextBoxColumn.HeaderText = "姓名";
            this.fXMDataGridViewTextBoxColumn.Name = "fXMDataGridViewTextBoxColumn";
            this.fXMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fBRBHDataGridViewTextBoxColumn
            // 
            this.fBRBHDataGridViewTextBoxColumn.DataPropertyName = "F_BRBH";
            this.fBRBHDataGridViewTextBoxColumn.HeaderText = "编号";
            this.fBRBHDataGridViewTextBoxColumn.Name = "fBRBHDataGridViewTextBoxColumn";
            this.fBRBHDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fSPARE5DataGridViewTextBoxColumn
            // 
            this.fSPARE5DataGridViewTextBoxColumn.DataPropertyName = "F_SPARE5";
            this.fSPARE5DataGridViewTextBoxColumn.HeaderText = "审核时间";
            this.fSPARE5DataGridViewTextBoxColumn.Name = "fSPARE5DataGridViewTextBoxColumn";
            this.fSPARE5DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fSQXHDataGridViewTextBoxColumn
            // 
            this.fSQXHDataGridViewTextBoxColumn.DataPropertyName = "F_SQXH";
            this.fSQXHDataGridViewTextBoxColumn.HeaderText = "申请序号";
            this.fSQXHDataGridViewTextBoxColumn.Name = "fSQXHDataGridViewTextBoxColumn";
            this.fSQXHDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fXBDataGridViewTextBoxColumn
            // 
            this.fXBDataGridViewTextBoxColumn.DataPropertyName = "F_XB";
            this.fXBDataGridViewTextBoxColumn.HeaderText = "性别";
            this.fXBDataGridViewTextBoxColumn.Name = "fXBDataGridViewTextBoxColumn";
            this.fXBDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fNLDataGridViewTextBoxColumn
            // 
            this.fNLDataGridViewTextBoxColumn.DataPropertyName = "F_NL";
            this.fNLDataGridViewTextBoxColumn.HeaderText = "年龄";
            this.fNLDataGridViewTextBoxColumn.Name = "fNLDataGridViewTextBoxColumn";
            this.fNLDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fZYHDataGridViewTextBoxColumn
            // 
            this.fZYHDataGridViewTextBoxColumn.DataPropertyName = "F_ZYH";
            this.fZYHDataGridViewTextBoxColumn.HeaderText = "住院号";
            this.fZYHDataGridViewTextBoxColumn.Name = "fZYHDataGridViewTextBoxColumn";
            this.fZYHDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fMZHDataGridViewTextBoxColumn
            // 
            this.fMZHDataGridViewTextBoxColumn.DataPropertyName = "F_MZH";
            this.fMZHDataGridViewTextBoxColumn.HeaderText = "门诊号";
            this.fMZHDataGridViewTextBoxColumn.Name = "fMZHDataGridViewTextBoxColumn";
            this.fMZHDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 上传失败原因
            // 
            this.上传失败原因.DataPropertyName = "上传失败原因";
            this.上传失败原因.HeaderText = "上传失败原因";
            this.上传失败原因.Name = "上传失败原因";
            this.上传失败原因.ReadOnly = true;
            // 
            // tJCXXBindingSource
            // 
            this.tJCXXBindingSource.DataSource = typeof(SendPisResult.Models.T_JCXX);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblStatus);
            this.panel3.Controls.Add(this.progressBar1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 928);
            this.panel3.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(2188, 82);
            this.panel3.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(25, 12);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(630, 58);
            this.progressBar1.Step = 5;
            this.progressBar1.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnClearLog
            // 
            this.btnClearLog.ForeColor = System.Drawing.Color.Blue;
            this.btnClearLog.Location = new System.Drawing.Point(1779, 25);
            this.btnClearLog.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(207, 65);
            this.btnClearLog.TabIndex = 13;
            this.btnClearLog.Text = "清空上传记录";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // chkSearchOnlyUnupload
            // 
            this.chkSearchOnlyUnupload.AutoSize = true;
            this.chkSearchOnlyUnupload.Location = new System.Drawing.Point(165, 206);
            this.chkSearchOnlyUnupload.Name = "chkSearchOnlyUnupload";
            this.chkSearchOnlyUnupload.Size = new System.Drawing.Size(351, 34);
            this.chkSearchOnlyUnupload.TabIndex = 14;
            this.chkSearchOnlyUnupload.Text = "只查询未上传过的记录";
            this.chkSearchOnlyUnupload.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2188, 1285);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "结果重传";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tJCXXBindingSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dteShsj1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dteShsj2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBrlb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnResend;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSqlWhere;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.BindingSource tJCXXBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn 上传状态;
        private System.Windows.Forms.DataGridViewTextBoxColumn fBLKDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fBRLBDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fBLHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fXMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fBRBHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fSPARE5DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fSQXHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fXBDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fNLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fZYHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fMZHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 上传失败原因;
        private System.Windows.Forms.ComboBox cmbBlk;
        private System.Windows.Forms.Button btnShowLog;
        private System.Windows.Forms.CheckBox chkSearchOnlyUnupload;
        private System.Windows.Forms.Button btnClearLog;
    }
}

