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
    public partial class SuppliersRegistration : Form
    {
        public SuppliersRegistration()
        {
            InitializeComponent();
            Showmain();
        }
        ButtonClick buttonClick = new ButtonClick();
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DILA\source\repos\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\WijerathneAuto.mdf;Integrated Security=True");
        int key = 0;
        public void Showmain()
        {

            con.Open();
            string query = "select * from Suppliers_Tbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgvSuppliers.DataSource = ds.Tables[0];
            con.Close();
        }
        public void Clear()
        {
           
            txtComName.Text = String.Empty;
            txtSupName.Text = String.Empty;
            txtSupEmail.Text = String.Empty;
            txtSupPhone.Text = String.Empty;
            txtComAdress.Text = String.Empty;
            dtpSupDate.Value.Equals(0);
            key = 0;

        }

        private void btnSupReg_Click(object sender, EventArgs e)
        {
            if (txtComName.Text == "" || txtSupName.Text == "" || txtSupEmail.Text == "" || txtSupPhone.Text == "" || txtComAdress.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Suppliers_Tbl(CompanyName,Name,Email,Phone,JoinDate,Adress)values(@cn,@na,@em,@ph,@dt,@ad)", con);
                   
                    cmd.Parameters.AddWithValue("@cn", txtComName.Text);
                    cmd.Parameters.AddWithValue("@na", txtSupName.Text);
                    cmd.Parameters.AddWithValue("@em", txtSupEmail.Text);
                    cmd.Parameters.AddWithValue("@ph", txtSupPhone.Text);
                    cmd.Parameters.AddWithValue("@dt", dtpSupDate.Value.Date);
                    cmd.Parameters.AddWithValue("@ad", txtComAdress.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Saved");
                    con.Close();
                    Showmain();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
              
                txtComName.Text = dgvSuppliers.SelectedRows[0].Cells[1].Value.ToString();
                txtSupName.Text = dgvSuppliers.SelectedRows[0].Cells[2].Value.ToString();
                txtSupEmail.Text = dgvSuppliers.SelectedRows[0].Cells[3].Value.ToString();
                txtSupPhone.Text = dgvSuppliers.SelectedRows[0].Cells[4].Value.ToString();
                dtpSupDate.Text = dgvSuppliers.SelectedRows[0].Cells[5].Value.ToString();
                txtComAdress.Text = dgvSuppliers.SelectedRows[0].Cells[6].Value.ToString();
               

                if (txtComName.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(dgvSuppliers.SelectedRows[0].Cells[0].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select a Supplier");
            }
        }

        private void btnSupUpd_Click(object sender, EventArgs e)
        {
            if (txtComName.Text == "" || txtSupName.Text == "" || txtSupEmail.Text == "" || txtSupPhone.Text == "" || txtComAdress.Text == ""||dtpSupDate.Value.Equals(0))
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update Suppliers_Tbl Set CompanyName=@cn,Name=@na,Email=@ea,Phone=@ph,JoinDate=@jd,Adress=@ad where SuplierID=@Skey", con);
                    
                    cmd.Parameters.AddWithValue("@cn", txtComName.Text);
                    cmd.Parameters.AddWithValue("@na", txtSupName.Text);
                    cmd.Parameters.AddWithValue("@ea", txtSupEmail.Text);
                    cmd.Parameters.AddWithValue("@ph", txtSupPhone.Text);
                    cmd.Parameters.AddWithValue("@jd", dtpSupDate.Value.Date);
                    cmd.Parameters.AddWithValue("@ad", txtComAdress.Text);                  
                    cmd.Parameters.AddWithValue("@Skey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Updated");
                    con.Close();
                    Showmain();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSupDel_Click(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("Delete from Suppliers_Tbl where SuplierID=@Skey", con);
                    cmd.Parameters.AddWithValue("@Skey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Deleted");
                    con.Close();
                    Showmain();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDashBoad_Click(object sender, EventArgs e)
        {
            buttonClick.Dashboad(); this.Hide();
        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            buttonClick.Cars(); this.Hide();
        }

        private void btnCustReg_Click(object sender, EventArgs e)
        {
            buttonClick.CustReg(); this.Hide();
        }

        private void btnSup_Click(object sender, EventArgs e)
        {
            buttonClick.SupReg(); this.Hide();
        }

        private void btnStaffReg_Click(object sender, EventArgs e)
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
    }
}
