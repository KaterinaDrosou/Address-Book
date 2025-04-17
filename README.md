# 📒 Address Book 
A simple Address Book app using Windows Forms App (.NET Framework) and SQLite
</br>

### 🔑 Key Features:
Upon launching the application, the user is presented with the following options on the main screen:
- Show All Contacts
- Add New Contact
- Search Contact
- Edit Contact
- Delete Contact
- Exit

The user can enter details for a new contact, including First Name, Last Name, Address, Phone Number, Email, Date of Birth and ID (Primary key in our database).

### ⚙️ Detailed Functionality

- **Show All Contacts** 👥: The user can view all contacts stored in the database.
- **Add New Contact** ➕: The user enters contact details, which are then stored in the database.
- **Search** 🔍: The user can search for a contact by name. If the contact exists, their details are displayed. 
- **Edit** ✏️: The user can edit and update the contact's details by searching a contact by name.
- **Delete** ❌: The user can delete a contact by entering the name in the provided field.

### 🗃️ Database Operations

- **Insert** 📝: Adds a new contact to the database.
- **Select** 🔍: Retrieves contact information based on the user's input.
- **Update** 🔄: Modifies existing contact details.
- **Delete** 🗑️: Removes a contact from the database.

### 💡User Experience Enhancements

The app includes message boxes (MessageBox) to notify the user of:

- Successful operations 🎉: e.g., "Contact added successfully!"
- Errors 🚫: e.g., "Contact not found!" or "Invalid input."

This improves the overall user-friendliness of the application and ensures clear communication between the app and the user.
