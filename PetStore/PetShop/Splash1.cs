using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace PetShop
{
    public partial class Splash1 : Form
    {
        public Splash1()
        {
            InitializeComponent();
            timer1.Start();

        }
        int startP = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            startP += 1;
            MyProgress.Value = startP;
            PercentageLbl.Text = startP + "%";
            if (MyProgress.Value == 100)
            {
                MyProgress.Value = 0;
                Login1 obj = new Login1();
                obj.Show();
                this.Hide();
                timer1.Stop();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MyProgress_ValueChanged(object sender, EventArgs e)
        {

        }

        private void PercentageLbl_Click(object sender, EventArgs e)
        {

        }
    }
}
