using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Zoo_Sys_ADI
{
    public partial class frmExercises : Form
    {
        public frmExercises()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|ADI.mdb");
        OleDbCommand cmd = new OleDbCommand();

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void getData()
        {
            con.Open();
            string query = "SELECT exercise_tbl.ex_id, exercise_tbl.ex_type, animal_tbl.animal_name, exercise_tbl.ex_minutes FROM animal_tbl INNER JOIN exercise_tbl ON animal_tbl.animal_id = exercise_tbl.animal_id";
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataExe.DataSource = dt;
            con.Close();
            getAnimals();
            getType();
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

        public void getType()
        {
            con.Open();
            string query = "SELECT ex_type FROM ex_type_tbl";
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cboType.DataSource = dt;
            cboType.ValueMember = "ex_type";
            cboType.DisplayMember = "ex_type";
            con.Close();
        }

        private void frmExercises_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            getData();
        }


        public void getExercise()
        {
            if (cboType.SelectedItem == null && cboName.SelectedItem == null && txtTime.Text == null)
            {
                MessageBox.Show("Exercise Data Missing", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                con.Open();
                string exe = "INSERT INTO exercise_tbl VALUES ("+ txtID.Text +",'"+ cboType.SelectedValue.ToString() +"',"+ cboName.SelectedValue.ToString() +","+ txtTime.Text +")";
                cmd = new OleDbCommand(exe, con);
                cmd.ExecuteNonQuery();
                con.Close();

                cboType.SelectedItem = "";
                cboName.SelectedItem = "";
                txtTime.Text = "";
                MessageBox.Show("Exercise Successfully Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            getData();
        }

        public void getUpdate()
        {
            con.Open();
            string exercise_tbl = "UPDATE exercise_tbl SET  ex_type='"+ cboType.SelectedValue.ToString() +"', animal_id="+ cboName.SelectedValue.ToString() +", ex_minutes="+ txtTime.Text +" WHERE ex_id=" + txtID.Text + "";
            cmd = new OleDbCommand(exercise_tbl, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Exercise Successfully Updated", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            cboType.SelectedItem = "";
            cboName.SelectedItem = "";
            txtTime.Text = "";
            getData();
        }


        public void getDelete()
        {
            con.Open();
            string exercise_tbl = "DELETE FROM exercise_tbl  WHERE ex_id="+ txtID.Text +"";
            cmd = new OleDbCommand(exercise_tbl, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Exercise Successfully Deleted", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            cboType.SelectedItem = "";
            cboName.SelectedItem = "";
            txtTime.Text = "";
            getData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            getUpdate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            getDelete();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            getExercise();
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

        private void dataExe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataExe.Rows[e.RowIndex];
                txtID.Text = row.Cells[0].Value.ToString();
                cboType.SelectedItem = row.Cells[1].Value.ToString();
                cboName.SelectedItem = row.Cells[2].Value.ToString();
                txtTime.Text = row.Cells[3].Value.ToString();
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }
    }
}
