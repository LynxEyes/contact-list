using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.iOS;
using Xunit;
using System;
using System.Linq;

namespace ContactListUITest.ContactList {

    public class ListTest : IDisposable {

        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        protected static IOSDriver<IOSElement> ListSession; // Temporary placeholder until Windows namespace exists

        public ListTest() {
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "cf59c34d-6a44-4b82-9029-ad2fc3cc2611_xnnwpqakf2yqj!App");
            ListSession = new IOSDriver<IOSElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Assert.NotNull(ListSession);
        }

        public void Dispose() {
            ListSession.Dispose();
            ListSession = null;
        }

        [Fact]
        public void TitleLabelExists() {
            var TitleLabel = ListSession.FindElementByAccessibilityId("title");
            Assert.NotNull(TitleLabel);
        }

        [Fact]
        public void ListExistsAndContainsElements() {
            // Given there are 3 contacts named Ze, Carlos, Alberto
            CreateContact("Rafael");
            CreateContact("Carlos");
            CreateContact("Alberto");

            // when I view the list of contacts
            var list = ListSession.FindElementByAccessibilityId("contactList");
            Assert.NotNull(list);

            // Then I can see it has 3 names: Rafael, Carlos and Alberto
            var items = list.FindElementsByClassName("ListViewItem");
            Assert.Equal(3, items.Count);

            var names = list.FindElementsByClassName("TextBlock").Select(x => x.Text).ToList();
            Assert.Contains("Rafael", names);
            Assert.Contains("Carlos", names);
            Assert.Contains("Alberto", names);
        }

        private void CreateContact(string Name) {
            ListSession.FindElementByAccessibilityId("createContactBtn").Click();
            var nameInput = ListSession.FindElementByAccessibilityId("nameInput");
            nameInput.Clear();
            nameInput.SendKeys(Name);
            // Just to focus on another element, without this the text wouldn't be TwoWay sync'd
            ListSession.FindElementByAccessibilityId("emailInput").Click();
            ListSession.FindElementByAccessibilityId("saveBtn").Click();
        }
    }
}
