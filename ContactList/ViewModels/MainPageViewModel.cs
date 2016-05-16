using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using ContactList.Models;
using System.Collections.ObjectModel;

namespace ContactList.ViewModels {

    public class MainPageViewModel : ViewModelBase {
        private IContactRepository Repository;

        public IList<Contact> Contacts => Repository.GetContacts(); //new ObservableCollection<Contact>();

        public MainPageViewModel(IContactRepository repository) {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) {
                Value = "Designtime value";
            }

            Repository = repository;

            //Repository.GetContacts().ToList().ForEach(Contacts.Add);
        }

        string _Value = "Gas";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState) {
            if (suspensionState.Any()) {
                Value = suspensionState[nameof(Value)]?.ToString();
            }
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

    }
}

