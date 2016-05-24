using ContactList.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.ComponentModel;

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
