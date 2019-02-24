using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CLAVIER.QualityAssurance
{
    [Collection("QA")]
    public class GroupSecurityQATest : BaseQATest
    {
        [Fact]
        public void CheckUserCanHost()
        {
            ChromeDriver user = GetUserConnection();

            IWebElement hostButton = user.FindElementById("host-button");
            hostButton.Click();

            Task.Delay(5000).Wait();

            IWebElement group = user.FindElementByClassName("group");
            Assert.NotNull(group.Text);

            user.Quit();
        }

        [Fact]
        public void CheckUserCanJoin()
        {
            ChromeDriver driver = GetUserConnection();

            IWebElement hostButton = driver.FindElementById("host-button");
            hostButton.Click();

            Task.Delay(5000).Wait();
            IWebElement groupDriver = driver.FindElementByClassName("group");

            string groupId = groupDriver.Text.Split(null)[2];

            ChromeDriver navigator = GetUserConnection();
            IWebElement groupField = navigator.FindElementById("join-input");
            groupField.SendKeys(groupId);

            Task.Delay(5000).Wait();
            IWebElement joinButton = navigator.FindElementById("join-button");
            joinButton.Click();

            Task.Delay(5000).Wait();
            IWebElement groupNavigator = driver.FindElementByClassName("group");

            Assert.Equal(groupDriver.Text, groupNavigator.Text);
            driver.Quit();
            navigator.Quit();
    }
    }
}
