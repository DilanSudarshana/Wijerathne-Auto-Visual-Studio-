using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E2140139_Sudarshana_GDL_ITE_1942_ICT_Project
{
    public class ButtonClick
    {
        public  void Sales()
        {
            Sales sales=new Sales();
            sales.Show();
            
        }
        public  void Dashboad()
        {
            DashBoad dashboad = new DashBoad();
            dashboad.Show();
        }
        public void Cars()
        {
            Car_Inventory cars = new Car_Inventory();
            cars.Show();
            
        }
        public void CustReg()
        {
            CustomerRegistration cr = new CustomerRegistration();
            cr.Show();
        }
        public void SupReg()
        {
            SuppliersRegistration sr = new SuppliersRegistration();
            sr.Show();
        }
        public void staffreg()
        {
            LogStaffReg lsr = new LogStaffReg();
            lsr.Show();
        }
        public void StaffRegistration()
        {
            EmployeeRegistration er = new EmployeeRegistration();
            er.Show();
        }
        public void LogOut()
        {
            LogIn lo = new LogIn();
            lo.Show();
        }
        public void ShowBNCars()
        {
            BrandNewCarsInventory bnc = new BrandNewCarsInventory();
            bnc.Show();
        }
        public void ShowUCars()
        {
            UsedCarInventory uc = new UsedCarInventory();
            uc.Show();
            
        }
        public void ShowRRCars()
        {
            UsedCarInventory uc = new UsedCarInventory();
            uc.Show();

        }
        public void ShowNBRCars()
        {
            CarsNBR nbr = new CarsNBR();
            nbr.Show();
        }
        public void Chashier_Sales()
        {
            SalesForCashier sfc = new SalesForCashier();
            sfc.Show();

        }
        public void Chashier_CustReg()
        {
            CustRegForCashier crc = new CustRegForCashier();
            crc.Show();

        }
    }
}
