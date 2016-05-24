using ContactList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace ContactList.ViewModels {
    public class UpdateContactViewModel : ContactFormViewModel {

        public UpdateContactViewModel(IContactRepository repository) {
            Repository = repository;
            ContactFormTitle = "Update Contact";
        }

        private IContactRepository Repository { get; set; }

        public override void SaveContact() {
            if (Repository.UpdateContact(Contact))
                NavigationService.GoBack();
            else
                ErrorMessage = "Error: Please fill in the Name";
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState) {
            Contact = parameter as Contact;
            await Task.CompletedTask;
        }
    }
}
