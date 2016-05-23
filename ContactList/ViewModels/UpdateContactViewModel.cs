using ContactList.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace ContactList.ViewModels {
    public class UpdateContactViewModel : ContactFormViewModel {

        public UpdateContactViewModel(IContactRepository repository) {
            Repository = repository;
            ContactFormTitle = "Update Contact";
        }

        private IContactRepository Repository { get; set; }

        public override void SaveContact() {
            // TODO: actually save the Contact...
            //       Navigate Back if successfull, set error message otherwise
            return; 
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState) {
            Contact = parameter as Contact;
            await Task.CompletedTask;
        }
    }
}
