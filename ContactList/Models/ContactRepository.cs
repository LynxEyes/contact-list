using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static ContactList.Services.Database;

namespace ContactList.Models {
    public class ContactRepository : IContactRepository {

        //private SQLiteConnection DB { get; } = Database.DB; 
        public ObservableCollection<Contact> Contacts { get; private set; }

        public ContactRepository() {
            DB.DropTable<Contact>();
            DB.CreateTable<Contact>();

            //DB.InsertOrReplace(new Contact("Rafael"));
            //DB.InsertOrReplace(new Contact("Carlos"));
            //DB.InsertOrReplace(new Contact("Alberto"));

            Contacts = new ObservableCollection<Contact>(GetContacts());
        }

        public IList<Contact> GetContacts() {
            return (from contact in DB.Table<Contact>() orderby contact.Name select contact).ToList();
        }

        public bool SaveContact(Contact contact) {

            try {
                DB.Insert(contact);
            } catch (SQLite.NotNullConstraintViolationException) {
                return false;
            }

            Contacts.Add(contact);
            // TODO: is there a better way?
            return true;
        }
    }
}
