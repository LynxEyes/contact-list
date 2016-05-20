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
            NavigateToCreateForm();
            
            var createContactTitle = CreateSession.FindElementByAccessibilityId("createContactTitle");
            Assert.NotNull(createContactTitle);
        }

        [Fact]
        public void CreateContactPageHasRightElements() {
            NavigateToCreateForm();

            var nameInput = CreateSession.FindElementByAccessibilityId("nameInput");
            var mobileInput = CreateSession.FindElementByAccessibilityId("mobileInput");
            var emailInput = CreateSession.FindElementByAccessibilityId("emailInput");

            Assert.NotNull(nameInput);
            Assert.NotNull(mobileInput);
            Assert.NotNull(emailInput);
        }

        [Fact]
        public void SaveContactGetsBackToMainPage() {
            NavigateToCreateForm();

            FillInContact("Jack", "jack@acme.com", "1234567890");

            ClickSaveButton();

            //check that it returned to main page
            var TitleLabel = CreateSession.FindElementByAccessibilityId("title");
            Assert.NotNull(TitleLabel);
        }

        [Fact]
        public void SaveContactUpdatesList() {
            var listSize = CreateSession.FindElementByAccessibilityId("contactList")
                                        .FindElementsByClassName("Image").Count;
            NavigateToCreateForm();
            FillInField("nameInput", "Jack", "emailInput");
            ClickSaveButton();
            
            var newListSize = CreateSession.FindElementByAccessibilityId("contactList")
                                           .FindElementsByClassName("Image").Count;

            //check that list has one new element
            Assert.Equal(listSize + 1, newListSize);
        }

        [Fact]
        public void UnableToSaveContactIfMissingName() {
            NavigateToCreateForm();

            FillInContact(null, "jack@acme.com", "1234567890");

            ClickSaveButton();

            var errorMessage = CreateSession.FindElementByAccessibilityId("errorMessage");
            Assert.NotEmpty(errorMessage.Text);
        }

        private void NavigateToCreateForm() {
            CreateSession.FindElementByAccessibilityId("createContactBtn").Click();
        }

        private void ClickSaveButton() {
            CreateSession.FindElementByAccessibilityId("saveBtn").Click();
        }

        private void FillInContact(string name, string email, string mobile) {
            FillInField("nameInput", name, "emailInput");
            FillInField("emailInput", email, "mobileInput");
            FillInField("mobileInput", mobile, "nameInput");
        }

        private void FillInField(string fieldName, string data, string focusField) {
            if (data == null) return;

            var field = CreateSession.FindElementByAccessibilityId(fieldName);
            field.Clear();
            field.SendKeys(data);
            CreateSession.FindElementByAccessibilityId(focusField).Click();
        }
    }
}
