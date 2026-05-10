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
    public partial class Archive : Form
    {
        public Archive()
        {
            InitializeComponent();
        }

        public void ShowData()
        {
            DBHelper db = new DBHelper();
            archiveDataGridView.DataSource = db.GetData("Select * from Inventory");
        }

        private void Archive_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'canteenDBDataSet.Archive' table. You can move, or remove it, as needed.
            this.archiveTableAdapter.Fill(this.canteenDBDataSet.Archive);
            ShowData();

        }

        private void btnInv_Click(object sender, EventArgs e)
        {
            InvForm invForm = new InvForm();
            invForm.Show();
            this.Hide();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            
        }

        private void archiveDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                DataGridViewRow row = archiveDataGridView.Rows[e.RowIndex];

            }
            catch (Exception ex)
            {
                
            }

        }
    }
}
