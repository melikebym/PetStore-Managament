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
    public partial class Billlings : Form
    {
        public Billlings()
        {
            InitializeComponent();
            // EmpNameLbl.Text = Login.Employee;
            GetCustomers();
            DisplayProduct();
            DisplayTransactions();
            UserNameLbl.Text = Login1.User;
            CustomizeDataGridView();

        }
        private void CustomizeDataGridView()
        {
            // DataGridView'in ana sütun (header) rengini ayarlayın
            ProductsDGV.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Brown;
            ProductsDGV.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            ProductsDGV.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);


            // Alternatif olarak diğer stilleri de ayarlayabilirsiniz
            ProductsDGV.EnableHeadersVisualStyles = false;

            BillDGV.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Brown;
            BillDGV.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            BillDGV.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            BillDGV.EnableHeadersVisualStyles = false;

            TransactionsGDV.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Brown;
            TransactionsGDV.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            TransactionsGDV.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            TransactionsGDV.EnableHeadersVisualStyles = false;
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\melik\OneDrive\Belgeler\PetShopDb.mdf;Integrated Security=True;Connect Timeout=30");

        int Stock = 0 , Key=0 ;
        private void Reset()
        {
            PrNameTb.Text = "";
            QtyTb.Text = "";
            Stock = 0;
            Key = 0 ;
        }
        private void GetCustomers()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CustId from CustomerTbl" , Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId" ,  typeof(int));
            dt.Load(Rdr);
            CustIdCb.ValueMember = "CustId";
            CustIdCb.DataSource = dt;
            Con.Close();
        }

        private void DisplayProduct()
        {
            Con.Open();
            string Query = "Select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query , Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void DisplayTransactions()
        {
            Con.Open();
            string Query = "Select * from BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TransactionsGDV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void GetCustName()
        {

            try
            {
                Con.Open();
                string query = "SELECT * FROM CustomerTbl WHERE CustId = @CustId";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@CustId", CustIdCb.SelectedValue); // Parametreize sorgu kullanımı

                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                if (dt.Rows.Count > 0) // Eğer sonuç bulunduysa
                {
                    DataRow dr = dt.Rows[0]; // İlk satırı al
                    CustNameTb.Text = dr["CustName"].ToString();
                }
                Con.Close();

            }
            catch 
            {
            }
            finally
            {
                Con.Close(); // Bağlantıyı kapat
            }
        }

        private void UpdateStock()
        {
            try
            {
                
                int NewQty = Stock - Convert.ToInt32(QtyTb.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update ProductTbl set PrQty=@PQ where PrId=@PKey" , Con);
                cmd.Parameters.AddWithValue("@PQ", NewQty);
                cmd.Parameters.AddWithValue("@PKey", Key );
                cmd.ExecuteNonQuery();
                Con.Close();
                DisplayProduct();

            }catch (Exception Ex) 
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int n = 0, GrdTotal= 0;

        private void CustIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustName();
        }

        private void ResetTb_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ProductsDGV_CellContentClick(object sender, EventArgs e)
        {
            PrNameTb.Text = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();
            Stock = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[3 ].Value.ToString());
            PrpriceTb.Text = ProductsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PrNameTb.Text == "")
            {
                Key = 0;
            } else
            {
                Key = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PrNameTb.Text = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();
            Stock = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[3].Value.ToString());
            PrpriceTb.Text = ProductsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PrNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void InsertBill()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into BillTbl (BDate, CustId, CustName, EmpName, Amt) values (@BD, @CI, @CN, @EN, @Am)", Con);
                cmd.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                cmd.Parameters.AddWithValue("@CI", CustIdCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                cmd.Parameters.AddWithValue("@EN", UserNameLbl.Text);
                cmd.Parameters.AddWithValue("@Am", GrdTotal);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Fatura Kaydedildi!");
                Con.Close();
                DisplayTransactions();
              //  Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm" , 285, 300);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            InsertBill();
        }
        int prodid, prodqty, prodprice, tottal, pos = 60;

        private void label1_Click_1(object sender, EventArgs e)
        {
            Customers1 obj = new Customers1();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Products1 obj = new Products1();
            obj.Show();
            this.Hide();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            Employees1 obj = new Employees1();
            obj.Show();
            this.Hide();
        }

        private void label6_Click_1(object sender, EventArgs e)
        {
            Login1 obj = new Login1();
            obj.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void CustNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            Homes1 obj = new Homes1();
            obj.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        string prodname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("To Go Pet Store", new Font("Cambria", 14, FontStyle.Bold),Brushes.Crimson, new Point(80));
            e.Graphics.DrawString("No  Ürün        Adet       Fiyat       Toplam ", new Font("Cambria", 10, FontStyle.Bold), Brushes.Crimson, new Point(30,40));
            foreach (DataGridViewRow row in BillDGV.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Id"].Value);
                prodname = "" + row.Cells["PrName"].Value;
                prodprice = Convert.ToInt32(row.Cells["Price"].Value);
                prodqty = Convert.ToInt32(row.Cells["Qty"].Value);
                tottal = Convert.ToInt32(row.Cells["Total"].Value);
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26,pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + tottal, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Toplam Fiyat: $" + GrdTotal, new Font("Cambria", 9, FontStyle.Bold), Brushes.Brown, new Point(50, pos + 50));
            e.Graphics.DrawString("İyi günler, yine bekleriz!", new Font("Cambria", 10, FontStyle.Bold), Brushes.Brown, new Point(10, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void AddTb_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "" || Convert.ToInt32 (QtyTb.Text) > Stock)
            {
                MessageBox.Show("No Enough In House");
            } else if (QtyTb.Text =="" || Key == 0)
            {
                MessageBox.Show("Missing Information");
            } else
            {

                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PrpriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1 ;
                newRow.Cells[1].Value = PrNameTb.Text;
                newRow.Cells[2].Value = QtyTb.Text;
                newRow.Cells[3].Value = PrpriceTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                GrdTotal = GrdTotal + total;
                n++;
                TotalLbl.Text = "$: " + GrdTotal;
                UpdateStock();
                Reset();
            }
        }
    }
}
