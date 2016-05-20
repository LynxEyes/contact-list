using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactListUITest.ContactList {
    public class DeleteContactTest : IDisposable {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        protected static IOSDriver<IOSElement> DeleteSession; // Temporary placeholder until Windows namespace exists
        private string Name = "Rafael";
        private string Email = "rafael@acme.com";
        private string Mobile = "1234567890";

        public DeleteContactTest() {
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
        public void DeleteContactButtonNavigatesToMainPage() {
            // Given there is a contact
            CreateContact(Name, Email, Mobile);

            // when I click on its item on the contact list
            When_IClickOnTheListItem();

            //and click on the delete button on the details page
            DeleteSession.FindElementByAccessibilityId("deleteContactBtn").Click();

            //then I am navigated to main page
            var TitleLabel = DeleteSession.FindElementByAccessibilityId("title");
            Assert.NotNull(TitleLabel);
        }

        [Fact]
        public void DeletedContactIsRemovedFromList() {
            // Given there is a contact
            CreateContact(Name, Email, Mobile);

            // when I click on its item on the contact list
            When_IClickOnTheListItem();

            //and click on the delete button on the details page
            DeleteSession.FindElementByAccessibilityId("deleteContactBtn").Click();

            //the contact gets removed from the main page list
            var list = DeleteSession.FindElementByAccessibilityId("contactList");
            var names = list.FindElementsByClassName("TextBlock").Where(x => x.Text == Name).ToList();
            Assert.Empty(names);
        }

        private void CreateContact(string name, string email, string mobile) {
            DeleteSession.FindElementByAccessibilityId("createContactBtn").Click();
            FillInField("nameInput", name, "emailInput");
            FillInField("emailInput", email, "mobileInput");
            FillInField("mobileInput", mobile, "nameInput");
            DeleteSession.FindElementByAccessibilityId("saveBtn").Click();
        }

        private void FillInField(string fieldName, string data, string focusField) {
            if (data == null) return;

            var field = DeleteSession.FindElementByAccessibilityId(fieldName);
            field.Clear();
            field.SendKeys(data);
            DeleteSession.FindElementByAccessibilityId(focusField).Click();
        }

        private static void When_IClickOnTheListItem() {
            DeleteSession.FindElementByAccessibilityId("contactList")
                          .FindElementByClassName("ListViewItem")
                          .Click();
        }
    }
}
