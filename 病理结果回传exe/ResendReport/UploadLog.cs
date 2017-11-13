using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dbbase;

namespace ResendReport
{
    public partial class UploadLog : Form
    {
        private odbcdb aa = new odbcdb("DSN=pathnet;UID=pathnet;PWD=4s3c2a1p", "", "");
        private List<UploadLogEntity> logs = new List<UploadLogEntity>();
        public event Action<List<string>> OnImportUploadList; 

        public UploadLog()
        {
            InitializeComponent();

            cmbBlk.Items.Add("");
            var dtBlk = aa.GetDataTable(" select F_BLKMC from t_blk_cs ", "dt1");
            foreach (DataRow dtBlkRow in dtBlk.Rows)
            {
                cmbBlk.Items.Add(dtBlkRow["F_BLKMC"].ToString());
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            logs.Clear();

            string sql = " select * from T_BGCC_LOG where ";
            string sqlWhere = " 1=1 ";

            if (cmbBlk.Text.Trim() != "")
                sqlWhere += $" and f_blk = '{cmbBlk.Text.Trim()}' ";
            if (cmbZt.Text.Trim() != "")
                sqlWhere += $" and f_Sczy = '{cmbZt.Text.Trim()}' ";
            if (txtBlh.Text != "")
                sqlWhere += $" and f_blh = '{txtBlh.Text.Trim()}' ";
            if (txtXm.Text != "")
                sqlWhere += $" and f_xm = '{txtXm.Text.Trim()}' ";
            sqlWhere += $" and convert(datetime,f_scsj) >=  convert(datetime,'{dteScsj1.Text}')";
            sqlWhere += $" and convert(datetime,f_scsj) <=  convert(datetime,'{dteScsj2.Text}')";

            sql += sqlWhere;
            sql += " order by f_scsj desc";

            var dt = aa.GetDataTable(sql, "aa");
            foreach (DataRow dtRow in dt.Rows)
            {
                logs.Add(new UploadLogEntity(dtRow));
            }

            uploadLogEntityBindingSource.DataSource = logs;
            dataGridView1.DataSource = uploadLogEntityBindingSource;
            dataGridView1.Refresh();
            dataGridView1.AutoResizeColumns();

        }

        public class UploadLogEntity
        {
            public string Blk { get; set; }
            public string Blh { get; set; }
            public string Xm { get; set; }
            public string Nl { get; set; }
            public string Xb { get; set; }
            public string Sczy { get; set; }
            public string Sbyy { get; set; }
            public DateTime Scsj { get; set; }

            public UploadLogEntity()
            {
                
            }

            public UploadLogEntity(DataRow dr)
            {
                this.Blk = dr["F_BLK"].ToString();
                this.Blh = dr["F_BLH"].ToString();
                this.Xm = dr["F_XM"].ToString();
                this.Nl = dr["F_NL"].ToString();
                this.Xb = dr["F_XB"].ToString();
                this.Sczy = dr["F_SCZY"].ToString();
                this.Sbyy = dr["F_SBYY"].ToString();
                this.Scsj = Convert.ToDateTime(dr["F_SCSJ"]);
            }
        }

        private void btnImportToUploadList_Click(object sender, EventArgs e)
        {
            if (logs == null || logs.Count == 0)
            {
                MessageBox.Show("结果列表为空,请先查询!");
                return;
            }

            var blhList = new List<string>();
            foreach (UploadLogEntity logEntity in logs)
            {
                if(blhList.Contains(logEntity.Blh)==false)
                    blhList.Add(logEntity.Blh);
            }

            OnOnImportUploadList(blhList);
            Close();
        }

        protected virtual void OnOnImportUploadList(List<string> obj)
        {
            OnImportUploadList?.Invoke(obj);
        }
    }
}
