using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Zoo_Sys_ADI
{
    public partial class frmWieght : Form
    {
        public frmWieght()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|ADI.mdb");
        OleDbCommand cmd = new OleDbCommand();


        public void getWeight()
        {
            if (cboName.SelectedItem == null && cboTime.SelectedItem == null && txtWeight.Text == null && txtDate.Text == null)
            {
                MessageBox.Show("Weight Data Missing", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                
                con.Open();
                string waist ="INSERT INTO weight_tbl VALUES ("+ txtID.Text +","+ cboName.SelectedValue.ToString() +",'" + txtDate.Text + "',"+ txtWeight.Text +",'"+ cboTime.SelectedItem.ToString() +"')";
                cmd = new OleDbCommand(waist, con);
                cmd.ExecuteNonQuery();
                con.Close();

                cboTime.SelectedItem = "";
                cboName.SelectedItem = "";
                txtWeight.Text = "";
                MessageBox.Show("Weight Successfully Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            getData();
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


        public void getData()
        {
           
            con.Open();
            string query = "SELECT weight_tbl.ID, animal_tbl.animal_name,  weight_tbl.weight,  weight_tbl.w_time, weight_tbl.w_date FROM animal_tbl INNER JOIN weight_tbl ON animal_tbl.animal_id = weight_tbl.animal_id";
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataWeight.DataSource = dt;
            con.Close();
            getAnimals();
        }

        public void getUpdate()
        {
            con.Open();
            string weight_tbl = "UPDATE weight_tbl SET animal_id="+ cboName.SelectedValue.ToString() +", w_date='" + txtDate.Text + "', weight="+ txtWeight.Text +", w_time='" + cboTime.SelectedItem.ToString() + "' WHERE ID="+ txtID.Text +"";
            cmd = new OleDbCommand(weight_tbl, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Weight Data  Successfully Updated", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);


            lblID.Text = "";
            cboName.SelectedItem = "";
            cboTime.SelectedItem = "";
            txtDate.Text = "";
            getData();
        }

        public void getDelete()
        {
            con.Open();
            string weight_tbl = "DELETE FROM weight_tbl WHERE ID="+ txtID.Text +"";
            cmd = new OleDbCommand(weight_tbl, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Weight Data  Successfully Deleted", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

            lblID.Text = "";
            cboName.SelectedItem = "";
            cboTime.SelectedItem = "";
            txtDate.Text = "";
            getData();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void frmWieght_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            getData();

        }

        private void dataWeight_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataWeight.Rows[e.RowIndex];
                lblID.Text = row.Cells[0].Value.ToString();
                txtID.Text = row.Cells[0].Value.ToString();
                cboName.SelectedItem = row.Cells[1].Value.ToString();
                txtWeight.Text = row.Cells[2].Value.ToString();
                cboTime.SelectedItem = row.Cells[3].Value.ToString();
                txtDate.Text = row.Cells[4].Value.ToString();
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            getWeight();
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
