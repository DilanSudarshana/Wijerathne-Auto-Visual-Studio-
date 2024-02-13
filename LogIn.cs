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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace E2140139_Sudarshana_GDL_ITE_1942_ICT_Project
{
    public partial class LogIn : Form
    {
        ButtonClick bc=new ButtonClick();
        public LogIn()
        {
            InitializeComponent();
            
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DILA\source\repos\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\WijerathneAuto.mdf;Integrated Security=True");

        private void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtEMPType.SelectedItem == "Admin")
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DILA\\source\\repos\\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\\WijerathneAuto.mdf;Integrated Security=True";
                    con.Open();
                    string userid = txtUserName.Text;
                    string password = txtPassword.Text;
                    SqlCommand cmd = new SqlCommand("select EmpName,Emp_Password,EmpCategory from Employee_Tbl where EmpName='" + txtUserName.Text + "' and Emp_Password='" + txtPassword.Text + "' and  EmpCategory='" + txtEMPType.SelectedItem.ToString() + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Login sucess Welcome to Homepage ");
                        bc.Dashboad();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Login please check username and password");
                    }
                    con.Close();


                }
                else if (txtEMPType.SelectedItem == "Cashier")
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DILA\\source\\repos\\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\\WijerathneAuto.mdf;Integrated Security=True";
                    con.Open();
                    string userid = txtUserName.Text;
                    string password = txtPassword.Text;
                    SqlCommand cmd = new SqlCommand("select EmpName,Emp_Password,EmpCategory from Employee_Tbl where EmpName='" + txtUserName.Text + "' and Emp_Password='" + txtPassword.Text + "' and  EmpCategory='" + txtEMPType.SelectedItem.ToString() + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Login sucess Welcome to Sales Page");
                        bc.Chashier_Sales(); 
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Login please check username,password and staff category");
                    }
                }
                else if (txtEMPType.SelectedItem == "Clerk")
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DILA\\source\\repos\\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\\E2140139_Sudarshana_GDL_ITE_1942_ICT_Project\\WijerathneAuto.mdf;Integrated Security=True";
                    con.Open();
                    string userid = txtUserName.Text;
                    string password = txtPassword.Text;
                    SqlCommand cmd = new SqlCommand("select EmpName,Emp_Password,EmpCategory from Employee_Tbl where EmpName='" + txtUserName.Text + "' and Emp_Password='" + txtPassword.Text + "' and  EmpCategory='" + txtEMPType.SelectedItem.ToString() + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Login sucess Welcome to Homepage ");
                        bc.Dashboad();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Login please check username,password and staff category");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtEMPType.SelectedIndex = 0;
            
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
