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
using static Canteen_Inventory_System_v3.DBHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace Canteen_Inventory_System_v3
{
    public partial class InvForm : Form
    {
        public InvForm()
        {
            InitializeComponent();
        }

        

        public void ShowData()
        {
            DBHelper db = new DBHelper();
            inventoryDataGridView.DataSource = db.GetData("Select * from Inventory");
        }

        private void InvForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'canteenDBDataSet.Inventory' table. You can move, or remove it, as needed.
            this.inventoryTableAdapter.Fill(this.canteenDBDataSet.Inventory);
            ShowData();

        }

        private void btnPreAddItem_Click(object sender, EventArgs e)
        {
            lblCancel.Show();
            btnPreAddItem.Hide();
            btnAddItem.Show();
            btnPreUpdate.Enabled = false;

            addItemTXT.Enabled = true;
            addcategorycbo.Enabled = true;
            addPriceTXT.Enabled = true;
            addQuantityTXT.Enabled = true;
        }

        private void addPriceTXT_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            lblCancel.Hide();

            btnPreAddItem.Show(); btnPreAddItem.Enabled = true;
            btnAddItem.Hide();
            btnUpdate.Hide();
            btnPreUpdate.Show(); btnPreUpdate.Enabled = true;
            
            addItemTXT.Clear();
            addcategorycbo.Text = null;
            addPriceTXT.Clear();
            addQuantityTXT.Clear();

            addItemTXT.Enabled = false;
            addcategorycbo.Enabled = false;
            addPriceTXT.Enabled = false;
            addQuantityTXT.Enabled = false;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            DBHelper db = new DBHelper();

            try
            {
                string item = addItemTXT.Text;
                string category = addcategorycbo.Text;
                float price = float.Parse(addPriceTXT.Text);
                int quantity = int.Parse(addQuantityTXT.Text);


                string sql = $"INSERT INTO Inventory (Item, Category, Price, Quantity, CreatedDate, LastUpdated) " +
                                   $"VALUES ('{item}', '{category}', {price}, {quantity}, GETDATE(), GETDATE())";

                if (db.CreateUpdateDelete_Command(sql) > 0)
                {
                    ShowData();
                    MessageBox.Show("Record Saved Successfully", "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Record Not Saved", "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                MessageBox.Show("Duplicate entry detected. Please check your input and try again.", "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding item: {ex.Message}");
            }

            lblCancel.Hide();
            btnAddItem.Hide();
            btnPreAddItem.Show();
            btnPreAddItem.Enabled = true;
            btnPreUpdate.Enabled = true;

            addItemTXT.Clear();
            addcategorycbo.Text = null;
            addPriceTXT.Clear();
            addQuantityTXT.Clear();

            addItemTXT.Enabled = false;
            addcategorycbo.Enabled = false;
            addPriceTXT.Enabled = false;
            addQuantityTXT.Enabled = false;

        }




        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DBHelper db = new DBHelper();

            try
            {
                string item = addItemTXT.Text;
                string category = addcategorycbo.Text;
                float price = float.Parse(addPriceTXT.Text);


                string sql = $"UPDATE Inventory SET Category = '{category}', Price = {price}, LastUpdated = GETDATE() " +
                             $"WHERE Item = '{item}'";

                if (db.CreateUpdateDelete_Command(sql) > 0)
                {
                    ShowData();
                    MessageBox.Show("Record Saved Successfully", "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Record Not Saved", "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                MessageBox.Show("Duplicate entry detected. Please check your input and try again.", "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding item: {ex.Message}");
            }

            lblCancel.Hide();
            btnAddItem.Hide();
            btnPreAddItem.Show();
            btnPreAddItem.Enabled = true;
            btnPreUpdate.Enabled = true;

            addItemTXT.Clear();
            addcategorycbo.Text = null;
            addPriceTXT.Clear();
            addQuantityTXT.Clear();

            addItemTXT.Enabled = false;
            addcategorycbo.Enabled = false;
            addPriceTXT.Enabled = false;
            addQuantityTXT.Enabled = false;
        }

        private void btnFindInv_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchInv.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                inventoryBindingSource.Filter = null;
                inventoryDataGridView.DataSource = inventoryBindingSource;
            }
            else
            {
                
                string filterExpression = $"Item LIKE '{searchText}%' OR " +
                                         $"Category LIKE '{searchText}%' OR" +
                                         $"Id LIKE '{searchText}%' OR" +
                                         $"DateAdded LIKE '{searchText.ToString()}%' OR" +
                                         $"LastUpdated LIKE '{searchText.ToString()}%'";

                
                inventoryBindingSource.Filter = filterExpression;

                inventoryDataGridView.DataSource = inventoryBindingSource;
            }
        }

        private void btnPreUpdate_Click(object sender, EventArgs e)
        {
            lblCancel.Show();
            btnPreUpdate.Hide();
            btnUpdate.Show();
            btnPreAddItem.Enabled = false;

            addItemTXT.Enabled = true;
            addcategorycbo.Enabled = true;
            addPriceTXT.Enabled = true;
            addQuantityTXT.Enabled = false;


        }

        private void button1_Click(object sender, EventArgs e) // Archive button
        {
            DialogResult result = MessageBox.Show("Are you sure you want to archive this item?", "Confirm Archive", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            DBHelper db = new DBHelper();
            try
            {
                string item = addItemTXT.Text.Trim();
                string categoryText = addcategorycbo.Text.Trim();

                // ✅ Validate inputs first
                if (string.IsNullOrEmpty(item))
                {
                    MessageBox.Show("Please enter an item name", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(categoryText))
                {
                    MessageBox.Show("Please select a category", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Validate price and quantity
                double priceValue = 0;
                int quantityValue = 0;

                if (string.IsNullOrEmpty(addPriceTXT.Text.Trim()))
                {
                    MessageBox.Show("Please enter a valid price", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(addQuantityTXT.Text.Trim()))
                {
                    MessageBox.Show("Please enter a valid quantity", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    priceValue = double.Parse(addPriceTXT.Text.Trim());
                    quantityValue = int.Parse(addQuantityTXT.Text.Trim());

                    if (priceValue <= 0)
                    {
                        MessageBox.Show("Price must be greater than 0", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (quantityValue <= 0)
                    {
                        MessageBox.Show("Quantity must be greater than 0", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please enter valid numeric values for Price and Quantity",
                                   "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Get the item ID to update correctly
                int itemId = 0;
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string getIdQuery = "SELECT Id FROM dbo.Inventory WHERE Item = @Item";
                        using (SqlCommand cmd = new SqlCommand(getIdQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Item", item);
                            object resultValue = cmd.ExecuteScalar();

                            if (resultValue != null && resultValue.ToString() != "")
                            {
                                itemId = Convert.ToInt32(resultValue);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting item ID: {ex.Message}");
                    MessageBox.Show("Failed to get item ID. Archive may have failed.",
                                   "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Build the UPDATE query with proper parameters
                string sql = $"UPDATE Inventory SET Category = '{categoryText}', Price = {priceValue}, LastUpdated = GETDATE() WHERE Id = {itemId}";

                // ✅ Execute using DBHelper's transaction method
                if (db.CreateUpdateDelete_Command(sql) > 0)
                {
                    ShowData();
                    MessageBox.Show("Item Archived Successfully", "Success",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ✅ Reset form after successful archive
                    lblCancel.Hide();
                    btnPreAddItem.Show();
                    btnPreUpdate.Show();
                    addItemTXT.Clear();
                    addcategorycbo.Text = null;
                    addPriceTXT.Clear();
                    addQuantityTXT.Clear();
                }
                else
                {
                    MessageBox.Show("Record Not Archived - Check Console for errors", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                MessageBox.Show("Duplicate entry detected. Please check your input and try again.", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Archive Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);

                MessageBox.Show($"Error archiving item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void inventoryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnArchived_Click(object sender, EventArgs e)
        {
            Archive archive = new Archive();
            archive.Show();
            this.Hide();
        }

        private void inventoryDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) { return; }

                DataGridViewRow row = inventoryDataGridView.Rows[e.RowIndex];
                addItemTXT.Text = row.Cells["itemDataGridViewTextBoxColumn"].Value?.ToString() ?? "";
                addcategorycbo.Text = row.Cells["categoryDataGridViewTextBoxColumn"].Value?.ToString() ?? "";
                addPriceTXT.Text = row.Cells["priceDataGridViewTextBoxColumn"].Value?.ToString() ?? "";
                addQuantityTXT.Text = row.Cells["quantityDataGridViewTextBoxColumn"].Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void Navigation_Opening(object sender, CancelEventArgs e)
        {

        }

        private void InvForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dashboard dashboard = new Dashboard("manager");
            dashboard.Show();
            this.Hide();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard("manager");
            dashboard.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StockForm stockForm = new StockForm("manager");
            stockForm.Show();
            this.Hide();
        }
    }
}
