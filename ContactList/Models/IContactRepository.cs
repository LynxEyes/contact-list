using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Models {
    public interface IContactRepository {
        ObservableCollection<Contact> Contacts { get; }

        IList<Contact> GetContacts();
        bool SaveContact(Contact contact);
    }
}
