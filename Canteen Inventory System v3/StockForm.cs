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

namespace Canteen_Inventory_System_v3
{
    public partial class StockForm : Form
    {
        private string currentRole;
        public StockForm(string user)
        {
            InitializeComponent();
            currentRole = user;
        }

        public void ShowData()
        {
            DBHelper db = new DBHelper();
            inventoryDataGridView.DataSource = db.GetData("Select * from Inventory");
        }
        private void StockForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'canteenDBDataSet.Inventory' table. You can move, or remove it, as needed.
            this.inventoryTableAdapter.Fill(this.canteenDBDataSet.Inventory);
            ShowData();
        }

        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to update the stock quantity?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Validate inputs
                    if (string.IsNullOrWhiteSpace(txtIDstock.Text.Trim()))
                    {
                        MessageBox.Show("Please enter an ID number.");
                        return;
                    }

                    int id = Convert.ToInt32(txtIDstock.Text.Trim());

                    if (int.TryParse(txtQuanStock.Text, out int quantity))
                    {
                        // Check if item exists in database
                        DataTable inventoryTable = this.canteenDBDataSet.Inventory;

                        if (inventoryTable.Rows.Count == 0)
                        {
                            MessageBox.Show("No items found in Inventory table.");
                            return;
                        }

                        // Find the row with matching ID
                        int rowIndex = -1;
                        for (int i = 0; i < inventoryTable.Rows.Count; i++)
                        {
                            if (inventoryTable.Rows[i]["ID"].ToString() == id.ToString())
                            {
                                rowIndex = i;
                                break;
                            }
                        }

                        if (rowIndex >= 0)
                        {
                            // Get current quantity and subtract inputted quantity
                            int currentQuantity = Convert.ToInt32(inventoryTable.Rows[rowIndex]["Quantity"]);

                            if (quantity > 0 && quantity <= currentQuantity)
                            {
                                using (SqlConnection conn = new SqlConnection(DBHelper.connectionString))
                                {
                                    conn.Open();

                                    // ✅ FIXED: Use parameter placeholders instead of string interpolation
                                    string sqlQuery = "UPDATE Inventory SET Quantity = @NewQuantity WHERE ID = @ID";

                                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                                    {
                                        // ✅ FIX: Add both parameters explicitly
                                        cmd.Parameters.AddWithValue("@ID", inventoryTable.Rows[rowIndex]["ID"]);

                                        cmd.Parameters.AddWithValue("@NewQuantity", currentQuantity - quantity);

                                        int rowsAffected = cmd.ExecuteNonQuery();



                                        if (rowsAffected > 0)
                                        {
                                            // ✅ Refresh DataGridView to show updated data
                                            inventoryDataGridView.DataSource = null;
                                            inventoryDataGridView.DataSource = this.canteenDBDataSet.Tables["Inventory"];

                                            MessageBox.Show($"Successfully reduced quantity by {quantity} for Item ID: {inventoryTable.Rows[rowIndex]["ID"]}");
                                        }
                                    }
                                }

                            }
                            else if (quantity > currentQuantity)
                            {
                                MessageBox.Show($"Insufficient stock! Current quantity: {currentQuantity}, Requested: {quantity}");
                            }
                            else
                            {
                                MessageBox.Show("Please enter a positive quantity.");
                            }
                        }
                        else
                        {
                            MessageBox.Show($"No item found with ID: {id}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid quantity number.");
                    }
                    ShowData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating item: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Update cancelled.");
            }
        }

        int currentQuantity = 0;
        private void txtIDstock_Leave(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DBHelper.connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Quantity FROM Inventory WHERE ID = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", int.Parse(txtIDstock.Text));
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    currentQuantity = reader.GetInt32(0);
                }
                reader.Close();
                conn.Close();
                txtQuan1.Text = currentQuantity.ToString();
            }
            catch (Exception ex)
            {
                txtQuan1.Text = "0";
            }
        }

        private void txtIDstock_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIDstock_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtQuanStock_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(txtQuanStock.Text))
                {
                    lblResult.Text = "   ";
                }
                else
                {
                    int Quan1 = int.Parse(txtQuanStock.Text);

                    if (Quan1 > 0)
                    {

                        try
                        {
                            lblResult.Text = (currentQuantity - Quan1).ToString();
                        }
                        catch (Exception ex)
                        {
                            lblResult.Text = "    ";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = "Sync Error";
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard(currentRole);
            dashboard.Show();
            this.Close();
        }

        private void label3_Leave(object sender, EventArgs e)
        {

        }

        private void txtQuanStock_Leave(object sender, EventArgs e)
        {
            
        }
    }
}