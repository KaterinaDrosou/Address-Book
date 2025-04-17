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
    public partial class EditContact : Form
    {
        String connectionString = "Data source=AddressBook.db;Version=3;";
        SQLiteConnection connection;
        public EditContact()
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
            
            // Select data from DB Addressbook
            string searchSQL = "Select * from Addressbook WHERE Firstname LIKE @Firstname";
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

        private void Form3_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection(connectionString);

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            connection.Open();
           
            //Update data for one person
            string updateSQL = "UPDATE Addressbook SET Firstname = @Firstname, Lastname = @Lastname, PhoneNumber = @PhoneNumber, Address = @Address, email = @email, Birthday = @Birthday WHERE ID = @ID";
            SQLiteCommand command = new SQLiteCommand(updateSQL, connection);
            command.Parameters.AddWithValue("@Firstname", textBox1.Text);
            command.Parameters.AddWithValue("@Lastname", textBox2.Text);
            command.Parameters.AddWithValue("@PhoneNumber", textBox3.Text);
            command.Parameters.AddWithValue("@Address", textBox4.Text);
            command.Parameters.AddWithValue("@email", textBox5.Text);
            command.Parameters.AddWithValue("@Birthday", textBox6.Text);
            command.Parameters.AddWithValue("ID", textBox7.Text);

            int updated = command.ExecuteNonQuery();
            command.Dispose();

            if (updated > 0)
            {
                MessageBox.Show("Changes saved successfully!");
            }
            else
            {
                MessageBox.Show("Failed to save changes!");
            }

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
                    textBox1.Text = reader.GetString(0);
                    textBox2.Text = reader.GetString(1);
                    textBox3.Text = reader.GetString(2);
                    textBox4.Text = reader.GetString(3);
                    textBox5.Text = reader.GetString(4);
                    textBox6.Text = reader.GetString(5);
                    textBox7.Text = reader.GetString(6);

                }
                else
                {
                    MessageBox.Show("No details found for this contact.");
                }

                reader.Close();
                command.Dispose();
                connection.Close();
        }
    }
}