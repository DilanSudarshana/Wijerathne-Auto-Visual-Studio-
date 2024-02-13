using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace E2140139_Sudarshana_GDL_ITE_1942_ICT_Project
{
    public partial class DashBoad : Form
    {
        ButtonClick buttonClick=new ButtonClick();
        public DashBoad()
        {
            InitializeComponent();
            ShowCust();
            ShowSup();
            ShowBNCars();
            ShowUsedCars();
            BestCustomer();
            TotalSelling();
            MostDemanding();
            ShowNBRCars();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DILA\source\repos\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\WijerathneAuto.mdf;Integrated Security=True");
        double TotalCost = 0;
        double TotalSell = 0;
        
        private void ShowCust()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from Customer_tbl", con);
            DataTable dt=new DataTable();
            sda.Fill(dt);
            lblCustomers.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void ShowSup()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from Suppliers_Tbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            lblSup.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void ShowBNCars()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from BNC_Tbl", con);
            DataTable dtt = new DataTable();
            sda.Fill(dtt);
            lblBNCars.Text = dtt.Rows[0][0].ToString();
            con.Close();
        }
        private void ShowUsedCars()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from UCars_Tbl", con);
            DataTable dtt = new DataTable();
            sda.Fill(dtt);
            lblUsed.Text = dtt.Rows[0][0].ToString();
            con.Close();
        }
        private void ShowNBRCars()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from CNBR_tbl", con);
            DataTable dtt = new DataTable();
            sda.Fill(dtt);
            lblNBR_Cars.Text = dtt.Rows[0][0].ToString();
            con.Close();
        }
        private void BestCustomer()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT TOP 1 CAST(Cust_Name AS nvarchar(100)) AS [CUSTNAME],COUNT(*) AS [CUST_COUNT] FROM Sold_Cars_Tbl GROUP BY (CAST(Cust_Name AS nvarchar(100))) ORDER BY CUST_COUNT DESC", con);
            DataTable dtt = new DataTable();
            sda.Fill(dtt);
            lblBestCust.Text = dtt.Rows[0][0].ToString();
            con.Close();
        }
        private void TotalSelling()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(Total) from Sold_Cars_Tbl", con);
            DataTable dtt = new DataTable();
            sda.Fill(dtt);
            lblTotalSelling.Text =dtt.Rows[0][0].ToString();
            double Total = Convert.ToDouble(lblTotalSelling.Text);
            double sum = Total / 100000;
            double sum1=Math.Round(sum);
            lblTotalSelling.Text = sum1.ToString()+" LAKH";
            con.Close();
        }
        private void MostDemanding()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT TOP 1 CAST(Brand AS nvarchar(100)) AS [model],COUNT(*) AS [car_count] FROM Sold_Cars_Tbl GROUP BY (CAST(Brand AS nvarchar(100))) ORDER BY car_count DESC", con);
            DataTable dtt = new DataTable();
            sda.Fill(dtt);
            lblMostD.Text = dtt.Rows[0][0].ToString();
            con.Close();
        }

        private void btnDashBoad_Click(object sender, EventArgs e)
        {
            buttonClick.Dashboad(); this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonClick.Cars(); this.Hide();
        }

        private void btnCust_Click(object sender, EventArgs e)
        {
            buttonClick.CustReg(); this.Hide();
        }

        private void btnSup_Click(object sender, EventArgs e)
        {
            buttonClick.SupReg(); this.Hide();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            buttonClick.staffreg(); this.Hide();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            buttonClick.Sales(); this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            buttonClick.LogOut(); this.Hide();
        }

        private void btnGenerateBill_Click(object sender, EventArgs e)
        {
            GntTotalSelling();
            gntBestCust();
            gntMostDemading();
            gntTotalCost();
            gntLeastDemanding();
            gntTotalProfit();
        }
        public void GntTotalSelling ()
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select Sum(Total) from Sold_Cars_Tbl where CAST(Date AS date) >'" + dtpFrom.Value.ToString() + "' AND CAST(Date AS date)<'" + dtpTo.Value.ToString() + "'", con);
                DataTable dtt = new DataTable();
                sda.Fill(dtt);
                TotalSell = double.Parse(dtt.Rows[0][0].ToString());
                double TotalSellLack = TotalSell / 100000;
                lblTSoP.Text = TotalSellLack.ToString()+" LAKH";
                con.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        public void gntBestCust()
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT TOP 1 CAST(Cust_Name AS nvarchar(100)) AS [name],COUNT(*) AS [cust_count] FROM Sold_Cars_Tbl where CAST(Date AS date) >'" + dtpFrom.Value.ToString() + "' AND CAST(Date AS date)<'" + dtpTo.Value.ToString() + "' GROUP BY (CAST(Cust_Name AS nvarchar(100))) ORDER BY cust_count DESC", con);
                DataTable dtt = new DataTable();
                sda.Fill(dtt);
                lblBCoP.Text = dtt.Rows[0][0].ToString();
                con.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        public void gntMostDemading() {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT TOP 1 CAST(Brand AS nvarchar(100)) AS [model],COUNT(*) AS [car_count] FROM Sold_Cars_Tbl where CAST(Date AS date) >'" + dtpFrom.Value.ToString() + "' AND CAST(Date AS date)<'" + dtpTo.Value.ToString() + "' GROUP BY (CAST(Brand AS nvarchar(100))) ORDER BY car_count DESC", con);
                DataTable dtt = new DataTable();
                sda.Fill(dtt);
                lblMDCoP.Text = dtt.Rows[0][0].ToString();
                con.Close();

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        public void gntTotalCost()
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select Sum(BuyingPrice) from Sold_Cars_Tbl where CAST(Date AS date) >'" + dtpFrom.Value.ToString() + "' AND CAST(Date AS date)<'" + dtpTo.Value.ToString() + "'", con);
                DataTable dtt = new DataTable();
                sda.Fill(dtt);
                TotalCost = double.Parse(dtt.Rows[0][0].ToString());
                double TotalCostLack = TotalCost / 100000;
                lblTCoP.Text = TotalCostLack.ToString()+" LAKH";
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void gntLeastDemanding()
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT TOP 1 CAST(Brand AS nvarchar(100)) AS [model],COUNT(*) AS [car_count] FROM Sold_Cars_Tbl where CAST(Date AS date) >'" + dtpFrom.Value.ToString() + "' AND CAST(Date AS date)<'" + dtpTo.Value.ToString() + "' GROUP BY (CAST(Brand AS nvarchar(100))) ORDER BY car_count ASC", con);
                DataTable dtt = new DataTable();
                sda.Fill(dtt);
                lblLDCoP.Text = dtt.Rows[0][0].ToString();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void gntTotalProfit()
        {
            try
            {
                double tSell = TotalSell;
                double tCost = TotalCost;
                double tProfit = (TotalSell - TotalCost)/100000;
                lblTPoP.Text = tProfit.ToString()+" LAKH";

                chart1.Series["Sales"].Points.AddXY( "Selling",tSell);
                chart1.Series["Sales"].Points.AddXY( "Cost",tCost);
                chart1.Series["Sales"].Points.AddXY( "Pfofit",tProfit * 100000);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSCD_Click(object sender, EventArgs e)
        {
            SoldCarDetails scd=new SoldCarDetails();
            scd.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dtpFrom.ResetText();
            dtpTo.ResetText();
            lblTCoP.Text = "";
            lblTSoP.Text = "";
            lblMDCoP.Text = "";
            lblTPoP.Text = "";
            lblLDCoP.Text = "";
            lblBCoP.Text = "";

            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
        }
        
    }
}
