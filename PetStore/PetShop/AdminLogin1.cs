using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShop
{
    public partial class AdminLogin1 : Form
    {
        public AdminLogin1()
        {
            InitializeComponent();
            AdminPassTb.PasswordChar = '*';

        }

        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogin1 obj = new AdminLogin1();
            obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Login1 obj = new Login1();
            obj.Show();
            this.Hide();
        }

        private void PasswordTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (AdminPassTb.Text == "")
            {
            }
            else if (AdminPassTb.Text == "Admin")
            {
                Homes1 obj = new Homes1();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Password");
                AdminPassTb.Text = "";
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            SignUp1 obj = new SignUp1();
            obj.Show();
            this.Hide();
        }
    }
}
