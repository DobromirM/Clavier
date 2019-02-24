using Xunit;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Interactions;
using System;

namespace CLAVIER.QualityAssurance
{
    [Collection("QA")]
    public class CodeEditorQATest : BaseQATest
    {
        [Fact]
        public void CheckVisibility()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            IWebElement driverEditor = driver.FindElementById("code-editor");
            IWebElement navigatorEditor = navigator.FindElementById("code-editor");

            Assert.NotNull(driverEditor);
            Assert.NotNull(navigatorEditor);

            driver.Quit();
            navigator.Quit();
        }

        [Fact]
        public void VerifyDriverAbleToWrite()
        {
            ChromeDriver driver = GetDriverConnection();

            IWebElement driverEditor = driver.FindElementById("code-editor");

            string expected = "Test_to_see_if_the_driver_can_write";

            Actions actions = new Actions(driver);
            actions.MoveToElement(driverEditor);
            actions.Click();
            actions.SendKeys(expected);
            actions.Build().Perform();

            Task.Delay(5000).Wait();
            IWebElement line = driver.FindElementByClassName("ace_identifier");
            Assert.Equal(expected, line.Text);

            driver.Quit();
        }

        [Fact]
        public void VerifyNavigatorUnableToWrite()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            IWebElement navigatorEditor = navigator.FindElementById("code-editor");

            string text = "Test_to_see_if_the_navigator_can_write";

            Actions actions = new Actions(navigator);
            actions.MoveToElement(navigatorEditor);
            actions.Click();
            actions.SendKeys(text);
            actions.Build().Perform();

            Task.Delay(5000).Wait();
            IWebElement line = driver.FindElementByClassName("ace_line");
            Assert.Equal("", line.Text);

            driver.Quit();
            navigator.Quit();
        }

        [Fact]
        public void VerifyNavigatorEditorUpdates()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            IWebElement driverEditor = driver.FindElementById("code-editor");

            string expected = "Make_sure_that_the_text_appears_on_the_navigator's_screen";

            Actions actions = new Actions(driver);
            actions.MoveToElement(driverEditor);
            actions.Click();
            actions.SendKeys(expected);
            actions.Build().Perform();

            //Wait 5 seconds before reading the text
            Task.Delay(5000).Wait();
            IWebElement line = navigator.FindElementByClassName("ace_line");
            Assert.Equal(expected, line.Text);

            driver.Quit();
            navigator.Quit();
        }

        [Fact]
        public void VerifyNavigatorCanHighlight()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            IWebElement driverEditor = driver.FindElementById("code-editor");

            Actions actions = new Actions(driver);
            actions.MoveToElement(driverEditor);
            actions.Click();
            actions.SendKeys("test");
            actions.Build().Perform();

            Task.Delay(5000).Wait();
            IWebElement line = navigator.FindElementByClassName("ace_gutter-cell");
            line.Click();

            //Wait 5 seconds before trying to see if the line was highlighted
            Task.Delay(5000).Wait();
            IWebElement selectedElement = driver.FindElementByClassName("yellow-marker");

            Assert.Equal("rgba(255, 255, 0, 1)", selectedElement.GetCssValue("background-color"));

            driver.Quit();
            navigator.Quit();
        }

        [Fact]
        public void VerifyDriverCannotHighlight()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            IWebElement driverEditor = driver.FindElementById("code-editor");

            Actions actions = new Actions(driver);
            actions.MoveToElement(driverEditor);
            actions.Click();
            actions.SendKeys("test");
            actions.Build().Perform();

            Task.Delay(5000).Wait();
            IWebElement line = driver.FindElementByClassName("ace_gutter-cell");
            line.Click();

            //Wait 5 seconds before trying to see if the line was highlighted
            Task.Delay(5000).Wait();

            bool notFound = false;
            try
            {
                IWebElement selectedElements = navigator.FindElementByClassName("yellow-marker");
            }
            catch(Exception e)
            {
                notFound = true;
            }

            Assert.True(notFound);

            driver.Quit();
            navigator.Quit();
        }

        [Fact]
        public void VerifyKyewordHighlight()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            IWebElement driverEditor = driver.FindElementById("code-editor");

            Actions actions = new Actions(driver);
            actions.MoveToElement(driverEditor);
            actions.Click();
            actions.SendKeys("public");
            actions.Build().Perform();

            Task.Delay(5000).Wait();

            IWebElement driverWord = driver.FindElementByClassName("ace_keyword");
            IWebElement navigatorWord = navigator.FindElementByClassName("ace_keyword");

            //Wait 5 seconds before trying to see if the line was highlighted
            Task.Delay(5000).Wait();

            Assert.Equal("rgba(200, 0, 164, 1)", driverWord.GetCssValue("color"));
            Assert.Equal("rgba(200, 0, 164, 1)", navigatorWord.GetCssValue("color"));

            driver.Quit();
            navigator.Quit();
        }

        [Fact]
        public void VerifyStringHighlight()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            IWebElement driverEditor = driver.FindElementById("code-editor");

            Actions actions = new Actions(driver);
            actions.MoveToElement(driverEditor);
            actions.Click();
            actions.SendKeys("\"hello\"");
            actions.Build().Perform();

            Task.Delay(5000).Wait();

            IWebElement driverWord = driver.FindElementByClassName("ace_string");
            IWebElement navigatorWord = navigator.FindElementByClassName("ace_string");

            //Wait 5 seconds before trying to see if the line was highlighted
            Task.Delay(5000).Wait();

            Assert.Equal("rgba(223, 0, 2, 1)", driverWord.GetCssValue("color"));
            Assert.Equal("rgba(223, 0, 2, 1)", navigatorWord.GetCssValue("color"));

            driver.Quit();
            navigator.Quit();
        }
    }
}
