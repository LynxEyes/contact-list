using ContactList.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static ContactList.Services.Database;

namespace ContactList.Models {
    public class ContactRepository : IContactRepository {

        public ContactRepository() {
            DB.DropTable<Contact>();
            DB.CreateTable<Contact>();
        }

        public IList<Contact> GetContacts() {
            return (from contact in DB.Table<Contact>()
                    select contact)
                    .OrderBy(c => c.Name, new CaseInsensitiveComparer())
                    .ToList();
        }

        public bool SaveContact(Contact contact) {
            try {
                DB.Insert(contact);
            } catch (SQLite.NotNullConstraintViolationException) {
                return false;
            }

            return true;
        }

        public bool DeleteContact(Contact contact) {
            return DB.Delete(contact) == 1;
        }

        public bool DeleteAll() {
            DB.DeleteAll<Contact>();
            return true;
        }
    }
}
