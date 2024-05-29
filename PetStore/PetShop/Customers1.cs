using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShop
{
    public partial class Customers1 : Form
    {
        public Customers1()
        {
            InitializeComponent();
            DisplayCustomers();
            UserNameLbl.Text = Login1.User;

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\melik\OneDrive\Belgeler\PetShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayCustomers()
        {
            Con.Open();
            string Query = "Select * from CustomerTbl"; // SQL sorgusunu tanımlar. Bu sorgu, Employeetbl tablosundaki tüm kayıtları seçer.
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);  // SqlDataAdapter, veritabanından veri almayı ve veritabanına veri göndermeyi kolaylaştırır.
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda); // SqlCommandBuilder, SqlDataAdapter nesnesine bağlı olan veri komutlarını otomatik olarak oluşturur 
            var ds = new DataSet(); // Bir DataSet nesnesi oluşturur. DataSet, veritabanındaki verilerin bellek içi temsili olarak kullanılır.
            sda.Fill(ds); // SqlDataAdapter nesnesini kullanarak veritabanından verileri alır ve DataSet nesnesine doldurur.
            CustomerDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Clear()
        {
            CustNameTb.Text = "";
            CustAddTb.Text = "";
            CustPhoneTb.Text = "";
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustAddTb.Text == "" || CustPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl (CustName, CustAdd, CustPhone) values (@CN, @CA, @CP)", Con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added!");
                    Con.Close();
                    DisplayCustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustNameTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            CustAddTb.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            CustPhoneTb.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (CustNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Homes1 obj = new Homes1();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Products1 obj = new Products1();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Employees1 obj = new Employees1();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billlings obj = new Billlings();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login1 obj = new Login1();
            obj.Show();
            this.Hide();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            Login1 obj = new Login1();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Employees1 obj = new Employees1();
            obj.Show();
            this.Hide();
        }
    }
}
