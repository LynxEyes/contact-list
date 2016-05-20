using System;
using ContactList.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ContactList.Models;

namespace ContactList.Views {
    public sealed partial class MainPage : Page {
        public MainPage() {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void GoToDetail_Click(object sender, RoutedEventArgs e) {
        }

        private void contactList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var contactSelected = e.AddedItems[0] as Contact;

            var vm = this.DataContext as MainPageViewModel;

            vm.GotoCreateContact.Execute(null);
        }
    }
}
