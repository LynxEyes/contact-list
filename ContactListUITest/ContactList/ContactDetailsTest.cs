using Xunit;
using System.Linq;

namespace ContactListUITest.ContactList {

    public class ContactDetailsTest : BaseUITest {

        private string Name = "Rafael";
        private string Email = "rafael@acme.com";
        private string Mobile = "1234567890";

        [Fact]
        public void ListSelectionChangeNavigatesToDetailPage() {
            //given there is a contact in the list
            CreateContact(Name, Email, Mobile);

            //when I click the contact on the list
            When_IClickOnTheListItem();

            //then there is a navigation to the detail page
            var showContactTitle = AppSession.FindElementByAccessibilityId("showContactTitle");
            Assert.NotNull(showContactTitle);
        }

        [Fact]
        public void DisplaysAllContactDetails() {
            // Given there is a contact
            CreateContact(Name, Email, Mobile);

            // when I click on its item on the contact list
            When_IClickOnTheListItem();

            // Then I can see all the contact details in its own page
            var labels = AppSession.FindElementsByClassName("TextBlock").Select(x => x.Text).ToList();
            Assert.Contains(Name, labels);
            Assert.Contains(Email, labels);
            Assert.Contains(Mobile, labels);
        }


    }
}
