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
    public partial class OrdersForm: Form
    {
        private int buyerId; // Passed from login

        public OrdersForm(int buyerId)
        {
            InitializeComponent();
            this.buyerId = buyerId;
            LoadOrders();
        }
        private void LoadOrders()
        {
            flowLayoutPanelOrders.Controls.Clear();

            string connStr = "server=localhost;user=root;password=;database=marketplace;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                // Get all orders for the current buyer
                string orderQuery = "SELECT order_id, placed_at, total_price FROM orders WHERE buyer_id = @b ORDER BY placed_at DESC";

                using (MySqlCommand orderCmd = new MySqlCommand(orderQuery, conn))
                {
                    orderCmd.Parameters.AddWithValue("@b", buyerId);
                    using (MySqlDataReader orderReader = orderCmd.ExecuteReader())
                    {
                        while (orderReader.Read())
                        {
                            int orderId = orderReader.GetInt32("order_id");
                            DateTime placedAt = orderReader.GetDateTime("placed_at");
                            decimal total = orderReader.GetDecimal("total_price");

                            // Fetch items for this order
                            List<string> itemSummaries = GetOrderItems(orderId);

                            // Create and add the card panel
                            Panel orderCard = CreateOrderCard(orderId, placedAt, total, itemSummaries);
                            flowLayoutPanelOrders.Controls.Add(orderCard);
                        }
                    }
                }
            }
        }
        private List<string> GetOrderItems(int orderId)
        {
            List<string> items = new List<string>();
            string connStr = "server=localhost;user=root;password=;database=marketplace;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string itemQuery = @"
            SELECT p.name, oi.quantity 
            FROM order_items oi
            JOIN product p ON oi.product_id = p.product_id
            WHERE oi.order_id = @o";

                using (MySqlCommand cmd = new MySqlCommand(itemQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@o", orderId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString("name");
                            int qty = reader.GetInt32("quantity");
                            items.Add($"{name} x {qty}");
                        }
                    }
                }
            }

            return items;
        }
        private Panel CreateOrderCard(int orderId, DateTime placedAt, decimal total, List<string> items)
        {
            Panel card = new Panel
            {
                Width = 400,
                Height = 180,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            Label lblTitle = new Label
            {
                Text = $"Order #{orderId} - {placedAt:yyyy-MM-dd HH:mm}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Top = 10,
                Left = 10
            };

            Label lblItems = new Label
            {
                Text = string.Join("\n", items),
                Top = lblTitle.Bottom + 5,
                Left = 10,
                AutoSize = true
            };

            Label lblTotal = new Label
            {
                Text = $"Total: Rs. {total:F2}",
                Top = lblItems.Bottom + 10,
                Left = 10,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.Green
            };

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblItems);
            card.Controls.Add(lblTotal);

            return card;
        }

    }
}
