using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Zoo_Sys_ADI
{
    public partial class frmFeed : Form
    {
        public frmFeed()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|ADI.mdb");
        OleDbCommand cmd = new OleDbCommand();
        public void update()
        {
            con.Open();
            string feed = "UPDATE feed_tbl SET  meal_id='" + cboMeal.SelectedValue.ToString() + "', animal_id='" + cboAnimal.SelectedValue.ToString() + "', f_date='" + txtDate.Text + "' WHERE feed_id='" + txtID.Text + "'";
            cmd = new OleDbCommand(feed, con);
            cmd.ExecuteNonQuery();
            con.Close();

            txtID.Text = "";
            cboMeal.SelectedItem = "";
            cboAnimal.SelectedItem = "";
            txtDate.Text = "";
            getData();
        }
        public void getData()
        {
            con.Open();
            string query = "SELECT feed_tbl.feed_id, meal_tbl.meal_name, animal_tbl.animal_name, feed_tbl.f_date FROM meal_tbl INNER JOIN(animal_tbl INNER JOIN feed_tbl ON animal_tbl.animal_id = feed_tbl.animal_id) ON meal_tbl.meal_id = feed_tbl.meal_id";
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataFeed.DataSource = dt;
            con.Close();
        }

        public void delete()
        {
            con.Open();
            string feed = "DELETE FROM feed_tbl WHERE feed_id='" + txtID.Text + "'";
            cmd = new OleDbCommand(feed, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Deleted!", "Deletion Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            getData();
        }

        public void addFeed()
        {
            if (txtID.Text == null && cboMeal.SelectedIndex == 0 && cboAnimal.SelectedIndex == 0)
            {
                MessageBox.Show("Feeding Data's Missing", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                con.Open();
                string feed = "INSERT INTO feed_tbl VALUES ('" + txtID.Text + "','" + cboMeal.SelectedValue.ToString() + "','" + cboAnimal.SelectedValue.ToString() + "','" + txtDate.Text + "')";
                cmd = new OleDbCommand(feed, con);
                cmd.ExecuteNonQuery();
                con.Close();

                txtID.Text = "";
                cboAnimal.SelectedIndex = 0;
                cboMeal.SelectedIndex = 0;
                MessageBox.Show("Animal Successfully Fed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            getData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addFeed();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            update();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataFeed.Rows[e.RowIndex];
                txtID.Text = row.Cells[0].Value.ToString();
                cboMeal.SelectedItem = row.Cells[1].Value.ToString();
                cboAnimal.SelectedItem = row.Cells[2].Value.ToString();
                txtDate.Text = row.Cells[3].Value.ToString();
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

        private void frmFeed_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            getData();
        }
    }
}
