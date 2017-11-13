namespace ResendReport
{
    partial class UploadLog
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmbBlk = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dteScsj2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dteScsj1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBlh = new System.Windows.Forms.TextBox();
            this.cmbZt = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtXm = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.uploadLogEntityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.blkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xbDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sczyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scsjDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sbyyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnImportToUploadList = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uploadLogEntityBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtXm);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbZt);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtBlh);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnImportToUploadList);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dteScsj2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dteScsj1);
            this.panel1.Controls.Add(this.cmbBlk);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(912, 73);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.blkDataGridViewTextBoxColumn,
            this.blhDataGridViewTextBoxColumn,
            this.xmDataGridViewTextBoxColumn,
            this.nlDataGridViewTextBoxColumn,
            this.xbDataGridViewTextBoxColumn,
            this.sczyDataGridViewTextBoxColumn,
            this.scsjDataGridViewTextBoxColumn,
            this.sbyyDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.uploadLogEntityBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 73);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(912, 471);
            this.dataGridView1.TabIndex = 1;
            // 
            // cmbBlk
            // 
            this.cmbBlk.FormattingEnabled = true;
            this.cmbBlk.Location = new System.Drawing.Point(64, 12);
            this.cmbBlk.Name = "cmbBlk";
            this.cmbBlk.Size = new System.Drawing.Size(98, 20);
            this.cmbBlk.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "病例库:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(583, 9);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(73, 26);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(342, 41);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "上传时间2:";
            // 
            // dteScsj2
            // 
            this.dteScsj2.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dteScsj2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dteScsj2.Location = new System.Drawing.Point(410, 37);
            this.dteScsj2.Margin = new System.Windows.Forms.Padding(1);
            this.dteScsj2.Name = "dteScsj2";
            this.dteScsj2.Size = new System.Drawing.Size(136, 21);
            this.dteScsj2.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(342, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "上传时间1:";
            // 
            // dteScsj1
            // 
            this.dteScsj1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dteScsj1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dteScsj1.Location = new System.Drawing.Point(410, 10);
            this.dteScsj1.Margin = new System.Windows.Forms.Padding(1);
            this.dteScsj1.Name = "dteScsj1";
            this.dteScsj1.Size = new System.Drawing.Size(136, 21);
            this.dteScsj1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "病理号:";
            // 
            // txtBlh
            // 
            this.txtBlh.Location = new System.Drawing.Point(64, 38);
            this.txtBlh.Margin = new System.Windows.Forms.Padding(1);
            this.txtBlh.Name = "txtBlh";
            this.txtBlh.Size = new System.Drawing.Size(98, 21);
            this.txtBlh.TabIndex = 21;
            // 
            // cmbZt
            // 
            this.cmbZt.FormattingEnabled = true;
            this.cmbZt.Items.AddRange(new object[] {
            "",
            "上传成功",
            "上传失败"});
            this.cmbZt.Location = new System.Drawing.Point(229, 13);
            this.cmbZt.Name = "cmbZt";
            this.cmbZt.Size = new System.Drawing.Size(98, 20);
            this.cmbZt.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(177, 17);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "状  态:";
            // 
            // txtXm
            // 
            this.txtXm.Location = new System.Drawing.Point(229, 38);
            this.txtXm.Margin = new System.Windows.Forms.Padding(1);
            this.txtXm.Name = "txtXm";
            this.txtXm.Size = new System.Drawing.Size(98, 21);
            this.txtXm.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(177, 42);
            this.label6.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "姓  名:";
            // 
            // uploadLogEntityBindingSource
            // 
            this.uploadLogEntityBindingSource.DataSource = typeof(ResendReport.UploadLog.UploadLogEntity);
            // 
            // blkDataGridViewTextBoxColumn
            // 
            this.blkDataGridViewTextBoxColumn.DataPropertyName = "Blk";
            this.blkDataGridViewTextBoxColumn.HeaderText = "病例库";
            this.blkDataGridViewTextBoxColumn.Name = "blkDataGridViewTextBoxColumn";
            this.blkDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // blhDataGridViewTextBoxColumn
            // 
            this.blhDataGridViewTextBoxColumn.DataPropertyName = "Blh";
            this.blhDataGridViewTextBoxColumn.HeaderText = "病理号";
            this.blhDataGridViewTextBoxColumn.Name = "blhDataGridViewTextBoxColumn";
            this.blhDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // xmDataGridViewTextBoxColumn
            // 
            this.xmDataGridViewTextBoxColumn.DataPropertyName = "Xm";
            this.xmDataGridViewTextBoxColumn.HeaderText = "姓名";
            this.xmDataGridViewTextBoxColumn.Name = "xmDataGridViewTextBoxColumn";
            this.xmDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nlDataGridViewTextBoxColumn
            // 
            this.nlDataGridViewTextBoxColumn.DataPropertyName = "Nl";
            this.nlDataGridViewTextBoxColumn.HeaderText = "年龄";
            this.nlDataGridViewTextBoxColumn.Name = "nlDataGridViewTextBoxColumn";
            this.nlDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // xbDataGridViewTextBoxColumn
            // 
            this.xbDataGridViewTextBoxColumn.DataPropertyName = "Xb";
            this.xbDataGridViewTextBoxColumn.HeaderText = "性别";
            this.xbDataGridViewTextBoxColumn.Name = "xbDataGridViewTextBoxColumn";
            this.xbDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sczyDataGridViewTextBoxColumn
            // 
            this.sczyDataGridViewTextBoxColumn.DataPropertyName = "Sczy";
            this.sczyDataGridViewTextBoxColumn.HeaderText = "状态";
            this.sczyDataGridViewTextBoxColumn.Name = "sczyDataGridViewTextBoxColumn";
            this.sczyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // scsjDataGridViewTextBoxColumn
            // 
            this.scsjDataGridViewTextBoxColumn.DataPropertyName = "Scsj";
            this.scsjDataGridViewTextBoxColumn.HeaderText = "上传时间";
            this.scsjDataGridViewTextBoxColumn.Name = "scsjDataGridViewTextBoxColumn";
            this.scsjDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sbyyDataGridViewTextBoxColumn
            // 
            this.sbyyDataGridViewTextBoxColumn.DataPropertyName = "Sbyy";
            this.sbyyDataGridViewTextBoxColumn.HeaderText = "失败原因";
            this.sbyyDataGridViewTextBoxColumn.Name = "sbyyDataGridViewTextBoxColumn";
            this.sbyyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // btnImportToUploadList
            // 
            this.btnImportToUploadList.Location = new System.Drawing.Point(690, 10);
            this.btnImportToUploadList.Margin = new System.Windows.Forms.Padding(1);
            this.btnImportToUploadList.Name = "btnImportToUploadList";
            this.btnImportToUploadList.Size = new System.Drawing.Size(143, 26);
            this.btnImportToUploadList.TabIndex = 19;
            this.btnImportToUploadList.Text = "转化为上传任务";
            this.btnImportToUploadList.UseVisualStyleBackColor = true;
            this.btnImportToUploadList.Click += new System.EventHandler(this.btnImportToUploadList_Click);
            // 
            // UploadLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 544);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "UploadLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "上传记录";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uploadLogEntityBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmbBlk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dteScsj2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dteScsj1;
        private System.Windows.Forms.TextBox txtBlh;
        private System.Windows.Forms.ComboBox cmbZt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtXm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.BindingSource uploadLogEntityBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn blkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn blhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nlDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xbDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sczyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scsjDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sbyyDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnImportToUploadList;
    }
}