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
    public partial class BuyerDash : Form
    {
        private Buyer buyer;

        public BuyerDash(Buyer buyer)
        {
            InitializeComponent();
            this.buyer = buyer;
            this.Load += BuyerDash_Load;
        }

        private void BuyerDash_Load(object sender, EventArgs e)
        {
            // Just show the data in labels — no database query needed here
            lblName.Text = "Name: " + buyer.Name;
            lblEmail.Text = "Email: " + buyer.Email;
            lblContact.Text = "Contact: " + buyer.Phone;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BrowseProductsForm shop = new BrowseProductsForm(buyer); // Pass buyer if needed
            shop.Show();
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            cart cart = new cart(buyerID); // Pass buyer if needed
            cart.Show();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            OrdersForm orders = new OrdersForm(buyer); // Pass buyer if needed
            orders.Show();
        }
    }
}

