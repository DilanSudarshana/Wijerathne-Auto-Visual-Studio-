using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E2140139_Sudarshana_GDL_ITE_1942_ICT_Project
{
    public partial class Car_Inventory : Form
    {
        ButtonClick buttonClick = new ButtonClick();
        public Car_Inventory()
        {
            InitializeComponent();
        }

        private void btnUC_Click(object sender, EventArgs e)
        {
            buttonClick.ShowUCars();
            this.Hide();
        }

        private void btnBNC_Click(object sender, EventArgs e)
        {
            buttonClick.ShowBNCars();
            this.Hide();
        }

        private void btnNtBR_Click(object sender, EventArgs e)
        {
            buttonClick.ShowNBRCars();
            this.Hide();
        }

        private void btnDashBoad_Click(object sender, EventArgs e)
        {
            buttonClick.Dashboad();
            this.Hide();
        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            buttonClick.Cars();
            this.Hide();
        }

        private void btnCust_Click(object sender, EventArgs e)
        {
            buttonClick.CustReg();
            this.Hide();
        }

        private void btnSup_Click(object sender, EventArgs e)
        {
            buttonClick.SupReg();
            this.Hide();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            buttonClick.staffreg();
            this.Hide();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            buttonClick.Sales();
            this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            buttonClick.LogOut();
            this.Hide();
        }
    }
}
