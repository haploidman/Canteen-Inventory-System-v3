using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Canteen_Inventory_System_v3
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPass.Checked)
            {
                textBoxPassword.UseSystemPasswordChar = false; textBoxPassword.Focus();
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = true; textBoxPassword.Focus();
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text;
            
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DBHelper.User user = DBHelper.GetUser(username);
            if (user == null)
            {
                MessageBox.Show("User not found.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (user.Password != password)
            {
                MessageBox.Show("Incorrect password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show($"Welcome, {user.Username}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dashboard dashboard = new Dashboard(username);
                dashboard.Show();
                this.Hide();
            }
        }
    }
}
