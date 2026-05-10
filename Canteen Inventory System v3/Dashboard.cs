using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Canteen_Inventory_System_v3
{
    public partial class Dashboard : Form
    {
        private string currentRole;
        public Dashboard(string user)
        {
            InitializeComponent();
            currentRole = user;

            if (user == "manager")
            {
                buttonInv.Enabled = true;
                btnStock.Enabled = true;
            }
            else if (user == "cashier")
            {
                buttonInv.Enabled = false;
                btnStock.Enabled = true;
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void buttonInv_Click(object sender, EventArgs e)
        {
            this.Hide();
            InvForm invForm = new InvForm();
            invForm.Show();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            this.Close();
            StockForm stockForm = new StockForm(currentRole);
            stockForm.Show();
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}
