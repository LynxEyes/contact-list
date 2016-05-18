using System;
using System.Collections.Generic;
using System.Linq;
using static ContactList.Services.Database;

namespace ContactList.Models {
    public class ContactRepository : IContactRepository {

        //private SQLiteConnection DB { get; } = Database.DB; 

        public ContactRepository() {
            DB.DropTable<Contact>();
            DB.CreateTable<Contact>();

            DB.InsertOrReplace(new Contact("Rafael"));
            DB.InsertOrReplace(new Contact("Carlos"));
            DB.InsertOrReplace(new Contact("Alberto"));
        }

        public IList<Contact> GetContacts() {
            return (from contact in DB.Table<Contact>() select contact).ToList();
        }

        public bool SaveContact(Contact contact) {
            throw new NotImplementedException();
        }
    }
}
