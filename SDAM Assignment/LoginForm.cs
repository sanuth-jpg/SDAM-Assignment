using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SDAM_Assignment
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {   
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Seller");
            cmbRole.Items.Add("Buyer");
            cmbRole.SelectedIndex = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.");
                return;
            }

            string role = cmbRole.SelectedItem.ToString().ToLower();

            User user = User.Login(email, password, role);
            if (user != null)
            {
                user.OpenDashboard();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid login. Check email, password, and role.");
            }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm();
            regForm.ShowDialog();
        }
    }
}