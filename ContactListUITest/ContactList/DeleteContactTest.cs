using System.Linq;
using Xunit;

namespace ContactListUITest.ContactList {
    public class DeleteContactTest : BaseUITest {
        private string Name = "Rafael";
        private string Email = "rafael@acme.com";
        private string Mobile = "1234567890";

        [Fact]
        public void DeleteContactButtonNavigatesToMainPage() {
            // Given there is a contact
            CreateContact(Name, Email, Mobile);

            // when I click on its item on the contact list
            When_IClickOnTheListItem();

            //and click on the delete button on the details page
            AppSession.FindElementByAccessibilityId("deleteContactBtn").Click();

            //then I am navigated to main page
            var TitleLabel = AppSession.FindElementByAccessibilityId("title");
            Assert.NotNull(TitleLabel);
        }

        [Fact]
        public void DeletedContactIsRemovedFromList() {
            // Given there is a contact
            CreateContact(Name, Email, Mobile);

            // when I click on its item on the contact list
            When_IClickOnTheListItem();

            //and click on the delete button on the details page
            AppSession.FindElementByAccessibilityId("deleteContactBtn").Click();

            //the contact gets removed from the main page list
            var list = AppSession.FindElementByAccessibilityId("contactList");
            var names = list.FindElementsByClassName("TextBlock").Where(x => x.Text == Name).ToList();
            Assert.Empty(names);
        }
    }
}
