using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using Xunit;

namespace ContactListUITest.ContactList {
    public class DeleteAllContactsTest : IDisposable {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        protected static IOSDriver<IOSElement> DeleteSession; // Temporary placeholder until Windows namespace exists

        public DeleteAllContactsTest() {
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "cf59c34d-6a44-4b82-9029-ad2fc3cc2611_xnnwpqakf2yqj!App");
            DeleteSession = new IOSDriver<IOSElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Assert.NotNull(DeleteSession);
        }

        public void Dispose() {
            DeleteSession.Dispose();
            DeleteSession = null;
        }
        
        [Fact]
        public void DeleteAllContactsButtonExists() {
            var deleteAllBtn = DeleteSession.FindElementByAccessibilityId("deleteAllContactsBtn");
            Assert.NotNull(deleteAllBtn);
        }

        [Fact]
        public void DeleteAllContactsEmptiesListView() {
            // Given a set of contacts
            CreateContact("John");
            CreateContact("Jack");
            CreateContact("Jill");

            // When I delete all of them
            DeleteSession.FindElementByAccessibilityId("deleteAllContactsBtn").Click();

            // Then the contact list is empty
            var list = DeleteSession.FindElementByAccessibilityId("contactList")
                                    .FindElementsByClassName("ListViewItem");
            Assert.Equal(0, list.Count);
        }

        private void CreateContact(string Name) {
            DeleteSession.FindElementByAccessibilityId("createContactBtn").Click();
            var nameInput = DeleteSession.FindElementByAccessibilityId("nameInput");
            nameInput.Clear();
            nameInput.SendKeys(Name);
            // Just to focus on another element, without this the text wouldn't be TwoWay sync'd
            DeleteSession.FindElementByAccessibilityId("emailInput").Click();
            DeleteSession.FindElementByAccessibilityId("saveBtn").Click();
        }
    }
}
