using Common.Utils;

namespace Common.Models {

    public class ContactValidator : IContactValidator {

        public ContactValidator() {
        }

        public bool IsValid(Contact contact) {
            return IsNameValid(contact);
        }

        private bool IsNameValid(Contact contact) {
            return !contact.Name.IsEmpty();
        }
    }
}