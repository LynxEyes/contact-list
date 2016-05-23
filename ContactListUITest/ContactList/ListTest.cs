using Xunit;
using System.Linq;

namespace ContactListUITest.ContactList {

    public class ListTest : BaseUITest {
        [Fact]
        public void TitleLabelExists() {
            var TitleLabel = AppSession.FindElementByAccessibilityId("title");
            Assert.NotNull(TitleLabel);
        }

        [Fact]
        public void ListExistsAndContainsElements() {
            // Given there are 3 contacts named Ze, Carlos, Alberto
            CreateContact("Rafael");
            CreateContact("Carlos");
            CreateContact("Alberto");

            // when I view the list of contacts
            var list = AppSession.FindElementByAccessibilityId("contactList");
            Assert.NotNull(list);

            // Then I can see it has 3 names: Rafael, Carlos and Alberto
            var items = list.FindElementsByClassName("ListViewItem");
            Assert.Equal(3, items.Count);

            var names = list.FindElementsByClassName("TextBlock").Select(x => x.Text).ToList();
            Assert.Contains("Rafael", names);
            Assert.Contains("Carlos", names);
            Assert.Contains("Alberto", names);
        }
    }
}
