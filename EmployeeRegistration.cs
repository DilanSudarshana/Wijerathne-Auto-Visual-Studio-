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
    public partial class EmployeeRegistration : Form
    {
        public EmployeeRegistration()
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
            string query = "Select EmpID,EmpCategory,EmpName,EmpNationalID,EmpGen,EmpJoinDate,EmpAdress,Emp_Phone\r\nFrom Employee_Tbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgvEmployee.DataSource = ds.Tables[0];
            con.Close();
        }
        public void Clear()
        {
            cmb_EmpCategory.SelectedIndex = 0;
            txtEmpName.Text = String.Empty;
            txtEmpNID.Text = String.Empty;
            cmbEmpGen.SelectedIndex = 0;
            dtpEmpJDate.Value.Equals(0);
            txtEmpAdress.Text = String.Empty;
            txtEmpPhone.Text = String.Empty;
            txtEmpPassword.Text = String.Empty; 
            key = 0;

        }
        

        private void btnEmpReg_Click(object sender, EventArgs e)
        {
            if (cmb_EmpCategory.SelectedIndex == -1 || txtEmpName.Text == "" || txtEmpNID.Text == "" || cmbEmpGen.SelectedIndex == -1 || dtpEmpJDate.Value.Equals(0) || txtEmpAdress.Text == "" || txtEmpPhone.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Employee_Tbl(EmpCategory,EmpName,EmpNationalID,EmpGen,EmpJoinDate,EmpAdress,Emp_Phone,Emp_Password)values(@ec,@en,@eni,@egen,@ejd,@ea,@eph,@ep)", con);
                    cmd.Parameters.AddWithValue("@ec", cmb_EmpCategory.SelectedItem);
                    cmd.Parameters.AddWithValue("@en", txtEmpName.Text);
                    cmd.Parameters.AddWithValue("@eni", txtEmpNID.Text);
                    cmd.Parameters.AddWithValue("@egen", cmbEmpGen.SelectedItem);
                    cmd.Parameters.AddWithValue("@ejd", dtpEmpJDate.Value.Date);
                    cmd.Parameters.AddWithValue("@ea", txtEmpAdress.Text);
                    cmd.Parameters.AddWithValue("@eph", txtEmpPhone.Text);
                    cmd.Parameters.AddWithValue("@ep", txtEmpPassword.Text);
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

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cmb_EmpCategory.Text = dgvEmployee.SelectedRows[0].Cells[1].Value.ToString();
                txtEmpName.Text = dgvEmployee.SelectedRows[0].Cells[2].Value.ToString();
                txtEmpNID.Text = dgvEmployee.SelectedRows[0].Cells[3].Value.ToString();
                cmbEmpGen.Text = dgvEmployee.SelectedRows[0].Cells[4].Value.ToString();
                dtpEmpJDate.Text = dgvEmployee.SelectedRows[0].Cells[5].Value.ToString();
                txtEmpAdress.Text = dgvEmployee.SelectedRows[0].Cells[6].Value.ToString();
                txtEmpPhone.Text = dgvEmployee.SelectedRows[0].Cells[7].Value.ToString();
             

                if (txtEmpName.Text == "")
                {
                    key = 0;
                }
                else
                {
                    
                    key = Convert.ToInt32(dgvEmployee.SelectedRows[0].Cells[0].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select the customer");
            }
        }

        private void btnEmpUpd_Click(object sender, EventArgs e)
        {
            if (cmb_EmpCategory.SelectedIndex == -1 || txtEmpName.Text == "" || txtEmpNID.Text == "" || cmbEmpGen.SelectedIndex == -1 || dtpEmpJDate.Value.Equals(0) || txtEmpAdress.Text == "" || txtEmpPhone.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update Employee_Tbl Set EmpCategory=@ec,EmpName=@en,EmpNationalID=@eni,EmpGen=@egen,EmpJoinDate=@ejd,EmpAdress=@ea,Emp_Phone=@eph,Emp_Password=@ep where EmpID=@EKey", con);
                    cmd.Parameters.AddWithValue("@ec", cmb_EmpCategory.SelectedItem);
                    cmd.Parameters.AddWithValue("@en", txtEmpName.Text);
                    cmd.Parameters.AddWithValue("@eni", txtEmpNID.Text);
                    cmd.Parameters.AddWithValue("@egen", cmbEmpGen.SelectedItem);
                    cmd.Parameters.AddWithValue("@ejd", dtpEmpJDate.Value.Date);
                    cmd.Parameters.AddWithValue("@ea", txtEmpAdress.Text);
                    cmd.Parameters.AddWithValue("@eph", txtEmpPhone.Text);
                    cmd.Parameters.AddWithValue("@ep", txtEmpPassword.Text);
                    cmd.Parameters.AddWithValue("@Ekey", key);
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

        private void btnEmpDel_Click(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("Delete from Employee_Tbl where EmpID=@Ekey", con);
                    cmd.Parameters.AddWithValue("@Ekey", key);
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

        private void btnDashboad_Click(object sender, EventArgs e)
        {
            buttonClick.Dashboad(); this.Hide();
        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            buttonClick.Cars(); this.Hide();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            buttonClick.CustReg(); this.Hide();
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

        private void label12_Click(object sender, EventArgs e)
        {
            buttonClick.Dashboad();this.Hide();
        }
    }

}
