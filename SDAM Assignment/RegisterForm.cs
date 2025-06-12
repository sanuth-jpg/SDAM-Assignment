using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SDAM_Assignment
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            cmbRole.Items.AddRange(new string[] { "admin", "seller", "buyer" });
            cmbRole.SelectedIndex = 0;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string role = cmbRole.SelectedItem.ToString().ToLower();
            string password = txtPassword.Text.Trim();
            string confirm = txtConfirm.Text.Trim();

            string result = User.Register(name, email, phone, role, password, confirm);

            if (result == "success")
            {
                MessageBox.Show("Account created successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show(result);
            }
        }
    }
}