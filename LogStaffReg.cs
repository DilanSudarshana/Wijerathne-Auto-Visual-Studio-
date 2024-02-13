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
    public partial class LogStaffReg : Form
    {
        public LogStaffReg()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DILA\source\repos\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\WijerathneAuto.mdf;Integrated Security=True");
        ButtonClick bc = new ButtonClick();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DILA\\source\\repos\\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\\WijerathneAuto.mdf;Integrated Security=True";
                con.Open();
                string userid = txtUserName.Text;
                string password = txtPassword.Text;
                SqlCommand cmd = new SqlCommand("select EmpName,Emp_Password from Employee_Tbl where EmpName='" + txtUserName.Text + "' and Emp_Password='" + txtPassword.Text + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Login sucess Welcome to Staff Registration ");
                    bc.StaffRegistration();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Login,Admin Only Can Login Staff Registration");
                }
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblBack_Click(object sender, EventArgs e)
        {
            bc.Dashboad();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
        }
    }
}
