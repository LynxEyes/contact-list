using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.iOS;
using Xunit;
using System;

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
            // when I view the list of contacts
            // Then I can see Ze, Carlos and Alberto

            var list = ListSession.FindElementByAccessibilityId("contactList");
            Assert.NotNull(list);

            var items = list.FindElementsByClassName("ListViewItem");

            Assert.Equal(3, items.Count);

            Assert.Equal("Ze", items[0].FindElementByClassName("TextBlock").Text);
            Assert.Equal("Carlos", items[1].FindElementByClassName("TextBlock").Text);
            Assert.Equal("Alberto", items[2].FindElementByClassName("TextBlock").Text);
        }
    }
}
