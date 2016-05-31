using ContactList.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using static ContactList.Services.Database;

namespace ContactListUnitTest.Models {

    public class ContactRepositoryTest : IDisposable {
        private ContactRepository subject;
        private ContactValidatorMock validator;

        public void Dispose() {
            DB.Rollback();
        }

        public ContactRepositoryTest() {
            DB.BeginTransaction();
            validator = new ContactValidatorMock();
            subject = new ContactRepository(validator);
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
            validator.AddHandlers()
                          .IsValid(Contact => true);
            subject.SaveContact(contact);

            // then it is stored on the database
            var stored = DB.Table<Contact>().Where(x => x.Name == "Joao das Neves").First();

            Assert.NotNull(stored);
        }

        [Fact]
        public void FailsToSaveContactWithoutName() {
            var contact = new Contact(null);
            Assert.Equal(CreateCodesEnum.INVALID_DATA_ERROR, subject.SaveContact(contact));
        }

        [Fact]
        public void FailsToSaveContactWithEmptyName() {
            var contact = new Contact("");
            Assert.Equal(CreateCodesEnum.INVALID_DATA_ERROR, subject.SaveContact(contact));
        }

        [Fact]
        public void DeletesExistingContact() {
            //given an existing contact
            var contact = new Contact("Joao das Neves");
            validator.AddHandlers()
                          .IsValid(Contact => true);

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

        [Fact]
        public void UpdatesAnExistingContact() {
            // given a new contact
            var contact = new Contact("Joao das Neves");
            DB.Insert(contact);

            // when I update it
            validator.AddHandlers()
                          .IsValid(Contact => true);
            contact.Name = "No One";
            contact.Email = "noone@nowhere.com";
            subject.UpdateContact(contact);

            // then the changes are persisted
            var changedContact = DB.Table<Contact>().Where(x => x.Name == "No One").First();
            Assert.Equal(contact.Id, changedContact.Id);
            Assert.Equal(contact.Name, changedContact.Name);
            Assert.Equal(contact.Email, changedContact.Email);
        }

        [Fact]
        public void FailsToUpdateANonExistingContact() {
            // Given a non existing contact
            var contact = new Contact("Joao das Neves");

            // When I try to update it
            var result = subject.UpdateContact(contact);

            // The operation fails
            Assert.False(result);
        }

        [Fact]
        public void FailsToUpdateAContactWithNullName() {
            var contact = new Contact("Joao das Neves");
            DB.Insert(contact);

            contact.Name = null;
            Assert.False(subject.UpdateContact(contact));
        }

        [Fact]
        public void FailsToUpdateAContactWithEmptyName() {
            var contact = new Contact("Joao das Neves");
            DB.Insert(contact);

            contact.Name = "";
            Assert.False(subject.UpdateContact(contact));
        }

        // -------------------------------------------------------------------------------

        [Fact]
        public void SearchesContactsInCaseInsensitiveManner() {
            var contact = new Contact("Joao das Neves");
            DB.Insert(contact);

            var list = subject.GetContacts("joao das neves");

            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void SearchesContactsByMatchingWithAnyPartOfTheContactName() {
            var contact = new Contact("Joao das Neves");
            DB.Insert(contact);

            var list = subject.GetContacts("nev");

            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void SearchingByNullReturnsAllContacts() {
            DB.Insert(new Contact("Joao das Neves"));
            DB.Insert(new Contact("Carlos das Montanhas"));

            var list = subject.GetContacts(null);

            Assert.Equal(2, list.Count);
        }
    }
}