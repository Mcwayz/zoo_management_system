using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Zoo_Sys_ADI
{
    public partial class frmAnimals : Form
    {
        public frmAnimals()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|ADI.mdb");
        OleDbCommand cmd = new OleDbCommand();


        private void frmAnimals_Load(object sender, EventArgs e)
        {
            getData();
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }
        public void getData()
        {
            // TODO: This line of code loads data into the 'aDIDataSet.animal_tbl' table. You can move, or remove it, as needed.
            this.animal_tblTableAdapter.Fill(this.aDIDataSet.animal_tbl);
        }

        public void update()
        {
            con.Open();
            string answers = "UPDATE animal_tbl SET animal_species='"+ txtSpecie.Text +"', animal_name='"+ txtName.Text +"', animal_gender='"+ cboGender.SelectedItem.ToString() +"', animal_age='"+ txtAge.Text + "' WHERE animal_id=" + txtNo.Text+"";
            cmd = new OleDbCommand(answers, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Animal Successfully Updated!", "Deletion Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            txtNo.Text = "";
            txtSpecie.Text = "";
            txtName.Text = "";
            cboGender.SelectedIndex = 0;
            txtAge.Text = "";
            getData();
        }

        public void delete()
        {
            con.Open();
            string animal = "DELETE FROM animal_tbl WHERE animal_id=" + txtNo.Text + "";
            cmd = new OleDbCommand(animal, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Animal Successfully Deleted!", "Deletion Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            getData();
        }

        public void addAnimal()
        {
            if (txtNo.Text == null && txtSpecie.Text == null && txtName.Text == null && cboGender.SelectedIndex == 0 && txtAge.Text == null)
            {
                MessageBox.Show("Animal Data Missing", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                con.Open();
                string animal = "INSERT INTO animal_tbl VALUES ('" + txtNo.Text + "','" + txtSpecie.Text + "','" + txtName.Text + "','" + cboGender.SelectedItem.ToString() + "','" + txtAge.Text + "')";
                cmd = new OleDbCommand(animal, con);
                cmd.ExecuteNonQuery();
                con.Close();

                txtNo.Text = "";
                txtName.Text = "";
                txtSpecie.Text = "";
                txtAge.Text = "";
                cboGender.SelectedIndex = 0;
                MessageBox.Show("Animal Successfully Added", "Creation Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            getData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addAnimal();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            update();
        }

        private void dataAnimal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataAnimal.Rows[e.RowIndex];
                txtNo.Text = row.Cells[0].Value.ToString();
                txtSpecie.Text = row.Cells[1].Value.ToString();
                txtName.Text = row.Cells[2].Value.ToString();
                cboGender.Text = row.Cells[3].Value.ToString();
                txtAge.Text = row.Cells[4].Value.ToString();

                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            new frmDash().Show();
            this.Hide();
        }
    }
}
