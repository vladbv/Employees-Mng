using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace EmpManagementTuto
{
    public partial class Salary : Form
    {
        public Salary()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Employees-Mng\EmpManagementTuto\Employees-Mng\Employees-Db.mdf;Integrated Security=True;Connect Timeout=30");
        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
        private void fetchempdata()
        {

            if(EmpIdTb.Text == "")
            {
                MessageBox.Show("Enter Employee Id");
            }else
            {
                Con.Open();
                string query = "select * from EmployeeTbl where EmpId='" + EmpIdTb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {

                    EmpNameTb.Text = dr["Empname"].ToString();

                    EmpPosTb.Text = dr["Emppos"].ToString();


                }
                Con.Close();
            }
               
        }
        private void Salary_Load(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            fetchempdata();
        }
        int Dailybase,total;

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("=======SALARY DOCUMENT=======", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Red, new Point(180));
            e.Graphics.DrawString("Employee ID: " + EmpIdTb.Text, new Font("Century Gothic", 18, FontStyle.Regular), Brushes.Blue, new Point(20, 100));
           // e.Graphics.DrawString("Employee Address: ", new Font("Century Gothic", 18, FontStyle.Regular), Brushes.Blue, new Point(10, 140));
            e.Graphics.DrawString("Employee Position: "+EmpPosTb.Text, new Font("Century Gothic", 18, FontStyle.Regular), Brushes.Blue, new Point(20, 140));
            e.Graphics.DrawString("Worked Days: " + WorkedTb.Text, new Font("Century Gothic", 18, FontStyle.Regular), Brushes.Blue, new Point(20, 180));
            e.Graphics.DrawString("Daily Pay: " + Dailybase, new Font("Century Gothic", 18, FontStyle.Regular), Brushes.Blue, new Point(20, 220));
            e.Graphics.DrawString("Total Salary: " + total, new Font("Century Gothic", 18, FontStyle.Regular), Brushes.Blue, new Point(20, 260));

            e.Graphics.DrawString("=======EmpiSoft=======", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Red, new Point(180, 300));
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if(EmpPosTb.Text == "")
            {
                MessageBox.Show("Select An Employee");
            }
            else if(WorkedTb.Text == "" || Convert.ToInt32(WorkedTb.Text) > 28)
            {
                MessageBox.Show("Enter A Valid Number of Days");
            }else
            {
                if(EmpPosTb.Text =="Manager")
                {
                    Dailybase = 1200;
                }else if(EmpPosTb.Text == "Senior Developper")
                {
                    Dailybase = 1000;
                }else if(EmpPosTb.Text == "Junior Developper")
                {
                    Dailybase = 950;
                }else
                {
                    Dailybase = 850;
                }
                total = Dailybase * Convert.ToInt32(WorkedTb.Text);
                SalarySlip.Text = "Employee Id:   "+EmpIdTb.Text + "\n" + "Employee Name:   "+EmpNameTb.Text + "\n" + "Employee Position:   "+EmpPosTb.Text + "\n" + "Days Worked:   "+WorkedTb.Text + "\n" + "Daily Salary Rs:   "+Dailybase + "\n" + "Total Amount Rs:   "+total;

            }
                   
        }
    }
}
