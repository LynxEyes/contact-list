using ContactList.Models;
using System;
using Xunit;
using static ContactList.Services.Database;

namespace ContactListUnitTest.Models {
    public class ContactRepositoryTest : IDisposable {

        private ContactRepository Repository = new ContactRepository();

        public void Dispose() {
            DB.Rollback();
        }

        public ContactRepositoryTest() {
            DB.BeginTransaction();
        }

        [Fact]
        public void GetContactsReturnsAllExistingContacts() {
            //DB.Insert(new Contact("John"));
            //DB.Insert(new Contact("Jack"));
            //DB.Insert(new Contact("Joseph"));

            var contacts = Repository.GetContacts();
            Assert.Equal(3, contacts.Count);
        }
    }
}
