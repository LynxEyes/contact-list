using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Models {
    public interface IContactRepository {
        IList<Contact> GetContacts();
        bool SaveContact(Contact contact);
    }
}
