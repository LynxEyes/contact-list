using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static ContactList.Services.Database;

namespace ContactList.Models {
    public class ContactRepository : IContactRepository {

        public ObservableCollection<Contact> Contacts { get; private set; }

        public ContactRepository() {
            DB.DropTable<Contact>();
            DB.CreateTable<Contact>();

            Contacts = new ObservableCollection<Contact>(GetContacts());
        }

        public IList<Contact> GetContacts() {
            return (from contact in DB.Table<Contact>()
                    select contact)
                    .OrderBy(c => c.Name, new CaseInsensitiveComparer())
                    .ToList();
        }

        private class CaseInsensitiveComparer : IComparer<string> {
            public int Compare(string x, string y) {
                return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
            }
        }

        public bool SaveContact(Contact contact) {

            try {
                DB.Insert(contact);
            } catch (SQLite.NotNullConstraintViolationException) {
                return false;
            }

            // TODO: is there a better way?
            Contacts.Clear();
            GetContacts().ToList().ForEach(Contacts.Add);
            return true;
        }
    }
}
