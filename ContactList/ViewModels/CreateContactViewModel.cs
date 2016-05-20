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
    public class CreateContactViewModel : ViewModelBase, INotifyPropertyChanged {
        private IContactRepository Repository { get; set; }

        public ICommand CreateContactCommand { get; private set; }

        public CreateContactViewModel(IContactRepository repository) {
            Repository = repository;
            CreateContactCommand = new RelayCommand(SaveContact);
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        private string errorMessage;
        public string ErrorMessage {
            get { return errorMessage; }
            set {
                errorMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ErrorMessage"));
            }
        }
        public new event PropertyChangedEventHandler PropertyChanged;

        public bool Save() {
            var contact = new Contact(Name, Email, Mobile);
            return Repository.SaveContact(contact);
        }

        public void SaveContact() {
            if (Save())
                NavigationService.GoBack();
            else
                ErrorMessage = "Error: Please fill in the Name";
        }
    }

}
