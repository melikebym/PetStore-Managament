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
    public partial class Employees1 : Form
    {
        public Employees1()
        {
            InitializeComponent();
            DisplayEmployees();
            UserNameLbl.Text = Login1.User;


        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\melik\OneDrive\Belgeler\PetShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayEmployees()
        {
            Con.Open(); 
            string Query = "Select * from Employeetbl"; // SQL sorgusunu tanımlar. Bu sorgu, Employeetbl tablosundaki tüm kayıtları seçer.
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con); // SqlDataAdapter, veritabanından veri almayı ve veritabanına veri göndermeyi kolaylaştırır.
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda); // SqlCommandBuilder, SqlDataAdapter nesnesine bağlı olan veri komutlarını otomatik olarak oluşturur 
            var ds = new DataSet(); // Bir DataSet nesnesi oluşturur. DataSet, veritabanındaki verilerin bellek içi temsili olarak kullanılır.
            sda.Fill(ds); // SqlDataAdapter nesnesini kullanarak veritabanından verileri alır ve DataSet nesnesine doldurur.
            EmployeesDGV.DataSource = ds.Tables[0]; 
            Con.Close();
        }

        private void Clear()
        {
            EmpNameTb.Text = "";
            EmpAddTb.Text = "";
            EmpPhoneTb.Text = "";
            PasswordTb.Text = "";
        }
        int Key = 0;

        private void EmployeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpNameTb.Text = EmployeesDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpAddTb.Text = EmployeesDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpDOB.Text = EmployeesDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPhoneTb.Text = EmployeesDGV.SelectedRows[0].Cells[4].Value.ToString();
            PasswordTb.Text = EmployeesDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (EmpNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(EmployeesDGV.SelectedRows[0].Cells[0].Value.ToString());

            }

        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl (EmpName, EmpAdd, EmpDOB, EmpPhone, EmpPass) values (@EN, @EA, @ED, @EP, @EPa)", Con);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@ED", EmpDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@EPa", PasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Added!");
                    Con.Close();
                    DisplayEmployees();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Homes1 obj = new Homes1();
            obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Products1 obj = new Products1();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers1 obj = new Customers1();
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
    }
}
