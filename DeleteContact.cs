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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AddressBook
{
    public partial class DeleteContact : Form
    {
        String connectionString = "Data source=AddressBook.db;Version=3;";
        SQLiteConnection connection;
        public DeleteContact()
        {
            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show("Please enter a name!");
                return;
            }

            comboBox1.Items.Clear();
            connection.Open();

            string searchSQL = "SELECT Lastname, ID from Addressbook WHERE Firstname LIKE @Firstname";
            SQLiteCommand command = new SQLiteCommand(searchSQL, connection);
            command.Parameters.AddWithValue("@Firstname", "%" + textBox8.Text + "%");
            SQLiteDataReader reader = command.ExecuteReader();

            bool found = false;
            while (reader.Read())
            {
                found = true;
                string Lastname = reader["Lastname"].ToString();
                string ID = reader["ID"].ToString();
                comboBox1.Items.Add($"{Lastname} - {ID}");
            }

            if (!found)
            {
                MessageBox.Show("No records found for this name!");
            }

            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection(connectionString);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;

            string selectedItem = comboBox1.SelectedItem.ToString();
            string[] parts = selectedItem.Split('-');
            string Lastname = parts[0].Trim();
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please choose a contact!");
                return;
            }

            string selectedItem = comboBox1.SelectedItem.ToString();
            string[] parts = selectedItem.Split('-');
            string Lastname = parts[0].Trim();
            string ID = parts[1].Trim(); // We get the name from the selected contact


            connection.Open();

            //Delete data for one person
            string deleteSQL = "DELETE FROM Addressbook WHERE Lastname = @Lastname AND ID = @ID";
            SQLiteCommand command = new SQLiteCommand(deleteSQL, connection);
            command.Parameters.AddWithValue("@Lastname", Lastname);
            command.Parameters.AddWithValue("@ID", ID);

            int deleted = command.ExecuteNonQuery();
            if (deleted > 0)
            {
                MessageBox.Show("Contact deleted successfully!");
                comboBox1.Items.Remove(comboBox1.SelectedItem);
            }
            else
            {
                MessageBox.Show("Failed to delete contact!");
            }
            command.Dispose();
            connection.Close();
        }
    }
}
