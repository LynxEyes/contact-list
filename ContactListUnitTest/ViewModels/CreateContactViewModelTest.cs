using ContactList.Models;
using ContactList.ViewModels;
using PCLMock;
using System;
using Xunit;

namespace ContactListUnitTest.ViewModels {
    public class CreateContactViewModelTest : IDisposable {

        private CreateContactViewModel subject;
        private ContactRepositoryMock mockRepository;
        private ContactRepositoryStub stubRepository;

        public CreateContactViewModelTest() {
            //mockRepository = A.Fake<ContactRepository>();
            mockRepository = new ContactRepositoryMock();
            stubRepository = new ContactRepositoryStub();
            subject = new CreateContactViewModel(stubRepository);
        }

        public void Dispose() {
            //throw new NotImplementedException();
        }

        [Fact]
        public void TestFakeItEasy() {
            //var list = A.Fake<IList<int>>();

            //list.Add(1);

            //A.CallTo(() => list.Add(A<int>._)).MustNotHaveHappened();
        }

        [Fact]
        public void SavesNewContact() {
            //mockRepository.Handle<bool>("SaveContact", () => { throw new Exception(); });

            var contact = new Contact("John");

            stubRepository.When(m => m.SaveContact(It.IsAny<Contact>())).Return(false);
            // given that I have set "Peter" on the Name property
            //subject.Name = "Peter";

            // when I save
            //var result = subject.Save();
            stubRepository.SaveContact(contact);

            // Then the repository saved the contact

            stubRepository
                .Verify(x => x.SaveContact(It.IsAny<Contact>()))
                .WasCalledExactlyOnce();

            //Assert.True(result);
        }
    }
}
