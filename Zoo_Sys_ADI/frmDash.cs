using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zoo_Sys_ADI
{
    public partial class frmDash : Form
    {
        public frmDash()
        {
            InitializeComponent();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btnAnimals_Click(object sender, EventArgs e)
        {
            new frmAnimals().Show();
            this.Hide();
        }

        private void btnMeals_Click(object sender, EventArgs e)
        {
            new frmMeals().Show();
            this.Hide();
        }

        private void btnExe_Click(object sender, EventArgs e)
        {
            new frmExercises().Show();
            this.Hide();
        }

        private void btnWeight_Click(object sender, EventArgs e)
        {
            new frmWieght().Show();
            this.Hide();
        }

        private void btnWaist_Click(object sender, EventArgs e)
        {
            new frmWaist().Show();
            this.Hide();
        }

        private void btnData_Click(object sender, EventArgs e)
        {
            new frmData().Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnFeed_Click(object sender, EventArgs e)
        {
            new frmFeed().Show();
            this.Hide();
        }

        private void btnMeals_Click_1(object sender, EventArgs e)
        {
            new frmMeals().Show();
            this.Hide();
        }
    }
 
}
