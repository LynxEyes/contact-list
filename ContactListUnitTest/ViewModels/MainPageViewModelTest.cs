using ContactList.Models;
using ContactList.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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