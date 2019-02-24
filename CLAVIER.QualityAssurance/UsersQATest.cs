using Xunit;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading.Tasks;

namespace CLAVIER.QualityAssurance
{
    [Collection("QA")]
    public class UsersQATest : BaseQATest
    {
        [Fact]
        public void VefiryRoles()
        {
            ChromeDriver firstUser = GetDriverConnection();
            string url = firstUser.Url;
            ChromeDriver secondUser = GetNavigatorConnection(url);

            string firstUserRole = firstUser.FindElementById("role").Text;
            string secondUserRole = secondUser.FindElementById("role").Text;

            Assert.Equal("driver", firstUserRole);
            Assert.Equal("navigator", secondUserRole);

            firstUser.Quit();
            secondUser.Quit();
        }

        [Fact]
        public void SwitchRolesSuccess()
        {
            ChromeDriver firstUser = GetDriverConnection();
            string url = firstUser.Url;
            ChromeDriver secondUser = GetNavigatorConnection(url);

            string firstUserRole = firstUser.FindElementById("role").Text;
            string secondUserRole = secondUser.FindElementById("role").Text;

            Assert.Equal("driver", firstUserRole);
            Assert.Equal("navigator", secondUserRole);

            IWebElement firstSwitch = firstUser.FindElementByClassName("switch-button");
            firstSwitch.Click();
            IWebElement secondSwitch = secondUser.FindElementByClassName("switch-button");
            secondSwitch.Click();

            Task.Delay(5000).Wait();

            firstUserRole = firstUser.FindElementById("role").Text;
            secondUserRole = secondUser.FindElementById("role").Text;

            Assert.Equal("driver", secondUserRole);
            Assert.Equal("navigator", firstUserRole);

            firstUser.Quit();
            secondUser.Quit();
        }

        [Fact]
        public void SwitchRolesOnlyDriver()
        {
            ChromeDriver firstUser = GetDriverConnection();
            string url = firstUser.Url;
            ChromeDriver secondUser = GetNavigatorConnection(url);

            string firstUserRole = firstUser.FindElementById("role").Text;
            string secondUserRole = secondUser.FindElementById("role").Text;

            Assert.Equal("driver", firstUserRole);
            Assert.Equal("navigator", secondUserRole);

            IWebElement firstSwitch = firstUser.FindElementByClassName("switch-button");
            IWebElement secondSwitch = secondUser.FindElementByClassName("switch-button");
            firstSwitch.Click();

            Task.Delay(5000).Wait();

            firstUserRole = firstUser.FindElementById("role").Text;
            secondUserRole = secondUser.FindElementById("role").Text;

            Assert.Equal("driver", firstUserRole);
            Assert.Equal("navigator", secondUserRole);

            Assert.Equal("rgba(0, 128, 0, 1)", firstSwitch.GetCssValue("background-color"));
            Assert.Equal("rgba(0, 128, 0, 1)", secondSwitch.GetCssValue("background-color"));

            firstUser.Quit();
            secondUser.Quit();
        }

        [Fact]
        public void SwitchRolesOnlyNavigator()
        {
            ChromeDriver firstUser = GetDriverConnection();
            string url = firstUser.Url;
            ChromeDriver secondUser = GetNavigatorConnection(url);

            string firstUserRole = firstUser.FindElementById("role").Text;
            string secondUserRole = secondUser.FindElementById("role").Text;

            Assert.Equal("driver", firstUserRole);
            Assert.Equal("navigator", secondUserRole);

            IWebElement firstSwitch = firstUser.FindElementByClassName("switch-button");
            IWebElement secondSwitch = secondUser.FindElementByClassName("switch-button");
            secondSwitch.Click();

            Task.Delay(5000).Wait();

            firstUserRole = firstUser.FindElementById("role").Text;
            secondUserRole = secondUser.FindElementById("role").Text;

            Assert.Equal("driver", firstUserRole);
            Assert.Equal("navigator", secondUserRole);

            Assert.Equal("rgba(0, 128, 0, 1)", firstSwitch.GetCssValue("background-color"));
            Assert.Equal("rgba(0, 128, 0, 1)", secondSwitch.GetCssValue("background-color"));

            firstUser.Quit();
            secondUser.Quit();
        }

        [Fact]
        public void SwitchRolesTwiceSuccess()
        {
            ChromeDriver firstUser = GetDriverConnection();
            string url = firstUser.Url;
            ChromeDriver secondUser = GetNavigatorConnection(url);

            string firstUserRole = firstUser.FindElementById("role").Text;
            string secondUserRole = secondUser.FindElementById("role").Text;

            Assert.Equal("driver", firstUserRole);
            Assert.Equal("navigator", secondUserRole);

            IWebElement firstSwitch = firstUser.FindElementByClassName("switch-button");
            firstSwitch.Click();
            IWebElement secondSwitch = secondUser.FindElementByClassName("switch-button");
            secondSwitch.Click();

            Task.Delay(5000).Wait();

            firstUserRole = firstUser.FindElementById("role").Text;
            secondUserRole = secondUser.FindElementById("role").Text;

            Assert.Equal("driver", secondUserRole);
            Assert.Equal("navigator", firstUserRole);

            firstSwitch = firstUser.FindElementByClassName("switch-button");
            firstSwitch.Click();
            secondSwitch = secondUser.FindElementByClassName("switch-button");
            secondSwitch.Click();

            Task.Delay(5000).Wait();

            firstUserRole = firstUser.FindElementById("role").Text;
            secondUserRole = secondUser.FindElementById("role").Text;

            Assert.Equal("driver", firstUserRole);
            Assert.Equal("navigator", secondUserRole);

            firstUser.Quit();
            secondUser.Quit();
        }
    }
}

