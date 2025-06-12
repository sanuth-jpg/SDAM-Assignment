using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SDAM_Assignment
{
    public partial class AddProductForm : Form
    {
        private string imagePath = "";
        private Seller seller;

        public AddProductForm(int sellerId)
        {
            InitializeComponent();
            this.seller = Seller.GetSellerById(sellerId);

            btnUploadImage.Click += btnUploadImage_Click;
            btnSave.Click += btnSave_Click;
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imagePath = ofd.FileName;
                pictureBox1.Image = Image.FromFile(imagePath);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string desc = txtDescription.Text.Trim();
            string priceStr = txtPrice.Text.Trim();

            string result = Product.AddNewProduct(seller.Id, name, desc, priceStr, imagePath);

            if (result == "success")
            {
                MessageBox.Show("Product added successfully.");
                txtName.Clear();
                txtDescription.Clear();
                txtPrice.Clear();
                pictureBox1.Image = null;
                imagePath = "";
            }
            else
            {
                MessageBox.Show(result);
            }
        }
    }
}