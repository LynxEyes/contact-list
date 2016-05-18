using ContactList.Models;

namespace ContactList.ViewModels {
    public class CreateContactViewModel {
        private IContactRepository Repository { get; set; }

        public CreateContactViewModel(IContactRepository repository) {
            Repository = repository;
        }

        public string Name { get; set; } = "Joao";
        public string Email { get; set; }
        public string Mobile { get; set; }

        public bool Save() {
            var contact = new Contact(Name, Email, Mobile);
            return Repository.SaveContact(contact);
        }
    }
}
