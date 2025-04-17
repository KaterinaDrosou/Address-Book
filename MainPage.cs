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
    public partial class MainPage : Form
    {
        String connectionString = "Data source=AddressBook.db;Version=3;";
        SQLiteConnection connection;
        public MainPage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection(connectionString);
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            richTextBoxContacts.Clear();   //clearing the richTextBox so that every time the button is pressed it overlaps
            connection.Open();

            String selectSQL = "Select * from Addressbook";
            SQLiteCommand command = new SQLiteCommand(selectSQL, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                richTextBoxContacts.AppendText(reader.GetString(0)); 
                richTextBoxContacts.AppendText("\n");
                richTextBoxContacts.AppendText(reader.GetString(1));
                richTextBoxContacts.AppendText("\n");
                richTextBoxContacts.AppendText(reader.GetString(2));
                richTextBoxContacts.AppendText("\n");
                richTextBoxContacts.AppendText(reader.GetString(3));
                richTextBoxContacts.AppendText("\n");
                richTextBoxContacts.AppendText(reader.GetString(4));
                richTextBoxContacts.AppendText("\n");
                richTextBoxContacts.AppendText(reader.GetString(5));
                richTextBoxContacts.AppendText("\n");
                richTextBoxContacts.AppendText(reader.GetString(6));
                richTextBoxContacts.AppendText("\n\n");

            }
            reader.Close();
            command.Dispose();
            connection.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new AddContact().Show();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            new SearchContact().Show();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            new EditContact().Show();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            new DeleteContact().Show();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}