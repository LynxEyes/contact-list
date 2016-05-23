using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using ContactList.Models;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System;

namespace ContactList.ViewModels {

    public class MainPageViewModel : ViewModelBase, INotifyPropertyChanged {
        private IContactRepository Repository;
        public new event PropertyChangedEventHandler PropertyChanged;

        public IList<Contact> Contacts => Repository.GetContacts();

        public MainPageViewModel(IContactRepository repository) {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) {
                Value = "Designtime value";
            }

            Repository = repository;
        }

        string _Value = "Gas";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public void DeleteAllContacts() {
            Repository.DeleteAll();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contacts"));
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState) {
            if (suspensionState.Any()) {
                Value = suspensionState[nameof(Value)]?.ToString();
            }

            if (mode == NavigationMode.Back || mode == NavigationMode.Refresh)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contacts"));

            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending) {
            if (suspending) {
                suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args) {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void GotoDetailsPage() => NavigationService.Navigate(typeof(Views.DetailPage), Value);
        public void GotoSettings()    => NavigationService.Navigate(typeof(Views.SettingsPage), 0);
        public void GotoPrivacy()     => NavigationService.Navigate(typeof(Views.SettingsPage), 1);
        public void GotoAbout()       => NavigationService.Navigate(typeof(Views.SettingsPage), 2);
        public void GotoContactDetails(Contact contact) => 
            NavigationService.Navigate(typeof(Views.ContactDetails), contact);

        public RelayCommand GotoCreateContact => new RelayCommand(() => NavigationService.Navigate(typeof(Views.CreateContact)));
        public RelayCommand DeleteAllContactsCommand => new RelayCommand(DeleteAllContacts);
    }
}

