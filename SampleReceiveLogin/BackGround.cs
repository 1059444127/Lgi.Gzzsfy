using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SampleReceiveLogin
{
    public partial class BackGround : Form
    {
        public BackGround()
        {
            InitializeComponent();
            StartPosition=FormStartPosition.CenterScreen;
            this.Shown += BackGround_Shown;
        }

        private void BackGround_Shown(object sender, EventArgs e)
        {
            new Login().ShowDialog();
        }

        private void BackGround_Load(object sender, EventArgs e)
        {

        }
    }
}
