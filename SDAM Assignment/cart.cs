using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDAM_Assignment
{
    public partial class cart: Form
    {
        private int buyerId;
        private string connectionString = "server=localhost;user=root;password=;database=marketplace;";

        public cart(int buyerId)
        {
            InitializeComponent();
            this.buyerId = buyerId;
            LoadCartItems();
        }

        private void LoadCartItems()
        {
            List<CartItem> cartItems = new List<CartItem>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT c.cart_id, c.product_id, p.name AS product_name, p.price, c.quantity
                    FROM cart c
                    JOIN product p ON c.product_id = p.product_id
                    WHERE c.buyer_id = @buyerId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@buyerId", buyerId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cartItems.Add(new CartItem
                            {
                                CartId = reader.GetInt32("cart_id"),
                                ProductId = reader.GetInt32("product_id"),
                                ProductName = reader.GetString("product_name"),
                                Price = reader.GetDecimal("price"),
                                Quantity = reader.GetInt32("quantity")
                            });
                        }
                    }
                }
            }

            dataGridViewCart.DataSource = cartItems;
            dataGridViewCart.Columns["CartId"].Visible = false;
            dataGridViewCart.Columns["ProductId"].Visible = false;

            // Show total
            decimal total = 0;
            foreach (var item in cartItems)
                total += item.Total;

            lblTotal.Text = $"Total: Rs. {total:F2}";
        }
    }

}
