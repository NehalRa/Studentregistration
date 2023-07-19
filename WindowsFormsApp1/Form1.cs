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


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();
        }
        
       
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\OneDrive\Documents\Studentregister.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;
        SqlDataReader read;
        SqlDataAdapter drr;
        string id;
        bool mode = true;
        String sql;

        //if the mode is true mean add the record otherwise update the record.
        

        public void Load()
        {
             try
            {

                sql = "select * from Stable";
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                read = cmd.ExecuteReader();
                //no need
                //drr = new SqlDataAdapter(sql, conn);
                dataGridView1.Rows.Clear();

                while(read.Read())
                {
                    //iid , name , course , fees for read 
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                conn.Close();


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void getid(String id)
        {
            sql = "select * from Stable where id = '" + id +"' ";
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            read = cmd.ExecuteReader();
            while(read.Read())
            {
                txtName.Text = read[1].ToString();
                txtcourse.Text = read[2].ToString();
                txtfees.Text = read[3].ToString();



            }
            conn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String name = txtName.Text;
            String course = txtcourse.Text;
            String fees = txtfees.Text;

            try
            {
                if (mode == true)
                {
                    sql = "insert into Stable(SName,Course,Fees) values(@SName,@Course,@Fees)";
                    conn.Open();
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@SName", name);
                    cmd.Parameters.AddWithValue("@Course", course);
                    cmd.Parameters.AddWithValue("@Fees", fees);
                   /// MessageBox.Show("Record Added");
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Added");


                    txtName.Clear();
                    txtcourse.Clear();
                    txtfees.Clear();
                    txtName.Focus();




                }
                else
                {
                   
                    id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    sql = "update Stable  set SName = @SName , Course= @Course, Fees = @Fees where id = @id";
                    conn.Open();
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@SName", name);
                    cmd.Parameters.AddWithValue("@Course", course);
                    cmd.Parameters.AddWithValue("@Fees", fees);
                    cmd.Parameters.AddWithValue("@id", id);

                    /// MessageBox.Show("Record Added");
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record updated");


                    txtName.Clear();
                    txtcourse.Clear();
                    txtfees.Clear();
                    txtName.Focus();
                    button1.Text = "Save";
                    mode = true;



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
         }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0 )
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getid(id);
                button1.Text = "Edit";

            }
            else if(e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from Stable where id = @id";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("record deleted.");

                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtcourse.Clear();
            txtfees.Clear();
            txtName.Focus();
            button1.Text = "Save";
            mode = true;

        }
    }
    

    }
    

