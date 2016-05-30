using ContactList.Models;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace ContactList.ViewModels {

    public class MainPageViewModel : ViewModelBase, INotifyPropertyChanged {

        public MainPageViewModel(IContactRepository repository) {
            Repository = repository;
        }

        private IContactRepository Repository;

        public new event PropertyChangedEventHandler PropertyChanged;

        public string SearchText { get; set; }

        public IList<Contact> Contacts => Repository.GetContacts(SearchText);

        public void DeleteAllContacts() {
            Repository.DeleteAll();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contacts"));
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState) {
            //if (mode == NavigationMode.Back || mode == NavigationMode.Refresh)
            //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contacts"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contacts"));
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending) {
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args) {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void GotoContactDetails(Contact contact) =>
            NavigationService.Navigate(typeof(Views.ContactDetails), contact);

        public RelayCommand GotoCreateContact => new RelayCommand(() => NavigationService.Navigate(typeof(Views.CreateContact)));
        public RelayCommand DeleteAllContactsCommand => new RelayCommand(DeleteAllContacts);

        public RelayCommand SearchContactsCommand =>
            new RelayCommand(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contacts")));
    }
}