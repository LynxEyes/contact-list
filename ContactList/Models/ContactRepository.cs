using System;
using System.Collections.Generic;
using System.Linq;
using static ContactList.Services.Database;

namespace ContactList.Models {
    public class ContactRepository : IContactRepository {

        //private SQLiteConnection DB { get; } = Database.DB; 

        public ContactRepository() {
            DB.CreateTable<Contact>();
        }

        public IList<Contact> GetContacts() {
            var contacts = (from contact in DB.Table<Contact>() select contact).ToList();

            return contacts;
        }
    }
}
