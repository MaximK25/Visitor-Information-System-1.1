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
            Staff_Data_Load(); // Calling function to load data in the ComboBox from Course_Details Table
        }

        public void Clear_Fields() // Function to clear all fields as well as ListBox
        {
            TB_Visitor_ID.Clear();
            TB_VisitorName.Clear();
            TB_VisitorSurname.Clear();
            TB_Email.Clear();
            TB_Mobile.Clear();
            CB_Meeting_With.Text = "Select From Drop Down";
            TB_Staff_ID.Clear();
            LB_Visitor_Details.Items.Clear();

        }

        public void ListBox_Data_Load()
        {
            Clear_Fields(); // Calling  Clear_Field function 

            // Creating instance of SqlConnection 
            SqlConnection conn = new SqlConnection(connString);

            // set the sql command ( Statement ) 
            string sql_Query = "select Visitor.Visitor_Id, Visitor.VisitorName, Visitor.SurName, Visitor.Mobile, Visitor.Email, Staff.Staff_ID, Staff.Meeting_With, From Visitor, Staffr Where Visitor.Staff_ID = Staff.Staff_ID";

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

        public void Staff_Data_Load() //Meeting_With_Data-?????????????????????//
        {
            // Creating instance of SqlConnection 
            SqlConnection conn = new SqlConnection(connString);

            // set the sql command ( Statement )
            string sql_Query2 = "Select Staff_Id, Meeting-With From Staff";

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

        private void Btn_Add_Click(object sender, EventArgs e)
        {

            // Creating instance of SqlConnection 
            SqlConnection conn = new SqlConnection(connString);

            // set the sql command ( Statement )
            string sql_Query3 = "Insert into Visitor (VisitorName, Surname, Mobile, Email, Meeting_With) values ('" + TB_VisitorName.Text + "','" + TB_VisitorSurname.Text + "','" + TB_Mobile.Text + "','" + TB_Email.Text + "', '" + TB_Staff_ID.Text + " )";

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
        private void Staff_ID_Function(object sender, EventArgs e)
        {
            // Creating instance of SqlConnection 
            SqlConnection conn = new SqlConnection(connString);

            // set the sql command ( Statement )
            string Staff_ID_Query = "Select Staff_Id from Staff where Meeting_With ='" + CB_Meeting_With.SelectedItem.ToString() + "'";

            // Creating instance of SqlCommand  and set the connection and query to instance of SqlCommand
            SqlCommand cmd3 = new SqlCommand(Staff_ID_Query, conn);

            //Open connection
            conn.Open();

            // Creating instance of SqlDataReader 
            SqlDataReader reader = cmd3.ExecuteReader();
            while (reader.Read())
            {
                //populate data in TextBox from Reader
                TB_Staff_ID.Text = reader["Staff_Id"].ToString();
            }

            //Close connection
            conn.Close();
        }
        private void LB_Visitor_Details_MouseClick(object sender, MouseEventArgs e)
        {
            var selectedValue = LB_Visitor_Details.SelectedItem;
            if (selectedValue != null)
            {
                MessageBox.Show(selectedValue.ToString());
            }
            string VisitorData = LB_Visitor_Details.SelectedItem.ToString();
            string[] Field_Data = VisitorData.Split('-');
            Visitor_ID = Int16.Parse(Field_Data[0]);

            // Creating instance of SqlConnection 
            SqlConnection conn = new SqlConnection(connString);

            // set the sql command ( Statement ) 
            string sql_Query = "select Visitor.Visitor_Id, Visitor.VisitorName, Visitor.SurName, Visitor.Mobile, Visitor.Email,Staff.Staff_ID, Staff.Meetung_With From Visitor, Staff Where Visitor.Staff_ID = Staff.Staff_ID AND Visitor_Id =" + Visitor_ID;

            // Creating instance of SqlCommand  and set the connection and query to instance of SqlCommand
            SqlCommand cmd = new SqlCommand(sql_Query, conn);

            //Open connection
            conn.Open();

            // Creating instance of SqlDataReader 
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //populate data in TextBox from Reader
                TB_Visitor_ID.Text = reader["Visitor_Id"].ToString();
                TB_VisitorName.Text = reader["VisitorName"].ToString();
                TB_VisitorSurname.Text = reader["Surname"].ToString();
                TB_Mobile.Text = reader["Mobile"].ToString();
                TB_Email.Text = reader["Email"].ToString();
                CB_Meeting_With.Text = reader["Meeting_WIth"].ToString();
                TB_Staff_ID.Text = reader["Staff_Id"].ToString();
            }

            //Close Database reader
            reader.Close();

            //Close connection
            conn.Close();

        }

        private void Btn_Update_Click_1(object sender, EventArgs e)
        {
            // Creating instance of SqlConnection 
            SqlConnection conn = new SqlConnection(connString);

            // set the sql command ( Statement )
            string sql_Query4 = "Update Visitor set Visitor Name = '" + TB_VisitorName.Text + "', Surname ='" + TB_VisitorSurname.Text + "', Mobile ='" + TB_Mobile.Text + "', Email ='" + TB_Email.Text + "', Staff_ID =" + TB_Staff_ID.Text + " Where Visitor_Id=" + TB_Visitor_ID.Text;

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

        private void Btn_Delete_Click_1(object sender, EventArgs e)
        {
            // Creating instance of SqlConnection 
            SqlConnection conn = new SqlConnection(connString);

            // set the sql command ( Statement )
            string sql_Query = "Delete from Visitor where Visitor_Id = " + TB_Visitor_ID.Text;

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

        private void Staff_ID_Funtion(object sender, EventArgs e)
        {

        }

        private void Btn_Insert_Click(object sender, EventArgs e)
        {

        }        
    }
}

