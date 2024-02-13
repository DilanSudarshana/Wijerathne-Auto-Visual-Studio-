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
    public partial class BrandNewCarsInventory : Form
    {
        ButtonClick buttonClick = new ButtonClick();
        GoBack goBack = new GoBack();
        public BrandNewCarsInventory()
        {
            InitializeComponent();
            ShowMain();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DILA\source\repos\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\WijerathneAuto.mdf;Integrated Security=True");
        int key = 0;
        public void ShowMain()
        {

            con.Open();
            string query = "select * from BNC_Tbl";
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
            txtType.Text = String.Empty;
            txtColor.Text = String.Empty;
            txtEngine.Text = String.Empty;
            txtBPrice.Text = String.Empty;
            cmbBrand.SelectedIndex = 0;
            dtpAddDate.Value.Equals(0);
            cmbTransmission.SelectedIndex = 0;
            txtSPrice.Text = String.Empty;
            key = 0;
        }

        private void btnAddCars_Click(object sender, EventArgs e)
        {
            if (txtYear.Text == "" || cmbBrand.SelectedIndex == -1 || txtColor.Text == "" || cmbTransmission.SelectedIndex == -1 || txtEngine.Text == "" || dtpAddDate.Value.Equals(0) || txtBPrice.Text == "" || txtSPrice.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BNC_Tbl(BNCYear,BNCType,BNCColor,BNCBrand,BNCTransmission,BNCEngine,BNCDate,BNCByingPrice,BNCSellingPrice)values(@yr,@tp,@cl,@br,@tr,@en,@dt,@bpr,@spr)", con);
                    cmd.Parameters.AddWithValue("@yr", txtYear.Text);
                    cmd.Parameters.AddWithValue("@tp", txtType.Text);
                    cmd.Parameters.AddWithValue("@cl", txtColor.Text);
                    cmd.Parameters.AddWithValue("@br", cmbBrand.SelectedItem);
                    cmd.Parameters.AddWithValue("@tr", cmbTransmission.SelectedItem);
                    cmd.Parameters.AddWithValue("@en", txtEngine.Text);
                    cmd.Parameters.AddWithValue("@dt", dtpAddDate.Value.Date);
                    cmd.Parameters.AddWithValue("@bpr", txtBPrice.Text);
                    cmd.Parameters.AddWithValue("@spr", txtSPrice.Text);
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
            if (txtYear.Text == "" || cmbBrand.SelectedIndex == -1 || txtColor.Text == "" || cmbTransmission.SelectedIndex == -1 || txtEngine.Text == "" || dtpAddDate.Value.Equals(0) || txtBPrice.Text == "" || txtSPrice.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update BNC_Tbl Set BNCYear=@yr,BNCType=@ct,BNCColor=@cc,BNCBrand=@cb,BNCTransmission=@tr,BNCEngine=@en,BNCDate=@bdt,BNCByingPrice=@bp,BNCSellingPrice=@sp where BNCID=@CKey", con);

                    cmd.Parameters.AddWithValue("@yr", txtYear.Text);
                    cmd.Parameters.AddWithValue("@ct", txtType.Text);
                    cmd.Parameters.AddWithValue("@cc", txtColor.Text);
                    cmd.Parameters.AddWithValue("@cb", cmbBrand.SelectedItem);
                    cmd.Parameters.AddWithValue("@tr", cmbTransmission.SelectedItem);
                    cmd.Parameters.AddWithValue("@en", txtEngine.Text);
                    cmd.Parameters.AddWithValue("@bdt", dtpAddDate.Value.Date);
                    cmd.Parameters.AddWithValue("@bp", txtBPrice.Text);
                    cmd.Parameters.AddWithValue("@sp", txtSPrice.Text);
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

        private void dgvBrandNewCars_Click(object sender, EventArgs e)
        {
            try
            {

                txtYear.Text = dgvBrandNewCars.SelectedRows[0].Cells[1].Value.ToString();
                txtType.Text = dgvBrandNewCars.SelectedRows[0].Cells[2].Value.ToString();
                txtColor.Text = dgvBrandNewCars.SelectedRows[0].Cells[3].Value.ToString();
                cmbBrand.Text = dgvBrandNewCars.SelectedRows[0].Cells[4].Value.ToString();
                cmbTransmission.Text = dgvBrandNewCars.SelectedRows[0].Cells[5].Value.ToString();
                txtEngine.Text = dgvBrandNewCars.SelectedRows[0].Cells[6].Value.ToString();
                dtpAddDate.Text = dgvBrandNewCars.SelectedRows[0].Cells[7].Value.ToString();
                txtBPrice.Text = dgvBrandNewCars.SelectedRows[0].Cells[8].Value.ToString();
                txtSPrice.Text = dgvBrandNewCars.SelectedRows[0].Cells[9].Value.ToString();

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
                MessageBox.Show("--Select A Car--");
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
                    SqlCommand cmd = new SqlCommand("Delete from BNC_Tbl where BNCID=@Ckey", con);
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

        private void btnDashboad_Click(object sender, EventArgs e)
        {
            buttonClick.Dashboad();
            this.Hide();
        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            buttonClick.Cars();
            this.Hide();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            buttonClick.CustReg();
            this.Hide();
        }

        private void btnSupplires_Click(object sender, EventArgs e)
        {
            buttonClick.SupReg();
            this.Hide();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            buttonClick.staffreg();
            this.Hide();
        }

        private void btnSalesManagment_Click(object sender, EventArgs e)
        {
            buttonClick.Sales();
            this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            
            buttonClick.LogOut();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            goBack.goBack();
            this.Hide();
        }
    }
}
