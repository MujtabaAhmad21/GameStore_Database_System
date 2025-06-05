using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamingStoreApplication
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (Form1.loginname != null)
            {
                toolStripStatusLabel2.Text = Form1.loginname;
            }
            if (Form1.logintype != null && Form1.logintype == "SELLER")
            {
                masterToolStripMenuItem.Enabled = false;
                masterToolStripMenuItem.ForeColor = Color.Red;
                productToolStripMenuItem.Enabled = false;
                productToolStripMenuItem.ForeColor = Color.Red;
                addUserToolStripMenuItem.Enabled = false;
                addUserToolStripMenuItem.ForeColor = Color.Red;
            }
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategory fcat = new frmCategory();
            fcat.Show();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 abt = new AboutBox1();
            abt.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you really want to close this application?", "CLOSE", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (dialog == DialogResult.No)
            {
                e.Cancel = true;
            }
            else 
            {
                Application.Exit();
            }
        }

        private void sellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewSeller fSeller = new frmAddNewSeller();
            fSeller.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAdmin aaf = new AddAdmin();
            aaf.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProduct ap = new AddProduct();
            ap.ShowDialog();
        }
    }
}
