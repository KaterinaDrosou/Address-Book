using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AddressBook
{
    public partial class SearchContact : Form
    {
        String connectionString = "Data source=AddressBook.db;Version=3;";
        SQLiteConnection connection;

        public SearchContact()
        {
            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a name!");
                return;
            }
              comboBox1.Items.Clear();  // Clears the list before adding the new items

            connection.Open();

            string searchSQL = "Select Lastname, ID from Addressbook WHERE Firstname LIKE @Firstname";
            SQLiteCommand command = new SQLiteCommand(searchSQL, connection);
            command.Parameters.AddWithValue("@Firstname", "%" + textBox1.Text + "%"); // Add parameter with Firstname
            SQLiteDataReader reader = command.ExecuteReader();

            bool found = false;
            while (reader.Read())
            {
                found = true;
                string Lastname = reader["Lastname"].ToString();
                string ID = reader["ID"].ToString();
                comboBox1.Items.Add($"{Lastname} - {ID}");
            }

            if (comboBox1.Items.Count == 0)
            {
                MessageBox.Show("No records found for this name!");
            }

            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection(connectionString);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;

            string selectedItem = comboBox1.SelectedItem.ToString();
            string[] parts = selectedItem.Split('-');  //Separates the LastName from the ID with -
            string Lastname = parts[0].Trim(); //Trim removes all spaces present in the string
            string ID = parts[1].Trim();

            connection.Open();
            string detailsSQL = "SELECT * FROM Addressbook WHERE Lastname = @Lastname AND ID = @ID";
            SQLiteCommand command = new SQLiteCommand(detailsSQL, connection);
            command.Parameters.AddWithValue("@Lastname", Lastname);
            command.Parameters.AddWithValue("@ID", ID);
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                richTextBox1.Clear();
                richTextBox1.AppendText($"First Name: {reader["Firstname"].ToString()}\n");
                richTextBox1.AppendText($"Last Name: {reader["Lastname"].ToString()}\n");
                richTextBox1.AppendText($"Phone Number: {reader["PhoneNumber"].ToString()}\n");
                richTextBox1.AppendText($"Address: {reader["Address"].ToString()}\n");
                richTextBox1.AppendText($"Email: {reader["email"].ToString()}\n");
                richTextBox1.AppendText($"Date of Birth: {reader["Birthday"].ToString()}\n");
                richTextBox1.AppendText($"ID: {reader["ID"].ToString()}\n");
            }
            else
            {
                MessageBox.Show("No records found for this contact!");
            }

            reader.Close();
            command.Dispose();
            connection.Close();
        }
    }
}