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
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }
        int startPoint = 15;

        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startPoint += 1;
            progressBar.Value = startPoint;
            if (progressBar.Value == 100)
            {
                progressBar.Value = 0;
                timer1.Stop();
                LogIn log = new LogIn();
                log.Show();
                this.Hide();

            }
        }
    }
}
