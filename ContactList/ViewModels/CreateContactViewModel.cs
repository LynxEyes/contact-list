using ContactList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace ContactList.ViewModels {

    public class CreateContactViewModel : ContactFormViewModel {

        public CreateContactViewModel(IContactRepository repository) {
            Repository = repository;
            ContactFormTitle = "Create Contact";
        }

        private IContactRepository Repository { get; set; }

        public CreateCodesEnum Save() {
            return Repository.SaveContact(Contact);
        }

        public override void SaveContact() {
            var result = Save();

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            switch (result) {
                case CreateCodesEnum.OK:
                    NavigationService.GoBack();
                    break;

                case CreateCodesEnum.INVALID_DATA_ERROR:
                    ErrorMessage = loader.GetString("ContactFormInvalidData");
                    break;

                case CreateCodesEnum.NULL_NAME_ERROR:
                    ErrorMessage = loader.GetString("ContactFormNullName");
                    break;

                case CreateCodesEnum.DUPLICATE_NAME_ERROR:
                    ErrorMessage = loader.GetString("ContactFormDuplicateName");
                    break;

                default:
                    break;
            }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState) {
            Contact = new Contact();
            await Task.CompletedTask;
        }
    }
}