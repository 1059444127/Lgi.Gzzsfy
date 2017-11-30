using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dbbase;
using Maticsoft.DAL;
using SendPisResult;
using SendPisResult.Models;

namespace ResendReport
{
    public partial class Form1 : Form
    {
        private List<T_JCXX> _lstJcxx = null;
        dbbase.odbcdb aa = new odbcdb("DSN=pathnet;UID=pathnet;PWD=4s3c2a1p", "", "");

        public Form1()
        {
            InitializeComponent();

            cmbBlk.Items.Add("");
            var dtBlk = aa.GetDataTable(" select F_BLKMC from t_blk_cs ", "dt1");
            foreach (DataRow dtBlkRow in dtBlk.Rows)
            {
                cmbBlk.Items.Add(dtBlkRow["F_BLKMC"].ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //get sqlWhere
            var blk = cmbBlk.Text.Trim();
            var brlb = txtBrlb.Text.Trim();
            var strShsj1 = dteShsj1.Text;
            var strShsj2 = dteShsj2.Text;

            string sqlWhere = " and  (  1=1 ";
            if (string.IsNullOrEmpty(blk) == false)
            {
                sqlWhere += $" and f_blk = '{blk}' ";
            }
            if (string.IsNullOrEmpty(brlb) == false)
            {
                sqlWhere += $" and f_brlb = '{brlb}' ";
            }
            sqlWhere += $" and convert(datetime,f_spare5) >=  convert(datetime,'{strShsj1}')";
            sqlWhere += $" and convert(datetime,f_spare5) <=  convert(datetime,'{strShsj2}')";
            sqlWhere +=" ) ";
            if (txtSqlWhere.Text.Trim()!="")
            {
                sqlWhere = sqlWhere + " " + txtSqlWhere.Text.Trim() + " ";
            }
            if (chkSearchOnlyUnupload.Checked == true)
            {
                sqlWhere += $" and f_blh not in (select f_blh from T_BGCC_LOG ) ";
            }
            

            _lstJcxx = (new T_JCXX_DAL()).GetBySqlWhere(sqlWhere);

            lblStatus.Text = $"等待上传,共{_lstJcxx.Count}条";

            BindData();
        }

        private void BindData()
        {
            _lstJcxx.ForEach(o => o.上传状态 = "未上传");
            tJCXXBindingSource.DataSource = _lstJcxx;
            dataGridView1.Refresh();
            dataGridView1.AutoResizeColumns();
        }

        private void btnResend_Click(object sender, EventArgs e)
        {
            if (_lstJcxx == null) return;

            var startTime = DateTime.Now;
            double usedSeconds = 0;
            double totalSeconds = 0;


            foreach (var jcxx in _lstJcxx)
            {
                var dateSpan = (DateTime.Now-startTime);
                var lefSeconds = dateSpan.TotalSeconds / (Convert.ToDouble(_lstJcxx.IndexOf(jcxx))) *
                                  (_lstJcxx.Count - Convert.ToDouble(_lstJcxx.IndexOf(jcxx)));
                var leftMinutes = lefSeconds / 60;


                lblStatus.Text = $"正在上传:{_lstJcxx.IndexOf(jcxx)}/{_lstJcxx.Count} " +
                                 $"已耗时:{(int)dateSpan.TotalHours}小时{dateSpan.Minutes}分{dateSpan.Seconds}秒 " +
                                 $"剩余时间:{(int)(leftMinutes/60)}小时{(int)(leftMinutes%60)}分{(int)lefSeconds%60}秒 ";
                Application.DoEvents();

                var args = $"{jcxx.F_BLH}^cg^1^old^save";
                try
                {
                    var failed = SendPisResult.Program.CallSendResultPlus(args);
                    if (failed)
                        throw new Exception($"病理号={jcxx.F_BLH}");
                    jcxx.上传状态 = "上传成功";
                }
                catch (Exception exception)
                {
                    jcxx.上传状态 = "上传失败";
                    jcxx.上传失败原因 = exception + "\r\n" + exception.InnerException;
                }
                finally
                {
                    string insertLog = $@"INSERT INTO [dbo].[T_BGCC_LOG]
                                           ([F_BLK]
                                           ,[F_BLH]
                                           ,[F_XM]
                                           ,[F_NL]
                                           ,[F_XB]
                                           ,[F_SCZY]
                                           ,[F_SBYY]
                                           ,[F_SCSJ])
                                          VALUES
                                           ('{jcxx.F_BLK}'
                                           ,'{jcxx.F_BLH}'
                                           ,'{jcxx.F_XM}'
                                           ,'{jcxx.F_NL}'
                                           ,'{jcxx.F_XB}'
                                           ,'{jcxx.上传状态}'
                                           ,'{jcxx.上传失败原因}'
                                           ,'{DateTime.Now}')";
                    aa.ExecuteSQL(insertLog);

                    progressBar1.Value = (int) (Convert.ToDouble(_lstJcxx.IndexOf(jcxx))*100 / _lstJcxx.Count);
                }
            }
            lblStatus.Text = "上传完成!";
            MessageBox.Show("重传完成!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IniFiles f = new IniFiles("sz.ini");
            var hospName = f.ReadString("savetohis", "yymc", "");

            if (string.IsNullOrEmpty(hospName.Trim()))
            {
                MessageBox.Show("没有找到sz.ini,或者未配置yymc,无法找到导入接口!");
                Application.Exit();
            }

            this.Text = "结果重传  医院名称:" + hospName;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
//            if (e.ColumnIndex == dataGridView1.Columns.IndexOf(colUpStatue))
//            {
//                var statue = e.Value;
//                var rv = dataGridView1.Rows[e.RowIndex] as DataGridViewRow;
//                switch (statue)
//                {
//                    case "未上传":
//                        (dataGridView1.Rows[e.RowIndex].
//                }
//            }
        }

        private void btnShowLog_Click(object sender, EventArgs e)
        {
            var f = new UploadLog();
            f.OnImportUploadList += list =>
            {
                var dal = new T_JCXX_DAL();
                var count = 0;
                var tempList=new List<string>();
                _lstJcxx=new List<T_JCXX>();

                foreach (string blh in list)
                {
                    tempList.Add(blh);

                    //每次用in查出500条
                    if (tempList.Count == 500 || list[list.Count - 1] == blh)
                    {
                        var blhString = "";
                        tempList.ForEach(o=>blhString += $"'{o}',");
                        blhString=blhString.TrimEnd(',');

                        var sqlWhere = $" and f_blh in ({blhString}) ";
                        _lstJcxx.AddRange(dal.GetBySqlWhere(sqlWhere));

                        tempList.Clear();
                    }
                }

                BindData();
                lblStatus.Text = $"等待上传,共{_lstJcxx.Count}条";
            };

            f.ShowDialog();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           Application.Exit();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定清空上传记录?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                return;

            aa.ExecuteSQL("delete T_BGCC_LOG");

            MessageBox.Show("已清空上传记录.");
        }
    }
}