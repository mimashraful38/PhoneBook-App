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

namespace PhoneBookApp
{
    public partial class PhoneBook : Form
    {
        SqlConnection connetion;
        SqlCommand cmd;
        DataSet ds;
        DataTable dt;


        public PhoneBook()
        {
            InitializeComponent();
            connetion = new SqlConnection(@"Data Source=localhost;Initial Catalog=PhoneBook;Integrated Security=true");
            //connetion = new SqlConnection(@"Data Source=DESKTOP-41977A0;Initial Catalog=Student_db;Integrated Security=true");
            //display();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (phonenoTextBox.Text == "" || firstNameTextBox.Text == "")
            {
                MessageBox.Show("Please Enter Name And Phone No");
            }
            else
            {
                SqlDataAdapter sqlAdapter = new SqlDataAdapter("select * FROM phones WHERE phoneno = '" + phonenoTextBox.Text + "'", connetion);
                DataTable dt = new DataTable();
                sqlAdapter.Fill(dt);
                //cmd = new SqlCommand("select * FROM phones WHERE phoneno='" + phonenoTextBox.Text + "'", connetion);
                //sqlceData da = new SqlDataAdapter(cmd);
                //da.Fill(ds);
                int i = dt.Rows.Count;
                if (i > 0)
                {
                    MessageBox.Show("Mobile No " +phonenoTextBox.Text + " Already Exists");
                    clear();
                }

                else
                {

                    try
                    {
                        connetion.Open();
                        string sqlQuery = "INSERT INTO Phones (firstname,lastname,phoneno,email,address) VALUES (@p1,@p2,@p3,@p4,@p5)";
                        cmd = new SqlCommand(sqlQuery, connetion);
                        cmd.Parameters.AddWithValue("@p1", firstNameTextBox.Text);
                        cmd.Parameters.AddWithValue("@p2", lastNameTextBox.Text);
                        cmd.Parameters.AddWithValue("@p3", phonenoTextBox.Text);
                        cmd.Parameters.AddWithValue("@p4", emailIdTextBox.Text);
                        cmd.Parameters.AddWithValue("@p5", AddressTextBox.Text);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        connetion.Close();
                        MessageBox.Show("Information Added Successfuly");
                        display();
                        clear();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }
        private void updateBtn_Click(object sender, EventArgs e)
        {


            if (phonenoTextBox.Text == "" || firstNameTextBox.Text=="")
            {
                MessageBox.Show("Please Enter Phone No");
            }
            else
            {

                try
                {
                    connetion.Open();
                    string sqlQuery = "update Phones set firstname=@firstname,lastname=@lastname,phoneno=@phoneno,email=@email,address=@address where id=@id";
                    cmd = new SqlCommand(sqlQuery, connetion);
                    cmd.Parameters.AddWithValue("@id", idtext.Text);
                    cmd.Parameters.AddWithValue("@firstname", firstNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@lastname", lastNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@phoneno", phonenoTextBox.Text);
                    cmd.Parameters.AddWithValue("@email", emailIdTextBox.Text);
                    cmd.Parameters.AddWithValue("@address", AddressTextBox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Updated Successfully");
                    connetion.Close();
                    display();
                    clear();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void deleteBtn_Click(object sender, EventArgs e)
        {

            try
            {
                connetion.Open();
                string sqlQuery = "delete Phones where id= @id";
                cmd = new SqlCommand(sqlQuery, connetion);
                cmd.Parameters.AddWithValue("@id", idtext.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted Successfully!");
                connetion.Close();
                display();
                clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void display()
        {
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("select * from Phones", connetion);
            DataTable dt = new DataTable();
            sqlAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    

        private void clear()
        {
            idtext.Text = "";
            firstNameTextBox.Text = "";
            lastNameTextBox.Text = "";
            emailIdTextBox.Text = "";
            phonenoTextBox.Text = "";
            AddressTextBox.Text = "";

        }
        private void clearBtn_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void searchtext_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            bs.Filter = "firstname like '%" + searchtext.Text + "%'";
            dataGridView1.DataSource = bs;

        }

        private void showbtn_Click(object sender, EventArgs e)
        {
            display();
        }

        int i;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            i = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[i];
            idtext.Text = row.Cells[0].Value.ToString();
            firstNameTextBox.Text = row.Cells[1].Value.ToString();
            lastNameTextBox.Text = row.Cells[2].Value.ToString();
            phonenoTextBox.Text = row.Cells[3].Value.ToString();
            emailIdTextBox.Text = row.Cells[4].Value.ToString();
            AddressTextBox.Text = row.Cells[5].Value.ToString();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
    }
}
