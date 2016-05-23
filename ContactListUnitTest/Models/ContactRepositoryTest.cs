using ContactList.Models;
using System;
using System.Diagnostics;
using System.Linq;
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

        [Fact]
        public void DeletesExistingContact() {
            //given an existing contact
            var contact = new Contact("Joao das Neves");
            subject.SaveContact(contact);

            //when I delete it
            var deleted = subject.DeleteContact(contact);

            //then is no longer there
            Assert.True(deleted);

            var stored = DB.Table<Contact>().Where(x => x.Name == "Joao das Neves").ToList();
            Assert.Empty(stored);
        }

        [Fact]
        public void FailsToDeleteUnexistingContact() {
            //given an existing contact
            var contact = new Contact("Joao das Neves");
            //subject.SaveContact(contact);

            //when I delete it
            var deleted = subject.DeleteContact(contact);
            Assert.False(deleted);
        }

        [Fact]
        public void DeletesAllContacts() {
            //given some existing contact
            var contact1 = new Contact("Joao das Neves");
            var contact2 = new Contact("Jonny of the Snows");
            subject.SaveContact(contact1);
            subject.SaveContact(contact2);

            // When I delete all contacts
            subject.DeleteAll();

            // Then we're left with an empty table of contacts
            var table = DB.Table<Contact>().ToList();
            Assert.Empty(table);
        }
    }
}
