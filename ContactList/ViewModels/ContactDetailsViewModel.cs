using ContactList.Models;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace ContactList.ViewModels {
    public class ContactDetailsViewModel : ViewModelBase, INotifyPropertyChanged {

        public ContactDetailsViewModel(IContactRepository repository) {
            Repository = repository;
        }

        public ICommand DeleteContactCommand => new RelayCommand(DeleteContact);
        public ICommand UpdateContactCommand => 
            new RelayCommand(() => NavigationService.Navigate(typeof(Views.UpdateContact), contact));

        private Contact contact;
        public Contact Contact {
            get { return contact; }
            private set {
                contact = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contact"));
            }
        }

        public IContactRepository Repository { get; private set; }

        public new event PropertyChangedEventHandler PropertyChanged;

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState) {
            if (NavigationMode.Back == mode)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contact"));
            else
                Contact = parameter as Contact;
            await Task.CompletedTask;
        }

        private void DeleteContact() {
            Repository.DeleteContact(contact);
            NavigationService.GoBack();
        }

    }
}
