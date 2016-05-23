using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactListUITest.ContactList {
    public class UpdateContactTest : BaseUITest {

        [Fact]
        public void UpdateContactButtonIsPresent() {
            //given I have a contact
            CreateContact("Jill");

            //when I click the contact on the list
            When_IClickOnTheListItem();

            //then, there is an update btn in the contact details view page
            var button = AppSession.FindElementByAccessibilityId("updateContactBtn");
            Assert.NotNull(button);
        }

        [Fact]
        public void UpdateButtonClickNavigatesToContactFormPage() {
            //given I have a contact
            CreateContact("Jill");

            //when I click the contact on the list
            When_IClickOnTheListItem();
            //and then click on the updateButton
            AppSession.FindElementByAccessibilityId("updateContactBtn").Click();

            //then I get redirected to the contact form page
            var updateContactTitle = AppSession.FindElementByAccessibilityId("contactFormTitle");
            Assert.NotNull(updateContactTitle);
            Assert.Equal("Update Contact", updateContactTitle.Text);
        }

        [Fact]
        public void SaveContactClickSavesChangesAndNavigatesToContacDetailsPage() {
            //given I have a contact
            CreateContact("Jill");

            //when I click the contact on the list
            When_IClickOnTheListItem();
            //and then click on the updateButton
            AppSession.FindElementByAccessibilityId("updateContactBtn").Click();

            string name = "Joao";
            string email = "aaa@aaa";
            string mobile = "111111";

            //and update the contact
            FillInContact(name, email, mobile);
            AppSession.FindElementByAccessibilityId("saveContactBtn").Click();

            //Then I am redirected to the contact details page
            var showContactTitle = AppSession.FindElementByAccessibilityId("showContactTitle");
            Assert.NotNull(showContactTitle);
            //with the right data filled
            var labels = AppSession.FindElementsByClassName("TextBlock").Select(x => x.Text).ToList();
            Assert.Contains(name, labels);
            Assert.Contains(email, labels);
            Assert.Contains(mobile, labels);
        }
    }
}
