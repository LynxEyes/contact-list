using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Models {
    public class ContactValidator : IContactValidator {
        public ContactValidator() {
        }

        public bool IsValid(Contact contact) {
            return IsNameValid(contact);
        }

        private bool IsNameValid(Contact contact) {
            return !(contact.Name == null) && !(contact.Name.Length == 0);
        }
    }
}
