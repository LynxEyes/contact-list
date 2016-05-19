using ContactList.Models;
using ContactList.ViewModels;
using PCLMock;
using System;
using Xunit;

namespace ContactListUnitTest.ViewModels {
    public class CreateContactViewModelTest : IDisposable {

        private CreateContactViewModel subject;
        private ContactRepositoryMock mockRepository;

        public CreateContactViewModelTest() {
            mockRepository = new ContactRepositoryMock();
            subject = new CreateContactViewModel(mockRepository);
        }

        public void Dispose() {
            //throw new NotImplementedException();
        }

        [Fact]
        public void SavesNewContact() {
            // given that I have set "whatever" on the Name property
            subject.Name = "whatever";

            // when I save
            subject.Save();

            // Then the repository saved the contact exactly once
            mockRepository.HasBeenCalled().Once().SaveContact();
            var invokation = mockRepository.GetCalls().First().SaveContact();
            Assert.Equal("SaveContact", invokation.Name);
        }

        [Fact]
        public void SavesNewContactReturnsTrue() {
            //given that SaveContact returns true
            var expected = true;
            mockRepository
                .AddHandlers()
                    .SaveContact(Contact => expected);

            // when I save
            var actual = subject.Save();

            // the repository (and hence the ViewModel) return true
            Assert.Equal(expected, actual);
        }

        //[Fact]
        //public void SavesNewContact() {
        //    //mockRepository.Handle<bool>("SaveContact", () => { throw new Exception(); });

        //    var contact = new Contact("John");

        //    stubRepository.When(m => m.SaveContact(It.IsAny<Contact>())).Return(false);
        //    // given that I have set "Peter" on the Name property
        //    //subject.Name = "Peter";

        //    // when I save
        //    //var result = subject.Save();
        //    stubRepository.SaveContact(contact);

        //    // Then the repository saved the contact

        //    stubRepository
        //        .Verify(x => x.SaveContact(It.IsAny<Contact>()))
        //        .WasCalledExactlyOnce();

        //    //Assert.True(result);
        //}
    }
}
