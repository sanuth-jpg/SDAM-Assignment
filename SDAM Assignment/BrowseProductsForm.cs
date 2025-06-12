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
    public partial class BrowseProductsForm : Form
    {
        private int buyerId;



        public BrowseProductsForm(int buyerId)
        {
            InitializeComponent();
            this.buyerId = buyerId;
            LoadAllProducts();
        }

        private void LoadAllProducts()
        {
            try
            {
                flowLayoutPanelProducts.Controls.Clear();
                List<Product> products = GetAllProducts(); // fetch from DB

                if (products.Count == 0)
                {
                    flowLayoutPanelProducts.Controls.Add(new Label
                    {
                        Text = "No products available.",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        ForeColor = Color.Gray
                    });
                    return;
                }

                foreach (var product in products)
                {
                    flowLayoutPanelProducts.Controls.Add(CreateProductCard(product));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            string connStr = "server=localhost;user=root;password=;database=marketplace;";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM product";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = reader.GetInt32("product_id"),
                            Name = reader.GetString("name"),
                            Description = reader.GetString("description"),
                            Price = reader.GetDecimal("price"),
                            ImagePath = reader.IsDBNull(reader.GetOrdinal("image_path")) ? null : reader.GetString("image_path")
                        });
                    }
                }
            }

            return products;
        }

        private Panel CreateProductCard(Product product)
        {
            Panel card = new Panel
            {
                Width = 200,
                Height = 360,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                BackColor = Color.White
            };

            PictureBox picture = new PictureBox
            {
                Width = 180,
                Height = 150,
                Top = 10,
                Left = 10,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            if (!string.IsNullOrEmpty(product.ImagePath) && File.Exists(product.ImagePath))
            {
                try { picture.Image = Image.FromFile(product.ImagePath); }
                catch { picture.Image = null; }
            }

            Label lblName = new Label
            {
                Text = product.Name,
                Top = picture.Bottom + 5,
                Left = 10,
                Width = 180,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            Label lblDesc = new Label
            {
                Text = product.Description,
                Top = lblName.Bottom + 5,
                Left = 10,
                Width = 180,
                Height = 40,
                AutoEllipsis = true
            };

            Label lblPrice = new Label
            {
                Text = $"Rs. {product.Price:F2}",
                Top = lblDesc.Bottom + 5,
                Left = 10,
                Width = 180,
                ForeColor = Color.Green,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            NumericUpDown numQty = new NumericUpDown
            {
                Top = lblPrice.Bottom + 10,
                Left = 10,
                Width = 180,
                Minimum = 1,
                Maximum = 100,
                Value = 1
            };

            Button btnAddToCart = new Button
            {
                Text = "Add to Cart",
                Width = 180,
                Height = 30,
                Top = numQty.Bottom + 5,
                Left = 10,
                BackColor = Color.LightGreen
            };

            btnAddToCart.Click += (s, e) =>
            {
                int quantity = (int)numQty.Value;

                using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=marketplace;"))
                {
                    conn.Open();
                    string insert = "INSERT INTO cart (buyer_id, product_id, quantity) VALUES (@b, @p, @q)";
                    using (MySqlCommand cmd = new MySqlCommand(insert, conn))
                    {
                        cmd.Parameters.AddWithValue("@b", buyerId);
                        cmd.Parameters.AddWithValue("@p", product.ProductId);
                        cmd.Parameters.AddWithValue("@q", quantity);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"{product.Name} added to cart.", "Success");
            };

            card.Controls.Add(picture);
            card.Controls.Add(lblName);
            card.Controls.Add(lblDesc);
            card.Controls.Add(lblPrice);
            card.Controls.Add(numQty);
            card.Controls.Add(btnAddToCart);

            return card;
        }
    }
}
