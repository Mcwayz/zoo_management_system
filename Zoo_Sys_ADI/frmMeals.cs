using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Zoo_Sys_ADI
{
    public partial class frmMeals : Form
    {
        public frmMeals()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|ADI.mdb");
        OleDbCommand cmd = new OleDbCommand();

        public void update()
        {
            con.Open();
            string meals = "UPDATE meal_tbl SET meal_name='" + txtName.Text + "', calories_info="+ txtCalories.Text +", portion_size="+ txtSize.Text +" WHERE meal_id="+ txtCode.Text +"";
            cmd = new OleDbCommand(meals, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Meal Successfully Updated", "Modification Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtCode.Text = "";
            txtName.Text = "";
            txtCalories.Text = "";
            txtSize.Text = "";
            getData();
        }

        public void getData()
        {
            // TODO: This line of code loads data into the 'aDIDataSet.meal_tbl' table. You can move, or remove it, as needed.
            this.meal_tblTableAdapter.Fill(this.aDIDataSet.meal_tbl);

        }

        public void delete()
        {
            con.Open();
            string meal = "DELETE FROM meal_tbl WHERE meal_id=" + txtCode.Text + "";
            cmd = new OleDbCommand(meal, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Meal Successfully Deleted!", "Deletion Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            getData();
        }

        public void addMeal()
        {
            if (txtCode.Text == null && txtName.Text == null  && txtCalories.Text == null && txtSize.Text == null)
            {
                MessageBox.Show("Meal Data Missing", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                con.Open();
                string animal = "INSERT INTO meal_tbl VALUES ("+ txtCode.Text +",'"+ txtName.Text +"',"+ txtCalories.Text +","+ txtSize.Text +")";
                cmd = new OleDbCommand(animal, con);
                cmd.ExecuteNonQuery();
                con.Close();

                txtCode.Text = "";
                txtName.Text = "";
                txtCalories.Text = "";
                txtSize.Text = "";
                MessageBox.Show("Meal Successfully Added", "Addition Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            getData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataMeal.Rows[e.RowIndex];
                txtCode.Text = row.Cells[0].Value.ToString();
                txtName.Text = row.Cells[1].Value.ToString();
                txtCalories.Text = row.Cells[2].Value.ToString();
                txtSize.Text = row.Cells[3].Value.ToString();
               
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addMeal();
        }

        private void frmMeals_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            getData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            update();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            new frmDash().Show();
            this.Hide();
        }
    }
}
