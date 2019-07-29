using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormSandbox
{
    public partial class frmStart : Form
    {
        public frmStart()
        {
            InitializeComponent();
        }

        private void btnStartColorMatch_Click(object sender, EventArgs e)
        {
            frmColorMatch fcm = new frmColorMatch();
            fcm.Show();
        }

        private void btnStartMinesweeper_Click(object sender, EventArgs e)
        {
            frmMinesweeper fms = new frmMinesweeper();
            fms.Show();
        }

        private void btnStartTetris_Click(object sender, EventArgs e)
        {
            frmTetris ftt = new frmTetris();
            ftt.Show();
        }
    }
}
