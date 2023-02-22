using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Zoo_Sys_ADI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|ADI.mdb");
        OleDbCommand cmd = new OleDbCommand();

        public void Login()
        {
            con.Open();
            string login = "SELECT * FROM user_tbl WHERE username= '" + txtUsername.Text + "' and usr_password= '" + txtpassword.Text + "'";
            cmd = new OleDbCommand(login, con);
            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                MessageBox.Show("Login Successful, You are Welcome", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                new frmDash().Show();
                con.Close();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password, Please Try Again", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Text = "";
                txtpassword.Text = "";
                txtUsername.Focus();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Login function
            Login();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtpassword.Text = "";
            txtUsername.Focus();
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtpassword.PasswordChar = '\0';
            }
            else
            {
                txtpassword.PasswordChar = '•';
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new frmRegister().Show();
            this.Hide();
        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
