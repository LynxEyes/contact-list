using ContactList.Models;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace ContactList.ViewModels {
    public class Locator {
        static Locator() {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IContactRepository, ContactRepository>();
            SimpleIoc.Default.Register<MainPageViewModel>();
        }

        public MainPageViewModel Main => ServiceLocator.Current.GetInstance<MainPageViewModel>();
    }
}
