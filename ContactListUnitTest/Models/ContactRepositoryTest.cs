using ContactList.Models;
using System;
using Xunit;
using static ContactList.Services.Database;

namespace ContactListUnitTest.Models {
    public class ContactRepositoryTest : IDisposable {

        private ContactRepository subject = new ContactRepository();

        public void Dispose() {
            DB.Rollback();
        }

        public ContactRepositoryTest() {
            DB.BeginTransaction();
        }

        [Fact]
        public void GetContactsReturnsAllExistingContacts() {
            DB.Insert(new Contact("John"));
            DB.Insert(new Contact("Jack"));
            DB.Insert(new Contact("Joseph"));

            var contacts = subject.GetContacts();
            Assert.Equal(3, contacts.Count);
        }

        [Fact]
        public void SavesANewContact() {
            // given a new contact
            var contact = new Contact("Joao das Neves");

            // when I save it
            subject.SaveContact(contact);

            // then it is stored on the database
            var stored = DB.Table<Contact>().Where(x => x.Name == "Joao das Neves").First(); 

            Assert.NotNull(stored);
        }

        [Fact]
        public void FailsToSaveContactWithoutName() {
            var contact = new Contact(null);

            // when I save it
            var result = subject.SaveContact(contact);

            Assert.False(result);
        }

    }
}
