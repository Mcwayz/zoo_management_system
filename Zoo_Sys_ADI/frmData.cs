using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Zoo_Sys_ADI
{
    public partial class frmData : Form
    {
        public frmData()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|ADI.mdb");
        OleDbCommand cmd = new OleDbCommand();

        public void chartLoad()
        {
            try
            {
                var chart = chart1.ChartAreas[0];
                chart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
                chart.AxisX.LabelStyle.Format = "";
                chart.AxisY.LabelStyle.Format = "";
                chart.AxisX.LabelStyle.IsEndLabelVisible = true;

                chart.AxisX.Minimum = 0;
                chart.AxisY.Minimum = 0;

                chart.AxisY.Interval = 2;
                chart.AxisY.Interval = 15;

                chart1.Series[0].IsVisibleInLegend = false;

                chart1.Series.Add("Weight");
                chart1.Series["Weight"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["Weight"].Color = Color.Green;
           
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                string query = "SELECT animal_tbl.animal_name, weight_tbl.weight FROM animal_tbl INNER JOIN weight_tbl ON animal_tbl.animal_id = weight_tbl.animal_id";
                cmd.CommandText = query;

                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    chart1.Series["Weight"].Points.AddXY(reader["animal_name"], reader["Weight"]);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex, "Chart Retrival Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void frmData_Load(object sender, EventArgs e)
        {
            chartLoad();
        }
    }
}
