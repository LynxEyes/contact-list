using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ContactListUITest.ContactList {
    public class UserSearchTest : BaseUITest {
        [Fact]
        public void UserSearchBarAndButtonExist() {
            Assert.NotNull(searchTextBox());
            Assert.NotNull(searchBtn());
        }

        [Fact]
        public void SearchForNonExistingNameResultsInAnEmptyList() {
            //given a set of users
            CreateContact("Rafael");
            CreateContact("Carlos");
            CreateContact("Alberto");

            // when I search for an unexisting name
            var box = searchTextBox();
            var button = searchBtn();

            box.SendKeys("Whatever");
            // Unfocus from the textbox to allow TwoWay Binding to happen
            Unfocus();
            button.Click();

            // Then I should see an empty list
            var items = AppSession.FindElementByAccessibilityId("contactList")
                                  .FindElementsByClassName("ListViewItem");
            Assert.Equal(0, items.Count);
        }

        [Fact]
        public void SearchForExistingNameResultsInAOneItemList() {
            //given a set of users
            CreateContact("Rafael");
            CreateContact("Carlos");
            CreateContact("Alberto");

            // when I search for an unexisting name
            var box = searchTextBox();
            var button = searchBtn();
            box.SendKeys("Rafael");
            Unfocus();
            button.Click();

            // Then I should see a one item list
            var list = AppSession.FindElementByAccessibilityId("contactList");
            var items = list.FindElementsByClassName("ListViewItem");
            Assert.Equal(1, items.Count);

            var names = list.FindElementsByClassName("TextBlock").Select(x => x.Text).ToList();
            Assert.Contains("Rafael", names);
            Assert.DoesNotContain("Carlos", names);
            Assert.DoesNotContain("Alberto", names);

            //even after consecutive searches
            box.Clear();
            box.SendKeys("Carlos");
            Unfocus();
            button.Click();

            var otherNames = list.FindElementsByClassName("TextBlock").Select(x => x.Text).ToList();
            Assert.Contains("Carlos", otherNames);
            Assert.DoesNotContain("Rafael", otherNames);
            Assert.DoesNotContain("Alberto", otherNames);
        }

        private OpenQA.Selenium.Appium.iOS.IOSElement searchTextBox() {
            return AppSession.FindElementByAccessibilityId("searchTextBox");
        }

        private OpenQA.Selenium.Appium.iOS.IOSElement searchBtn() {
            return AppSession.FindElementByAccessibilityId("searchBtn");
        }

        private void Unfocus() {
            AppSession.FindElementByClassName("ListView").Click();
        }
    }
}
