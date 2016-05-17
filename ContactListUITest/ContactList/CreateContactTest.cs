using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using Xunit;

namespace ContactListUITest.ContactList {
    public class CreateContactTest : IDisposable {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        protected static IOSDriver<IOSElement> CreateSession; // Temporary placeholder until Windows namespace exists

        public CreateContactTest() {
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "cf59c34d-6a44-4b82-9029-ad2fc3cc2611_xnnwpqakf2yqj!App");
            CreateSession = new IOSDriver<IOSElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Assert.NotNull(CreateSession);
        }

        public void Dispose() {
            CreateSession.Dispose();
            CreateSession = null;
        }

        [Fact]
        public void MainPageHasCreateContactButton() {
            var createContactBtn = CreateSession.FindElementByAccessibilityId("createContactBtn");
            Assert.NotNull(createContactBtn);
        }

        [Fact]
        public void CreateContactButtonNavigatesToCreateContactPage() {
            var createContactBtn = CreateSession.FindElementByAccessibilityId("createContactBtn");
            createContactBtn.Click();

            var createContactTitle = CreateSession.FindElementByAccessibilityId("createContactTitle");
            Assert.NotNull(createContactTitle);
        }

        [Fact]
        public void ContactPageHasRightElements() {
            var createContactBtn = CreateSession.FindElementByAccessibilityId("createContactBtn");
            createContactBtn.Click();

            var nameInput = CreateSession.FindElementByAccessibilityId("nameInput");
            var mobileInput = CreateSession.FindElementByAccessibilityId("mobileInput");
            var emailInput = CreateSession.FindElementByAccessibilityId("emailInput");

            Assert.NotNull(nameInput);
            Assert.NotNull(mobileInput);
            Assert.NotNull(emailInput);
        }
    }
}
