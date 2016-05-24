using ContactList.Models;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Windows.UI.Xaml.Data;

namespace ContactList.ViewModels {

    //see https://github.com/Windows-XAML/Template10/issues/464#issuecomment-210038007
    [Bindable]
    public class Locator {
        static Locator() {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IContactRepository, ContactRepository>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<CreateContactViewModel>();
            SimpleIoc.Default.Register<UpdateContactViewModel>();
            SimpleIoc.Default.Register<ContactDetailsViewModel>();
            SimpleIoc.Default.Register<IContactValidator, ContactValidator>();
        }

        public MainPageViewModel Main => ServiceLocator.Current.GetInstance<MainPageViewModel>();

        public CreateContactViewModel CreateContact => ServiceLocator.Current.GetInstance<CreateContactViewModel>();

        public UpdateContactViewModel UpdateContact => ServiceLocator.Current.GetInstance<UpdateContactViewModel>();

        public ContactDetailsViewModel ContactDetails => ServiceLocator.Current.GetInstance<ContactDetailsViewModel>();
    }
}
