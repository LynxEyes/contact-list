using Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommonUnitTests.Services {

    public class GithubContributorsTest : BaseTest {
        private GithubContributors subject = new GithubContributors();

        [Fact]
        public async void xpt() {
            //given

            //when
            var result = await subject.GetContributors("EqualExperts", "contact-list");

            //then
            Assert.Contains("LynxEyes", result);
            Assert.Contains("Shemahmforash", result);
            Assert.Contains("rafaelfiume", result);
        }
    }
}