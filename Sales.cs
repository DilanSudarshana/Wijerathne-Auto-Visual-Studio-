using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E2140139_Sudarshana_GDL_ITE_1942_ICT_Project
{
    public partial class Sales : Form
    {
        ButtonClick buttonClick = new ButtonClick();
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DILA\source\repos\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\WijerathneAuto.mdf;Integrated Security=True");
        public Sales()
        {
            InitializeComponent();
            CustormersDetails();
        }

        
        int key = 0,stock;
        double Stock;
        int n = 0;

        public void AddSoldCars()
        {
            try
            {
                double buyPrice = 0;
                buyPrice = double.Parse(dgvCarStock.Rows[0].Cells[8].Value.ToString());

                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Sold_Cars_Tbl(Bill_ID,Date,Cust_ID,Cust_Name,Car_ID,Year,Model,Transmission,Engine,Brand,Price,Chargers,Discount_Price,Total,BuyingPrice,CarCategory)values(@bi,@da,@ci,@cn,@cai,@ye,@mo,@tr,@en,@br,@pr,@ch,@dis,@tot,@bp,@cc)", con);

                cmd.Parameters.AddWithValue("@bi", txtCarID.Text);
                cmd.Parameters.AddWithValue("@da", dtpBillDate.Value.Date);
                cmd.Parameters.AddWithValue("@ci", txtCustID.Text);
                cmd.Parameters.AddWithValue("@cn", txtCustName.Text);
                cmd.Parameters.AddWithValue("@cai", txtCarID.Text);
                cmd.Parameters.AddWithValue("@ye", txtYear.Text);
                cmd.Parameters.AddWithValue("@mo", txtModel.Text);
                cmd.Parameters.AddWithValue("@tr", txtTrans.Text);
                cmd.Parameters.AddWithValue("@en", txtEngine.Text);
                cmd.Parameters.AddWithValue("@br", txtCarBrand.Text);
                cmd.Parameters.AddWithValue("@pr", txtPrice.Text);
                cmd.Parameters.AddWithValue("@ch", txtChargers.Text);
                cmd.Parameters.AddWithValue("@dis", txtDeducations.Text);
                cmd.Parameters.AddWithValue("@tot", lblTotal.Text);
                cmd.Parameters.AddWithValue("@bp", buyPrice);
                cmd.Parameters.AddWithValue("@cc", cmbCarType.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data saved into sold car list");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
 
        public void ClearDetails() {
            n = 0;
            key = 0;
            txtCarID.Text = "";
            txtCarBrand.Text = "";
            txtChargers.Text = "";
            txtCustID.Text = "";
            txtCustName.Text = "";
            txtDeducations.Text = "";
            txtEngine.Text = "";
            txtID.Text = "";
            txtModel.Text = "";
            txtPrice.Text = "";
            txtTrans.Text = "";
            txtYear.Text = "";
            
        }
        public void CustormersDetails()
        {
            try
            {
                con.Open();
                string query = "select * from Customer_tbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                dgvCustormers.DataSource = ds.Tables[0];
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAddToBill_Click(object sender, EventArgs e)
        {
            

            if (txtPrice.Text == ""||txtDeducations.Text==""||txtChargers.Text==""||txtCustID.Text==""||txtCustName.Text=="")
            {
                MessageBox.Show("Enter Current Details about Cars and Custermers");
            }
            else
            {
                try
                {
                    double deducation = double.Parse(txtDeducations.Text);
                    double chargers = double.Parse(txtChargers.Text);
                    double Total = double.Parse(txtPrice.Text);
                    double discountP = (Total / 100) * deducation;
                    double total = (Total + chargers) - discountP;
                    lblTotal.Text = total.ToString();

                    DataGridViewRow newrow = new DataGridViewRow();
                    newrow.CreateCells(dgvBill);

                    newrow.Cells[0].Value = txtCarID.Text ;

                    newrow.Cells[1].Value = dtpBillDate.Text;
                    newrow.Cells[2].Value = txtCustID.Text;
                    newrow.Cells[3].Value = txtCustName.Text;

                    newrow.Cells[4].Value = txtCarID.Text;
                    newrow.Cells[5].Value = txtYear.Text;
                    newrow.Cells[6].Value = txtModel.Text;
                    newrow.Cells[7].Value = txtTrans.Text;
                    newrow.Cells[8].Value = txtEngine.Text;
                    newrow.Cells[9].Value = txtCarBrand.Text;

                    newrow.Cells[10].Value = txtPrice.Text;
                    newrow.Cells[11].Value = txtChargers.Text;
                    newrow.Cells[12].Value = discountP;
                    newrow.Cells[13].Value = total;
                    newrow.Cells[14].Value = cmbCarType.SelectedItem.ToString();
                    dgvBill.Rows.Add(newrow);
                    n++;

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawString("Wijerathne Auto", new Font("Century gothic", 16, FontStyle.Bold), Brushes.Black, new Point(70));
                e.Graphics.DrawString("N0.119", new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(120, 25));
                e.Graphics.DrawString("ByPass Road", new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(120, 35));
                e.Graphics.DrawString("Piliyandala", new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(120, 45));
                e.Graphics.DrawString("_____________________________________________________________________", new Font("Century gothic", 7, FontStyle.Regular), Brushes.Black, new Point(0, 50));
                e.Graphics.DrawString("Total Bill", new Font("Century gothic", 8, FontStyle.Regular), Brushes.Black, new Point(26, 70));



                foreach (DataGridViewRow row in dgvBill.Rows)
                {
                    string BillID = Convert.ToString(row.Cells[0].Value);
                    string Date = Convert.ToString(row.Cells[1].Value);
                    string CustID = Convert.ToString(row.Cells[2].Value);
                    string CustName = Convert.ToString(row.Cells[3].Value);
                    string CarID = Convert.ToString(row.Cells[4].Value);

                    string Year = Convert.ToString(row.Cells[5].Value);
                    string model = Convert.ToString(row.Cells[6].Value);
                    string trans = Convert.ToString(row.Cells[7].Value);
                    string engin = Convert.ToString(row.Cells[8].Value);
                    string brand = Convert.ToString(row.Cells[9].Value);

                    string price = Convert.ToString(row.Cells[10].Value);
                    string chargers = Convert.ToString(row.Cells[11].Value);
                    string discountP = Convert.ToString(row.Cells[12].Value);
                    string total = Convert.ToString(row.Cells[13].Value);
                    string carCategory = Convert.ToString(row.Cells[14].Value);

                    e.Graphics.DrawString("-Bill ID: ", new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 100));
                    e.Graphics.DrawString( BillID, new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 100));

                    e.Graphics.DrawString("-Date: ", new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 120));
                    e.Graphics.DrawString( Date, new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 120));

                    e.Graphics.DrawString("-Customer ID: " , new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 140));
                    e.Graphics.DrawString(CustID, new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 140));

                    e.Graphics.DrawString("-Customer Name: " , new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 160));
                    e.Graphics.DrawString(CustName , new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 160));

                    e.Graphics.DrawString("-Car ID : " , new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 180));
                    e.Graphics.DrawString( CarID, new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 180));

                    e.Graphics.DrawString("-Year Of Manufactured: " , new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 200));
                    e.Graphics.DrawString( Year, new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 200));

                    e.Graphics.DrawString("-Car Model Name: " , new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 220));
                    e.Graphics.DrawString( model, new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 220));

                    e.Graphics.DrawString("-Transmission: " , new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 240));
                    e.Graphics.DrawString( trans, new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 240));

                    e.Graphics.DrawString("-Engine CC : ", new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 260));
                    e.Graphics.DrawString(engin, new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 260));

                    e.Graphics.DrawString("-Car Brand : " , new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 280));
                    e.Graphics.DrawString( brand, new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 280));


                    e.Graphics.DrawString("-Car Price Rs. : " , new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 300));
                    e.Graphics.DrawString( price , new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 300));
                    

                    e.Graphics.DrawString("-Chargers Rs. : " , new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 320));
                    e.Graphics.DrawString( chargers , new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 320));

                    e.Graphics.DrawString("-Discount Price Rs. : " , new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 340));
                    e.Graphics.DrawString(discountP, new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 340));

                    e.Graphics.DrawString("-Total Price Rs. : " , new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 360));
                    e.Graphics.DrawString( total, new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 360));

                    e.Graphics.DrawString("-Car Category : ", new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(26, 380));
                    e.Graphics.DrawString(carCategory, new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(140, 380));


                }
                e.Graphics.DrawString("WIJERATHNE AUTO", new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(100, 400));
                e.Graphics.DrawString("*****  Thank You !!!  *****", new Font("Century gothic", 6, FontStyle.Regular), Brushes.Black, new Point(90, 420));

                dgvBill.Rows.Clear();
                dgvBill.Refresh();

                //Delete items in car inventory database
                if (cmbCarType.SelectedItem != "BrandNewCars")
                {
                    if (cmbCarType.SelectedItem == "UsedCars")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Delete from UCars_Tbl where UCID=@Ckey", con);
                        cmd.Parameters.AddWithValue("@Ckey", key);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        key = 0;
                    }
                    else
                    {
                        MessageBox.Show("Data Updated");
                    }
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from BNC_Tbl where BNCID=@Ckey", con);
                    cmd.Parameters.AddWithValue("@Ckey", key);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    key = 0;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtDeducations.Text == "" || txtChargers.Text == "" || txtCustID.Text == "" || txtCustName.Text == "" || dtpBillDate.Value.Equals(0) || cmbCarType.SelectedIndex == -1 ) {
                MessageBox.Show("Please select and enter the current details");
            }
            else
            {
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 440);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
                try
                {
                    AddSoldCars();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ClearDetails();

            }
           
        }

        private void btnDashboad_Click(object sender, EventArgs e)
        {
            buttonClick.Dashboad();this.Hide();
        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            buttonClick.Cars(); this.Hide();
        }

        private void btnCustReg_Click(object sender, EventArgs e)
        {
            buttonClick.CustReg(); this.Hide();
        }

        private void btnSupReg_Click(object sender, EventArgs e)
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

        private void cmbCarType_MouseClick(object sender, MouseEventArgs e)
        {
            //
        }

        private void cmbCarType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCarType.SelectedItem != "BrandNewCars")
            {
                if (cmbCarType.SelectedItem == "UsedCars")
                {
                    con.Open();
                    string query = "select * from UCars_Tbl";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    dgvCarStock.DataSource = ds.Tables[0];
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Select Car Category");
                }
            }
            else
            {
                con.Open();
                string query = "select * from BNC_Tbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                dgvCarStock.DataSource = ds.Tables[0];
                con.Close();
            }
        }

        private void dgvCustormers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtCustID.Text = dgvCustormers.SelectedRows[0].Cells[0].Value.ToString();
                txtCustName.Text = dgvCustormers.SelectedRows[0].Cells[2].Value.ToString();
                dtpBillDate.Text = dgvCustormers.SelectedRows[0].Cells[5].Value.ToString();
                
                if (txtCarID.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(dgvCarStock.SelectedRows[0].Cells[0].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select a Car");
            }
        }

        private void dgvCarStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtCarID.Text = dgvCarStock.SelectedRows[0].Cells[0].Value.ToString();
                txtYear.Text = dgvCarStock.SelectedRows[0].Cells[1].Value.ToString();
                txtModel.Text = dgvCarStock.SelectedRows[0].Cells[2].Value.ToString();
                txtTrans.Text = dgvCarStock.SelectedRows[0].Cells[5].Value.ToString();
                txtEngine.Text = dgvCarStock.SelectedRows[0].Cells[6].Value.ToString();
                txtCarBrand.Text = dgvCarStock.SelectedRows[0].Cells[4].Value.ToString();
                txtPrice.Text = dgvCarStock.SelectedRows[0].Cells[9].Value.ToString();
                stock = Convert.ToInt32(dgvCarStock.SelectedRows[0].Cells[9].Value.ToString());
                Stock = stock;

                if (txtCarBrand.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(txtID.Text = dgvCarStock.SelectedRows[0].Cells[0].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select the customer");
            }
        }
    }
}
