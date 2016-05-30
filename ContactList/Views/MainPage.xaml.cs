using ContactList.Models;
using ContactList.ViewModels;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace ContactList.Views {

    public sealed partial class MainPage : Page {

        public MainPage() {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void contactList_ItemClick(object sender, ItemClickEventArgs e) {
            var contactSelected = e.ClickedItem as Contact;

            var vm = this.DataContext as MainPageViewModel;
            vm.GotoContactDetails(contactSelected);
        }

        private bool UserFilter(object item) {
            if (string.IsNullOrEmpty(searchTextBox.Text))
                return true;
            else
                return ((item as Contact).Name.IndexOf(searchTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void searchTextBox_KeyUp(object sender, KeyRoutedEventArgs e) {
            if (e.Key == Windows.System.VirtualKey.Enter) {
                var context = DataContext as MainPageViewModel;
                context.SearchText = searchTextBox.Text;
                context.SearchContactsCommand.Execute(sender);
            }
        }
    }
}