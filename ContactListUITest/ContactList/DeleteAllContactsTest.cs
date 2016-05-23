using Xunit;

namespace ContactListUITest.ContactList {
    public class DeleteAllContactsTest : BaseUITest {
        [Fact]
        public void DeleteAllContactsButtonExists() {
            var deleteAllBtn = AppSession.FindElementByAccessibilityId("deleteAllContactsBtn");
            Assert.NotNull(deleteAllBtn);
        }

        [Fact]
        public void DeleteAllContactsEmptiesListView() {
            // Given a set of contacts
            CreateContact("John");
            CreateContact("Jack");
            CreateContact("Jill");

            // When I delete all of them
            AppSession.FindElementByAccessibilityId("deleteAllContactsBtn").Click();

            // Then the contact list is empty
            var list = AppSession.FindElementByAccessibilityId("contactList")
                                 .FindElementsByClassName("ListViewItem");
            Assert.Equal(0, list.Count);
        }
    }
}
