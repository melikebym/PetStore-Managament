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
    public partial class SignUp1 : Form
    {
        public SignUp1()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\melik\OneDrive\Belgeler\PetShopDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayEmployees()
        {
            Con.Open();
            string Query = "Select * from Employeetbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
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
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void LoginBtn_Click(object sender, EventArgs e)
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

                    SqlCommand checkPhone = new SqlCommand("SELECT COUNT(*) FROM EmployeeTbl WHERE EmpPhone = @EP", Con);
                    checkPhone.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                    int PhoneExist = (int)checkPhone.ExecuteScalar();

                    if (PhoneExist > 0)
                    {
                        // Telefon numarası zaten mevcut
                        MessageBox.Show("This phone number already exists!");
                        Con.Close();
                        return;
                    }

                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl (EmpName, EmpAdd,EmpDOB, EmpPhone, EmpPass) values (@EN, @EA,@ED, @EP, @EPa)", Con);
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Login1 obj = new Login1();
            obj.Show();
            this.Hide();
        }
    }
}
