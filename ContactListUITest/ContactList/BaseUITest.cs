using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using Xunit;

namespace ContactListUITest.ContactList {
    public class BaseUITest : IDisposable {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        protected static IOSDriver<IOSElement> AppSession; // Temporary placeholder until Windows namespace exists

        public BaseUITest() {
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "cf59c34d-6a44-4b82-9029-ad2fc3cc2611_xnnwpqakf2yqj!App");
            AppSession = new IOSDriver<IOSElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Assert.NotNull(AppSession);
        }

        public void Dispose() {
            AppSession.Dispose();
            AppSession = null;
        }

        protected void CreateContact(string name, string email = null, string mobile = null) {
            NavigateToCreateForm();
            FillInContact(name, email, mobile);
            ClickSaveButton();
        }

        protected void FillInField(string fieldName, string data, string focusField) {
            if (data == null) return;

            var field = AppSession.FindElementByAccessibilityId(fieldName);
            field.Clear();
            if (data.Length > 0) {
                field.SendKeys(data);
            }
            AppSession.FindElementByAccessibilityId(focusField).Click();
        }

        protected void FillInContact(string name, string email = null, string mobile = null) {
            FillInField("nameInput", name, "emailInput");
            FillInField("emailInput", email, "mobileInput");
            FillInField("mobileInput", mobile, "nameInput");
        }

        protected void When_IClickOnTheListItem() {
            AppSession.FindElementByAccessibilityId("contactList")
                      .FindElementByClassName("ListViewItem")
                      .Click();
        }

        protected void NavigateToCreateForm() {
            AppSession.FindElementByAccessibilityId("createContactBtn").Click();
        }

        protected void ClickSaveButton() {
            AppSession.FindElementByAccessibilityId("saveContactBtn").Click();
        }
    }
}
