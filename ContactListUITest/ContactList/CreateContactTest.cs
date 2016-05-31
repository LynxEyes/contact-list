using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using Xunit;

namespace ContactListUITest.ContactList {

    public class CreateContactTest : BaseUITest {

        [Fact]
        public void MainPageHasCreateContactButton() {
            var createContactBtn = AppSession.FindElementByAccessibilityId("createContactBtn");
            Assert.NotNull(createContactBtn);
        }

        [Fact]
        public void CreateContactButtonNavigatesToCreateContactPage() {
            NavigateToCreateForm();

            var createContactTitle = AppSession.FindElementByAccessibilityId("contactFormTitle");
            Assert.NotNull(createContactTitle);
        }

        [Fact]
        public void CreateContactPageHasRightElements() {
            NavigateToCreateForm();

            var nameInput = AppSession.FindElementByAccessibilityId("nameInput");
            var mobileInput = AppSession.FindElementByAccessibilityId("mobileInput");
            var emailInput = AppSession.FindElementByAccessibilityId("emailInput");

            Assert.NotNull(nameInput);
            Assert.NotNull(mobileInput);
            Assert.NotNull(emailInput);
        }

        [Fact]
        public void FailsToCreateDuplicateContact() {
            NavigateToCreateForm();

            //given I have a contact named Jack
            FillInContact("Jack", "jack@acme.com", "1234567890");
            ClickSaveButton();

            //when I try to create another contact with the same name
            NavigateToCreateForm();
            FillInContact("Jack", "jack@acme.com", "1234567890");
            ClickSaveButton();

            //I get an error msg
            var errorMessage = AppSession.FindElementByAccessibilityId("errorMessage");
            Assert.NotEmpty(errorMessage.Text);
        }

        [Fact]
        public void SaveContactGetsBackToMainPage() {
            NavigateToCreateForm();

            FillInContact("Jack", "jack@acme.com", "1234567890");

            ClickSaveButton();

            //check that it returned to main page
            var TitleLabel = AppSession.FindElementByAccessibilityId("title");
            Assert.NotNull(TitleLabel);
        }

        [Fact]
        public void SaveContactUpdatesList() {
            var listSize = AppSession.FindElementByAccessibilityId("contactList")
                                        .FindElementsByClassName("Image").Count;
            NavigateToCreateForm();
            FillInField("nameInput", "Jack", "emailInput");
            ClickSaveButton();

            var newListSize = AppSession.FindElementByAccessibilityId("contactList")
                                        .FindElementsByClassName("Image").Count;

            //check that list has one new element
            Assert.Equal(listSize + 1, newListSize);
        }

        [Fact]
        public void UnableToSaveContactIfMissingName() {
            NavigateToCreateForm();

            FillInContact(null, "jack@acme.com", "1234567890");

            ClickSaveButton();

            var errorMessage = AppSession.FindElementByAccessibilityId("errorMessage");
            Assert.NotEmpty(errorMessage.Text);
        }
    }
}