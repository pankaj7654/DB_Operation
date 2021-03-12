using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace db_poperation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cnn = new SqlConnection(@"Data Source=DESKTOP-BA8E536\SQLEXPRESS;Initial Catalog=database1;Integrated Security=True");
        public int roll;
        

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentRecord();

        }

        private void GetStudentRecord()
        {
            
            SqlCommand cmd = new SqlCommand("Select * from s_details1", cnn);
            DataTable dt = new DataTable();

            cnn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            cnn.Close();

            dataGridView1.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Isvalid())
            {
                SqlCommand cmd = new SqlCommand("insert into s_details1 values(@roll,@name,@father,@city,@contact)",cnn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@roll",textBox1.Text);
                cmd.Parameters.AddWithValue("@name", textBox2.Text);
                cmd.Parameters.AddWithValue("@father", textBox3.Text);
                cmd.Parameters.AddWithValue("@city", textBox4.Text);
                cmd.Parameters.AddWithValue("@contact", textBox5.Text);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

                MessageBox.Show("new student is successfully inserted");
                GetStudentRecord();
                resetFormControl();
            }
        }

        private bool Isvalid()
        {
            if(textBox1.Text == string.Empty)
            {
                MessageBox.Show("Roll number is required");
                return false;
            }
            return true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            resetFormControl();
        }

        private void resetFormControl()
        {
            roll = 0;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            textBox1.Focus();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            roll = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(roll > 0)
            {
                SqlCommand cmd = new SqlCommand("update s_details1 set name=@name,father=@father,city=@city,contact=@contact where roll=@a", cnn);
                cmd.CommandType = CommandType.Text;
                
                cmd.Parameters.AddWithValue("@name", textBox2.Text);
                cmd.Parameters.AddWithValue("@father", textBox3.Text);
                cmd.Parameters.AddWithValue("@city", textBox4.Text);
                cmd.Parameters.AddWithValue("@contact", textBox5.Text);
                cmd.Parameters.AddWithValue("@a", this.roll);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

                MessageBox.Show("data updated successfully");
                GetStudentRecord();
                resetFormControl();
            }
            else
            {
                MessageBox.Show("please select a rows to update");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(roll > 0)
            {
                SqlCommand cmd = new SqlCommand("delete from s_details1 where roll=@a", cnn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@a", this.roll);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

                MessageBox.Show("data deleted successfully from table");
                GetStudentRecord();
                resetFormControl();
            }
            else
            {
                MessageBox.Show("please select rows to delete");
            }
        }
    }
}
