using ContactList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace ContactList.ViewModels {

    public class CreateContactViewModel : ContactFormViewModel {
        public CreateContactViewModel(IContactRepository repository) {
            Repository = repository;
            ContactFormTitle = "Create Contact";
        }

        private IContactRepository Repository { get; set; }

        public bool Save() {
            return Repository.SaveContact(Contact);
        }

        public override void SaveContact() {
            if (Save())
                NavigationService.GoBack();
            else
                ErrorMessage = "Error: Please fill in the Name";
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState) {
            Contact = new Contact();
            await Task.CompletedTask;
        }
    }
}
