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
    public partial class UserHomes1 : Form
    {
        public UserHomes1()
        {
            InitializeComponent();
            UserNameLbl.Text = Login1.User;

        }

        private void label4_Click(object sender, EventArgs e)
        {
            UserCustomers1 obj = new UserCustomers1();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            UserBillings1 obj = new UserBillings1();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login1 obj = new Login1();
            obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
