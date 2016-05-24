using System;
using ContactList.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ContactList.Models;
using Windows.UI.Xaml.Data;

namespace ContactList.Views {
    public sealed partial class MainPage : Page {
        public MainPage() {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void GoToDetail_Click(object sender, RoutedEventArgs e) {
        }

        private void contactList_ItemClick(object sender, ItemClickEventArgs e) {
            var contactSelected = e.ClickedItem as Contact;

            var vm = this.DataContext as MainPageViewModel;
            vm.GotoContactDetails(contactSelected);
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e) {
            //CollectionViewSource xpto = new CollectionViewSource(); //.GetDefaultView(contactList.ItemsSource);
            //xpto.Source = contactList.ItemsSource;
            //ICollectionView view = xpto.View;
        }

        private bool UserFilter(object item) {
            if (string.IsNullOrEmpty(searchTextBox.Text))
                return true;
            else
                return ((item as Contact).Name.IndexOf(searchTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}
