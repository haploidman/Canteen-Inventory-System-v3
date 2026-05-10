using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Canteen_Inventory_System_v3
{
    internal class DBHelper
    {
        string conStr;
        public static readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CanteenDB.mdf;Integrated Security=True";

        SqlConnection sqlcon;
        public DBHelper()
        {
            conStr = connectionString;
            sqlcon = new SqlConnection(conStr);
            sqlcon.Open();
        }

        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }

            public string Role { get; set; }
        }

        public class Inventory
        {
            public int Id { get; set; }
            public string Item { get; set; }
            public string Category { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public DateTime LastUpdated { get; set; }
            public DateTime CreatedDate { get; internal set; }
        }

        public class Archive
        {
            public int Id { get; set; }
            public string ItemName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public DateTime DateAdded { get; set; }
            public DateTime DateArchived { get; set; }
        }

        public static DBHelper.User GetUser(string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string selectQuery = "SELECT username, password, role FROM Login WHERE username = @username;";
                
                using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DBHelper.User
                            {
                                Username = reader.GetString(0),
                                Password = reader.GetString(1),
                            };
                        }
                    }
                }
            }
            return null;
        }

        //public static bool InsertItem(string itemText, string categoryText, int quantityText, float priceText)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            using (SqlTransaction trans = conn.BeginTransaction())
        //            {
        //                string insertQuery = "INSERT INTO Inventory (Item, Category, Quantity, Price, CreatedDate, LastUpdated) VALUES (@Item, @Category, @Quantity, @Price, @CreatedDate, @LastUpdated)";
        //                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
        //                {
        //                    cmd.Parameters.AddWithValue("@Item", itemText);
        //                    cmd.Parameters.AddWithValue("@Category", categoryText);
        //                    cmd.Parameters.AddWithValue("@Quantity", quantityText);
        //                    cmd.Parameters.AddWithValue("@Price", priceText);
        //                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
        //                    cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);

        //                    int rowsAffected = cmd.ExecuteNonQuery();
        //                    trans.Commit();
        //                    return true;

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error inserting item: " + ex.Message);
        //        return false;
        //    }
        //}
        //public static List<Inventory> GetAllItems()
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();

        //        string selectQuery = "SELECT Id, Item, Category, Price, Quantity, CreatedDate, LastUpdated FROM dbo.Inventory";

        //        using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
        //        {
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                List<Inventory> items = new List<Inventory>();

        //                while (reader.Read())
        //                {
        //                    items.Add(new Inventory
        //                    {
        //                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                        Item = reader.GetString(reader.GetOrdinal("Item")),
        //                        Category = reader.GetString(reader.GetOrdinal("Category")),
        //                        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
        //                        Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
        //                        CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
        //                        LastUpdated = reader.GetDateTime(reader.GetOrdinal("LastUpdated"))
        //                    });
        //                }

        //                return items;
        //            }
        //        }
        //    }
        //}

        //public static bool UpdateItem(int id, string itemText, string categoryText, int quantityText, decimal priceText)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            string updateQuery = "UPDATE Inventory SET Item = @Item, Category = @Category, Quantity = @Quantity, Price = @Price, LastUpdated = @LastUpdated WHERE Id = @Id";
        //            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@Id", id);
        //                cmd.Parameters.AddWithValue("@Item", itemText);
        //                cmd.Parameters.AddWithValue("@Category", categoryText);
        //                cmd.Parameters.AddWithValue("@Quantity", quantityText);
        //                cmd.Parameters.AddWithValue("@Price", priceText);
        //                cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
        //                int rowsAffected = cmd.ExecuteNonQuery();
        //                return rowsAffected > 0;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error updating item: " + ex.Message);
        //        return false;
        //    }
        //}

        //public static bool DeleteItem(int id)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            string MoveToArchiveQuery = "INSERT INTO Archive (ItemName, Quantity, Price, DateAdded, DateArchived) SELECT Item, Quantity, Price, CreatedDate, GETDATE() FROM Inventory WHERE Id = @Id";
        //            using (SqlCommand cmd = new SqlCommand(MoveToArchiveQuery, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@Id", id);
        //                int rowsAffected = cmd.ExecuteNonQuery();
        //                return rowsAffected > 0;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error deleting item: " + ex.Message);
        //        return false;
        //    }
        //}

        public DataTable GetData(string query)
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public int CreateUpdateDelete_Command(string query)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
