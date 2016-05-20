using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.iOS;
using Xunit;
using System;
using System.Linq;

namespace ContactListUITest.ContactList {

    public class ContactDetailsTest : IDisposable {

        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        protected static IOSDriver<IOSElement> DetailsSession; // Temporary placeholder until Windows namespace exists
        private string Name = "Rafael";
        private string Email = "rafael@acme.com";
        private string Mobile = "1234567890";

        public ContactDetailsTest() {
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "cf59c34d-6a44-4b82-9029-ad2fc3cc2611_xnnwpqakf2yqj!App");
            DetailsSession = new IOSDriver<IOSElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Assert.NotNull(DetailsSession);
        }

        public void Dispose() {
            DetailsSession.Dispose();
            DetailsSession = null;
        }

        [Fact]
        public void ListSelectionChangeNavigatesToDetailPage() {
            //given there is a contact in the list
            CreateContact(Name, Email, Mobile);

            //when I click the contact on the list
            When_IClickOnTheListItem();

            //then there is a navigation to the detail page
            var showContactTitle = DetailsSession.FindElementByAccessibilityId("showContactTitle");
            Assert.NotNull(showContactTitle);
        }

        [Fact]
        public void DisplaysAllContactDetails() {
            // Given there is a contact
            CreateContact(Name, Email, Mobile);

            // when I click on its item on the contact list
            When_IClickOnTheListItem();

            // Then I can see all the contact details in its own page
            var labels = DetailsSession.FindElementsByClassName("TextBlock").Select(x => x.Text).ToList();
            Assert.Contains(Name, labels);
            Assert.Contains(Email, labels);
            Assert.Contains(Mobile, labels);
        }

        private static void When_IClickOnTheListItem() {
            DetailsSession.FindElementByAccessibilityId("contactList")
                          .FindElementByClassName("ListViewItem")
                          .Click();
        }

        private void CreateContact(string name, string email, string mobile) {
            DetailsSession.FindElementByAccessibilityId("createContactBtn").Click();
            FillInField("nameInput", name, "emailInput");
            FillInField("emailInput", email, "mobileInput");
            FillInField("mobileInput", mobile, "nameInput");
            DetailsSession.FindElementByAccessibilityId("saveBtn").Click();
        }

        private void FillInField(string fieldName, string data, string focusField) {
            if (data == null) return;

            var field = DetailsSession.FindElementByAccessibilityId(fieldName);
            field.Clear();
            field.SendKeys(data);
            DetailsSession.FindElementByAccessibilityId(focusField).Click();
        }
    }
}
