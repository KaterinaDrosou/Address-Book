using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook
{
    public partial class AddContact : Form
    {
        String connectionString = "Data source=AddressBook.db;Version=3;";
        SQLiteConnection connection;
        public AddContact()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            connection.Open();

            // Insert new personal data
            string insertSQL = "Insert into Addressbook values(@Firstname,@Lastname,@PhoneNumber,@Address,@email,@Birthday,@ID)";
            SQLiteCommand command = new SQLiteCommand(insertSQL,connection);
            command.Parameters.AddWithValue("@Firstname",textBox1.Text);
            command.Parameters.AddWithValue("@Lastname",textBox2.Text);
            command.Parameters.AddWithValue("@PhoneNumber",textBox3.Text);
            command.Parameters.AddWithValue("@Address",textBox4.Text);
            command.Parameters.AddWithValue("@email",textBox5.Text);
            command.Parameters.AddWithValue("@Birthday",textBox6.Text);
            command.Parameters.AddWithValue("ID",textBox7.Text);
            command.ExecuteNonQuery();
            command.Dispose();

            connection.Close();

            MessageBox.Show("The addition was successful!");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection(connectionString);
        }
    }
}