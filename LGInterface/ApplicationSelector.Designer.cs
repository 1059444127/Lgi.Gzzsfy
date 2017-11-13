namespace LGInterface
{
    partial class ApplicationSelector
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.患者姓名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.医嘱开立时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.送检科室 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.病人来源 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.检查部位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.检查ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(772, 28);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "双击选择申请项目,按ESC放弃选择";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(772, 318);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.患者姓名,
            this.医嘱开立时间,
            this.送检科室,
            this.病人来源,
            this.检查部位,
            this.检查ID});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(772, 318);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // 患者姓名
            // 
            this.患者姓名.DataPropertyName = "PATIENTNAME";
            this.患者姓名.HeaderText = "患者姓名";
            this.患者姓名.Name = "患者姓名";
            this.患者姓名.ReadOnly = true;
            // 
            // 医嘱开立时间
            // 
            this.医嘱开立时间.DataPropertyName = "DOCTORCHARGESTIME";
            this.医嘱开立时间.HeaderText = "医嘱开立时间";
            this.医嘱开立时间.Name = "医嘱开立时间";
            this.医嘱开立时间.ReadOnly = true;
            // 
            // 送检科室
            // 
            this.送检科室.DataPropertyName = "SENTBYDEPARTMENT";
            this.送检科室.HeaderText = "送检科室";
            this.送检科室.Name = "送检科室";
            this.送检科室.ReadOnly = true;
            // 
            // 病人来源
            // 
            this.病人来源.DataPropertyName = "PATIENTFROM";
            this.病人来源.HeaderText = "病人来源";
            this.病人来源.Name = "病人来源";
            this.病人来源.ReadOnly = true;
            // 
            // 检查部位
            // 
            this.检查部位.DataPropertyName = "EXAMINEPARTS";
            this.检查部位.HeaderText = "检查部位";
            this.检查部位.Name = "检查部位";
            this.检查部位.ReadOnly = true;
            // 
            // 检查ID
            // 
            this.检查ID.DataPropertyName = "exid";
            this.检查ID.HeaderText = "检查ID";
            this.检查ID.Name = "检查ID";
            this.检查ID.ReadOnly = true;
            // 
            // ApplicationSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 346);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "ApplicationSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择申请项目";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ApplicationSelector_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ApplicationSelector_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 患者姓名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 医嘱开立时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 送检科室;
        private System.Windows.Forms.DataGridViewTextBoxColumn 病人来源;
        private System.Windows.Forms.DataGridViewTextBoxColumn 检查部位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 检查ID;
    }
}