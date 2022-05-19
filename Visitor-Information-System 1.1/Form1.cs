using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visitor_Information_System_1._1
{
    public partial class Form1 : Form
    {
        //connection string to connect to database
        string connString = @"Data Source=NSK-NOTE06\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        int Visitor_ID = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListBox_Data_Load(); // Calling function to load data in the ListBox from Student_Info Table
            Course_Data_Load(); // Calling function to load data in the ComboBox from Course_Details Table
        }

        public void Clear_Fields() // Function to clear all fields as well as ListBox
        {
            TB_Visitor_ID.Clear();
            TB_VisitorName.Clear();
            TB_VisitorSurname.Clear();
            TB_Email.Clear();
            TB_Visitor_ID.Clear();
            CB_Meeting_With.Text = "Select From Drop Down";
            LB_Visitor_Details.Items.Clear();
            TB_Staff_ID.Clear();
        }

        public void ListBox_Data_Load()
        {
            Clear_Fields(); // Calling  Clear_Field function 

            // Creating instance of SqlConnection 
            SqlConnection conn = new SqlConnection(connString);

            // set the sql command ( Statement ) 
            string sql_Query = "select Visitor.Visitor_Id, Visitor.VisitorName, Visitor.SurName,Visitor.Mobile, Visitor.Email,Staff.Staff_ID, Staff.Meeting_With, From Visitor, Staff Where Visitor.Staff_ID = Staff.Staff_ID";

            // Creating instance of SqlCommand  and set the connection and query to instance of SqlCommand
            SqlCommand cmd = new SqlCommand(sql_Query, conn);

            //Open connection
            conn.Open();

            // Creating instance of SqlDataReader 
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //populate data in Listbox from Reader
                LB_Visitor_Details.Items.Add((reader["Visitor_Id"] + "-" + reader["VisitorName"] + "-" + reader["Surname"] + "-" + reader["Mobile"] + "-" + reader["Email"] + "-" + reader["Meeting_With"]));
            }

            //Close Database reader
            reader.Close();

            //Close connection
            conn.Close();

        }

        public void Course_Data_Load() //Meeting_With_Data-???//
        {
            // Creating instance of SqlConnection 
            SqlConnection conn = new SqlConnection(connString);

            // set the sql command ( Statement )
            string sql_Query2 = "Select Staff_Id,Meeting-With From Staff";

            // Creating instance of SqlCommand  and set the connection and query to instance of SqlCommand
            SqlCommand cmd2 = new SqlCommand(sql_Query2, conn);

            //Open connection
            conn.Open();

            // Creating instance of SqlDataReader 
            SqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                //populate data in ComboBox from Reader
                CB_Meeting_With.Items.Add(reader["Meeting_With"]);

            }

            //Close Database reader
            reader.Close();

            //Close connection
            conn.Close();
        }

        private void btn_Add_Click(object sender, EventArgs e) //What is the button ADD??//
        {

            // Creating instance of SqlConnection 
            SqlConnection conn = new SqlConnection(connString);

            // set the sql command ( Statement )
            string sql_Query3 = "Insert into Visitor (VisitorName, Surname, Mobile, Email, Meeting_With) values ('" + TB_VisitorName.Text + "','" + TB_VisitorSurname.Text + "','" + TB_Mobile.Text + "','" + TB_Email.Text + "', '" + TB_Staff_ID.Text +" )";
            //FINISHED HERE//
            // Creating instance of SqlCommand  and set the connection and query to instance of SqlCommand
            SqlCommand cmd4 = new SqlCommand(sql_Query3, conn);

            //Open connection
            conn.Open();

            cmd4.ExecuteNonQuery();

            MessageBox.Show("Record Saved"); // showing messagebox for confirmation message for user  
            //Close connection
            conn.Close();

            ListBox_Data_Load(); // Calling load listbox function to fetch inserted row as well as display in ListBox
        }
        private void Course_ID_Function(object sender, EventArgs e)
        {
            // Creating instance of SqlConnection 
            SqlConnection conn = new SqlConnection(connString);

            // set the sql command ( Statement )
            string Course_ID_Query = "Select Course_Id from Course_Details where Course_Name ='" + CB_Course_Details.SelectedItem.ToString() + "'";

            // Creating instance of SqlCommand  and set the connection and query to instance of SqlCommand
            SqlCommand cmd3 = new SqlCommand(Course_ID_Query, conn);

            //Open connection
            conn.Open();

            // Creating instance of SqlDataReader 
            SqlDataReader reader = cmd3.ExecuteReader();
            while (reader.Read())
            {
                //populate data in TextBox from Reader
                TB_Course_ID.Text = reader["Course_Id"].ToString();
            }

            //Close connection
            conn.Close();
        }
        private void LB_Student_MouseClick(object sender, MouseEventArgs e)
        {
            var selectedValue = LB_Student.SelectedItem;
            if (selectedValue != null)
            {
                MessageBox.Show(selectedValue.ToString());
            }
            string StudentData = LB_Student.SelectedItem.ToString();
            string[] Field_Data = StudentData.Split('-');
            Stud_ID = Int16.Parse(Field_Data[0]);

            // Creating instance of SqlConnection 
            SqlConnection conn = new SqlConnection(connString);

            // set the sql command ( Statement ) 
            string sql_Query = "select Student_Info.Id, Student_Info.Name, Student_Info.Mobile, Student_Info.Email,Course_Details.Course_ID, Course_Details.Course_Name From Student_Info, Course_Details Where Student_Info.Course_ID = Course_Details.Course_ID AND Id =" + Stud_ID;

            // Creating instance of SqlCommand  and set the connection and query to instance of SqlCommand
            SqlCommand cmd = new SqlCommand(sql_Query, conn);

            //Open connection
            conn.Open();

            // Creating instance of SqlDataReader 
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //populate data in TextBox from Reader
                TB_Student_ID.Text = reader["Id"].ToString();
                TB_Name.Text = reader["Name"].ToString();
                TB_Mobile.Text = reader["Mobile"].ToString();
                TB_Email.Text = reader["Email"].ToString();
                CB_Course_Details.Text = reader["Course_Name"].ToString();
                TB_Course_ID.Text = reader["Course_Id"].ToString();
            }

            //Close Database reader
            reader.Close();

            //Close connection
            conn.Close();

        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            // Creating instance of SqlConnection 
            SqlConnection conn = new SqlConnection(connString);

            // set the sql command ( Statement )
            string sql_Query4 = "Update Student_Info set Name = '" + TB_Name.Text + "', Mobile ='" + TB_Mobile.Text + "', Email ='" + TB_Email.Text + "', Course_ID =" + TB_Course_ID.Text + " Where Id=" + TB_Student_ID.Text;

            MessageBox.Show(sql_Query4);
            // Creating instance of SqlCommand  and set the connection and query to instance of SqlCommand
            SqlCommand cmd5 = new SqlCommand(sql_Query4, conn);

            //Open connection
            conn.Open();

            cmd5.ExecuteNonQuery();

            MessageBox.Show("Record Updated"); // showing messagebox for confirmation message for user  
            //Close connection
            conn.Close();

            ListBox_Data_Load(); // Calling load listbox function to fetch inserted row as well as display in ListBox

        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            // Creating instance of SqlConnection 
            SqlConnection conn = new SqlConnection(connString);

            // set the sql command ( Statement )
            string sql_Query = "Delete from Student_Info where Id = " + TB_Student_ID.Text;

            // Creating instance of SqlCommand  and set the connection and query to instance of SqlCommand
            SqlCommand cmd5 = new SqlCommand(sql_Query, conn);

            //Open connection
            conn.Open();

            cmd5.ExecuteNonQuery();

            MessageBox.Show("Record Deleted"); // showing messagebox for confirmation message for user  
            //Close connection
            conn.Close();

            ListBox_Data_Load(); // Calling load listbox function to fetch inserted row as well as display in ListBox

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_Meeting_With_Click(object sender, EventArgs e)
        {

        }
    }
}

