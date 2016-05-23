using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Template10.Common;
using Windows.UI.Xaml.Data;

namespace ContactList {
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : BootStrapper {
        public App() {
            InitializeComponent();
        }

        public override async Task OnInitializeAsync(IActivatedEventArgs args) {
            await Task.CompletedTask;
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args) {
            // long-running startup tasks go here

            NavigationService.Navigate(typeof(Views.MainPage));
            await Task.CompletedTask;
        }
    }
}