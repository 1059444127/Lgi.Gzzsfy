using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LGInterface.Util;
using SendPisResult;

namespace SampleReceiveLogin
{
    public partial class Login : Form
    {
        private bool _loginSuccess = false;
        private bool _urlOpened=false;

        public Login()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            this.Closed += Login_Closed;
        }

        private void Login_Closed(object sender, EventArgs e)
        {

                Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string yhm = txtYhm.Text.Trim();
            if (yhm == "")
            {
                MessageBox.Show("请输入用户名!");
                txtYhm.Focus();
                return;
            }

            string sql = $" select top 1 * from t_yh where f_yhm='{yhm}' ";
            var dtYh = DbHelper.GetTable(sql);
            if (dtYh == null || dtYh.Rows.Count == 0)
            {
                MessageBox.Show("登录失败:用户不存在!");
                txtYhm.Focus();
                return;
            }
            if (dtYh.Rows[0]["F_YHMM"].ToString() != txtPwd.Text)
            {
                MessageBox.Show("登录失败:密码错误!");
                txtPwd.Focus();
                return;
            }

            //验证通过,调用浏览器并退出登录程序
            _loginSuccess = true;
            IniFiles f = new IniFiles("sz.ini");
            var url = f.ReadString("标本签收", "url", "http://168.168.252.111:8889/Receive/Receipt.aspx");
            if (_urlOpened==false)
            {
                _urlOpened = true;
                //调用系统默认的浏览器   
                Process.Start(url +$"?userid={dtYh.Rows[0]["F_YHM"]}&username={dtYh.Rows[0]["F_YHMC"]}");
            }
            Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtYhm.Focus();
        }

        private void txtYhm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPwd.Focus();
            }
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            //
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                e.Handled = true;
                btnLogin_Click(null, null);
            }
        }
    }
}