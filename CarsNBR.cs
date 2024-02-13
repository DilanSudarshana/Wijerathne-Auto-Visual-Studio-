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

namespace E2140139_Sudarshana_GDL_ITE_1942_ICT_Project
{
    public partial class CarsNBR : Form
    {
        public CarsNBR()
        {
            InitializeComponent();
            ShowMain();
        }
        GoBack goBack = new GoBack();
        ButtonClick buttonClick = new ButtonClick();
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DILA\source\repos\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\WijerathneAuto.mdf;Integrated Security=True");
        int key = 0;

        public void ShowMain()
        {

            con.Open();
            string query = "select * from CNBR_tbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgvBrandNewCars.DataSource = ds.Tables[0];
            con.Close();
        }
        public void Clear()
        {

            txtYear.Text = String.Empty;
            txtBPrice.Text = String.Empty;
            txtRCost.Text = String.Empty;
            txtTotalRCost.Text = String.Empty;  
            cmbBrand.SelectedIndex = 0;
            dtpAddDate.Value.Equals(0);  
            key = 0;

        }
       
        private void btnAddCars_Click(object sender, EventArgs e)
        {
            if (txtYear.Text == "" || cmbBrand.SelectedIndex == -1 || txtBPrice.Text == "" || cmbBrand.SelectedIndex == -1 || txtModel.Text == "" || dtpAddDate.Value.Equals(0) || txtBPrice.Text == "" || txtRCost.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CNBR_tbl(YOM,Brand,Model,BPrice,RCost,Date)values(@yr,@br,@md,@bp,@rc,@dt)", con);
                    cmd.Parameters.AddWithValue("@yr", txtYear.Text);
                    cmd.Parameters.AddWithValue("@br", cmbBrand.SelectedItem);
                    cmd.Parameters.AddWithValue("@md", txtModel.Text);
                    cmd.Parameters.AddWithValue("@bp", txtBPrice.Text);
                    cmd.Parameters.AddWithValue("@rc", txtRCost.Text);
                    cmd.Parameters.AddWithValue("@dt", dtpAddDate.Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Saved");
                    con.Close();
                    ShowMain();
                    Clear();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUpdateCars_Click(object sender, EventArgs e)
        {
            if (txtYear.Text == "" || cmbBrand.SelectedIndex == -1 || txtBPrice.Text == "" || cmbBrand.SelectedIndex == -1 || txtModel.Text == "" || dtpAddDate.Value.Equals(0) || txtBPrice.Text == "" || txtRCost.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update CNBR_tbl Set YOM=@yr,Brand=@br,Model=@md,BPrice=@bp,RCost=@rc,Date=@dt where CarID=@CKey", con);
                    cmd.Parameters.AddWithValue("@yr", txtYear.Text);
                    cmd.Parameters.AddWithValue("@br", cmbBrand.SelectedItem);
                    cmd.Parameters.AddWithValue("@md", txtModel.Text);
                    cmd.Parameters.AddWithValue("@bp", txtBPrice.Text);
                    cmd.Parameters.AddWithValue("@rc", txtRCost.Text);
                    cmd.Parameters.AddWithValue("@dt", dtpAddDate.Value);
                    cmd.Parameters.AddWithValue("@Ckey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Updated");
                    con.Close();
                    ShowMain();
                    Clear();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgvBrandNewCars_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //dgv for need to be repaired cars
                txtYear.Text = dgvBrandNewCars.SelectedRows[0].Cells[1].Value.ToString();
                cmbBrand.Text = dgvBrandNewCars.SelectedRows[0].Cells[2].Value.ToString();
                txtModel.Text = dgvBrandNewCars.SelectedRows[0].Cells[3].Value.ToString();
                txtBPrice.Text = dgvBrandNewCars.SelectedRows[0].Cells[4].Value.ToString();
                txtRCost.Text = dgvBrandNewCars.SelectedRows[0].Cells[5].Value.ToString();
                dtpAddDate.Text = dgvBrandNewCars.SelectedRows[0].Cells[6].Value.ToString();
                

                if (txtYear.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(dgvBrandNewCars.SelectedRows[0].Cells[0].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select a Car");
            }
        }

        private void btnDeleteCars_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the customers");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from CNBR_tbl where CarID=@Ckey", con);
                    cmd.Parameters.AddWithValue("@Ckey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("The Car was successefully Deleted");
                    con.Close();
                    ShowMain();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnAddToSale_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtYear.Text == "" || cmbBrand.SelectedIndex == -1 || txtBPrice.Text == "" || cmbBrand.SelectedIndex == -1 || txtModel.Text == "" || dtpAddDate.Value.Equals(0) || txtBPrice.Text == "" || txtRCost.Text == "")
                {
                    MessageBox.Show("Select a car for add to the sale");
                }
                else
                {
                    if (txtTotalRCost.Text != "")
                    {
                        double carBPrice = double.Parse(txtBPrice.Text);
                        double TotalPrice;
                        double TotalRCost = double.Parse(txtTotalRCost.Text);
                        //Calculate total car Price
                        TotalPrice = (carBPrice) + ((carBPrice / 100) * 20) + TotalRCost;
                        lblTotal.Text = TotalPrice.ToString();

                        con.Open();
                        SqlCommand cmd = new SqlCommand("insert into UCars_Tbl(UCYear,UCBrand,UCModel,UCByingPrice,UCSellingPrice,UCDate)values(@yr,@br,@mo,@bp,@sp,@dt)", con);
                        cmd.Parameters.AddWithValue("@yr", txtYear.Text);
                        cmd.Parameters.AddWithValue("@br", cmbBrand.SelectedItem);
                        cmd.Parameters.AddWithValue("@mo", txtModel.Text);
                        cmd.Parameters.AddWithValue("@bp", txtBPrice.Text);
                        cmd.Parameters.AddWithValue("@sp", lblTotal.Text);
                        cmd.Parameters.AddWithValue("@dt", dtpAddDate.Value.Date);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Car Add to The Used Car Inventory_Please Update Details and car Total Price is Rs. "+TotalPrice);
                        con.Close();


                        con.Open();
                        SqlCommand cmd1 = new SqlCommand("Delete from CNBR_tbl where CarID=@Ckey", con);
                        cmd1.Parameters.AddWithValue("@Ckey", key);
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        key = 0;
                        ShowMain();
                        Clear();

                    }
                    else
                    {
                        MessageBox.Show("Please Enter Total repair Cost");
                    }
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDashboad_Click(object sender, EventArgs e)
        {
            buttonClick.Dashboad();
            this.Hide();

        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            buttonClick.Cars(); this.Hide();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            buttonClick.CustReg();this.Hide();
        }

        private void btnSupplires_Click(object sender, EventArgs e)
        {
            buttonClick.SupReg(); this.Hide();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            buttonClick.staffreg(); this.Hide();
        }

        private void btnSalesManagment_Click(object sender, EventArgs e)
        {
            buttonClick.Sales(); this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            buttonClick.LogOut(); this.Hide();
        }

        private void lblGoBack_Click(object sender, EventArgs e)
        {
            goBack.goBack(); this.Hide();
        }
    }
}
