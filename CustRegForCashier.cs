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
    public partial class CustRegForCashier : Form
    {
        public CustRegForCashier()
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
            string query = "select * from Customer_tbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgvCusormers.DataSource = ds.Tables[0];
            con.Close();
        }
        public void Clear()
        {

            txtCustName.Text = String.Empty;
            txNaIdNum.Text = String.Empty;
            txtCustPhone.Text = String.Empty;
            txtCustEmail.Text = String.Empty;
            txtCustAdress.Text = String.Empty;
            cmbCustGen.SelectedIndex = 0;
            dtpJoinDate.Value.Equals(0);
            cmbCustGen.SelectedIndex = 0;
            key = 0;

        }


        private void btnCustSave_Click(object sender, EventArgs e)
        {
            if (txNaIdNum.Text == "" || txtCustEmail.Text == "" || txtCustName.Text == "" || cmbCustGen.SelectedIndex == -1 || txtCustPhone.Text == "" || txtCustAdress.Text == "" || dtpJoinDate.Value.Equals(0))
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Customer_tbl(NationalID,CustName,Gender,Phone,JoinDate,Email,Adress)values(@cnid,@cn,@gen,@ph,@dt,@em,@ad)", con);
                    cmd.Parameters.AddWithValue("@cnid", txNaIdNum.Text);
                    cmd.Parameters.AddWithValue("@cn", txtCustName.Text);
                    cmd.Parameters.AddWithValue("@gen", cmbCustGen.SelectedItem);
                    cmd.Parameters.AddWithValue("@ph", txtCustPhone.Text);
                    cmd.Parameters.AddWithValue("@dt", dtpJoinDate.Value.Date);
                    cmd.Parameters.AddWithValue("@em", txtCustEmail.Text);
                    cmd.Parameters.AddWithValue("@ad", txtCustAdress.Text);
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

        private void dgvCusormers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                txNaIdNum.Text = dgvCusormers.SelectedRows[0].Cells[1].Value.ToString();
                txtCustName.Text = dgvCusormers.SelectedRows[0].Cells[2].Value.ToString();
                cmbCustGen.Text = dgvCusormers.SelectedRows[0].Cells[3].Value.ToString();
                txtCustPhone.Text = dgvCusormers.SelectedRows[0].Cells[4].Value.ToString();
                dtpJoinDate.Text = dgvCusormers.SelectedRows[0].Cells[5].Value.ToString();
                txtCustEmail.Text = dgvCusormers.SelectedRows[0].Cells[6].Value.ToString();
                txtCustAdress.Text = dgvCusormers.SelectedRows[0].Cells[7].Value.ToString();

                if (txNaIdNum.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(txNaIdNum.Text = dgvCusormers.SelectedRows[0].Cells[0].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select the customer");
            }
        }

        private void btnCustUpdate_Click(object sender, EventArgs e)
        {
            if (txNaIdNum.Text == "" || txtCustEmail.Text == "" || txtCustName.Text == "" || cmbCustGen.SelectedItem == "" || txtCustPhone.Text == "" || txtCustAdress.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update Customer_tbl Set NationalID=@cnid,CustName=@cn,Gender=@gen,Phone=@ph,JoinDate=@dt,Email=@em,Adress=@ad where CustomerID=@CKey", con);

                    cmd.Parameters.AddWithValue("@cnid", txNaIdNum.Text);
                    cmd.Parameters.AddWithValue("@cn", txtCustName.Text);
                    cmd.Parameters.AddWithValue("@gen", cmbCustGen.SelectedItem);
                    cmd.Parameters.AddWithValue("@ph", txtCustPhone.Text);
                    cmd.Parameters.AddWithValue("@dt", dtpJoinDate.Value.Date);
                    cmd.Parameters.AddWithValue("@em", txtCustEmail.Text);
                    cmd.Parameters.AddWithValue("@ad", txtCustAdress.Text);
                    cmd.Parameters.AddWithValue("@Ckey", key);
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

        private void btnCustDelete_Click(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("Delete from Customer_tbl where CustomerID=@Ckey", con);
                    cmd.Parameters.AddWithValue("@Ckey", key);
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

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            buttonClick.LogOut(); this.Hide();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            buttonClick.Chashier_Sales(); this.Hide();
        }

        private void btnCustReg_Click(object sender, EventArgs e)
        {
            buttonClick.Chashier_CustReg(); this.Hide();
        }
    }
}
