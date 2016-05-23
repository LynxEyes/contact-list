using ContactList.Models;
using ContactList.ViewModels;
using Xunit;

namespace ContactListUnitTest.ViewModels {
    public class MainPageViewModelTest {
        private MainPageViewModel subject;
        private ContactRepositoryMock mockRepository;

        public MainPageViewModelTest() {
            mockRepository = new ContactRepositoryMock();
            subject = new MainPageViewModel(mockRepository);
        }

        [Fact]
        public void DeleteAllContactsRelaysToRepository() {
            // given that Repository.DeleteAll returns true
            mockRepository.AddHandlers()
                          .DeleteAll(() => true);

            // when I delete all contacts
            subject.DeleteAllContacts();

            // the repository is invoked once
            mockRepository.HasBeenCalled().Once().DeleteAll();
        }
    }
}