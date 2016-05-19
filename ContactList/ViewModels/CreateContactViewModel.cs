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

namespace ContactList.ViewModels {
    public class CreateContactViewModel : ViewModelBase {
        private IContactRepository Repository { get; set; }

        public ICommand CreateCommand { get; private set; }

        public CreateContactViewModel(IContactRepository repository) {
            Repository = repository;
            CreateCommand = new RelayCommand(saveMe);
        }

        public string Name { get; set; } = "Joao";
        public string Email { get; set; }
        public string Mobile { get; set; }

        public bool Save() {
            var contact = new Contact(Name, Email, Mobile);
            return Repository.SaveContact(contact);
        }

        public void saveMe() {
            this.Save();
            NavigationService.Navigate(typeof(Views.MainPage));
        }

        public RelayCommand SaveContact => new RelayCommand(
            () => {
                var result = Save();
                NavigationService.Navigate(typeof(Views.MainPage));
            }
        );
    }
}
