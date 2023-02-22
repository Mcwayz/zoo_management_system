using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Zoo_Sys_ADI
{
    public partial class frmWaist : Form
    {
        public frmWaist()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|ADI.mdb");
        OleDbCommand cmd = new OleDbCommand();

        public void getData()
        {
           

            con.Open();
            string query = "SELECT waist_tbl.waist_id,  animal_tbl.animal_name, waist_tbl.waist, waist_tbl.w_time, waist_tbl.w_date FROM animal_tbl INNER JOIN waist_tbl ON animal_tbl.animal_id = waist_tbl.animal_id";
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataWaist.DataSource = dt;
            con.Close();
            getAnimals();
        }

        public void getWaist()
        {
            if (cboName.SelectedItem == null && cboTime.SelectedItem == null && txtWaist.Text == null)
            {
                MessageBox.Show("Waist Data Missing", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                con.Open();
                string waist = "INSERT INTO waist_tbl VALUES ("+ txtID.Text +","+ cboName.SelectedValue.ToString() +","+ txtWaist.Text + ",'" + cboTime.SelectedItem.ToString() + "','" + txtDate.Text + "')";
                cmd = new OleDbCommand(waist, con);
                cmd.ExecuteNonQuery();
                con.Close();

                cboTime.SelectedItem = "";
                cboName.SelectedItem = "";
                txtWaist.Text = "";
                MessageBox.Show("Waist Data  Successfully Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            getData();
        }

        public void getUpdate()
        {
            con.Open();
            string waist = "UPDATE waist_tbl SET animal_id="+ cboName.SelectedValue.ToString() +", waist="+ txtWaist.Text +", w_time='"+ cboTime.SelectedItem.ToString() + "' WHERE waist_id="+ txtID.Text +"";
            cmd = new OleDbCommand(waist, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Waist Data  Successfully Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            lblID.Text = "";
            cboName.SelectedItem = "";
            cboTime.SelectedItem = "";
            txtDate.Text = "";
            getData();
        }

        public void getDelete()
        {
            con.Open();
            string waist = "DELETE FROM waist_tbl WHERE waist_id="+ lblID.Text +"";
            cmd = new OleDbCommand(waist, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Waist Data  Successfully Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

            lblID.Text = "";
            cboName.SelectedItem = "";
            cboTime.SelectedItem = "";
            txtDate.Text = "";
            getData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            getWaist();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            getUpdate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            getDelete();
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

        private void dataWaist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataWaist.Rows[e.RowIndex];
                lblID.Text = row.Cells[0].Value.ToString();
                txtID.Text = row.Cells[0].Value.ToString();
                cboName.SelectedItem = row.Cells[1].Value.ToString();
                txtWaist.Text = row.Cells[2].Value.ToString();
                cboTime.SelectedItem = row.Cells[3].Value.ToString();
                txtDate.Text = row.Cells[4].Value.ToString();
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        public void getAnimals()
        {
            con.Open();
            string query = "SELECT animal_id, animal_name FROM animal_tbl";
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cboName.DataSource = dt;
            cboName.ValueMember = "animal_id";
            cboName.DisplayMember = "animal_name";

            con.Close();
        }

        private void frmWaist_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            getData();
            
        }
    }
}
