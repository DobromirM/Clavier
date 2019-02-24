using Xunit;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.IO;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;

namespace CLAVIER.QualityAssurance
{

    [Collection("QA")]
    public class FileQATest : BaseQATest
    {
        [Fact]
        public void CheckVisibleOpenForDriver()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            IWebElement openFileInput = driver.FindElementById("open-file-input");

            Assert.NotNull(openFileInput);

            driver.Quit();
            navigator.Quit();
        }

        [Fact]
        public void CheckNotVisibleOpenForNavigator()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            ReadOnlyCollection<IWebElement> openFileInputs = navigator.FindElementsById("open-file-input");

            Assert.Empty(openFileInputs);

            driver.Quit();
            navigator.Quit();
        }

        [Fact]
        public void VerifyEditorDisplaysFileContents()
        {
            ChromeDriver driver = GetDriverConnection();

            string filepath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\Resources\\TestTextFile.txt");

            IWebElement openFileInput = driver.FindElementById("open-file-input");
            openFileInput.SendKeys(filepath);

            Task.Delay(5000).Wait();

            IWebElement line = driver.FindElementByClassName("ace_identifier");
            
            Assert.Equal("some_text_file_for_testing_purposes", line.Text);

            driver.Quit();
        }

        [Fact]
        public void VerifyFileContentsForNavigator()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            string filepath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\Resources\\TestTextFile.txt");

            IWebElement openFileInput = driver.FindElementById("open-file-input");
            openFileInput.SendKeys(filepath);
            Task.Delay(5000).Wait();

            IWebElement driverLine = driver.FindElementByClassName("ace_line");
            IWebElement navigatorLine = navigator.FindElementByClassName("ace_line");

            Assert.Equal(driverLine.Text, navigatorLine.Text);

            driver.Quit();
            navigator.Quit();
        }

        [Fact]
        public void VerifyEditorDisplaysFileSyntax()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            string filepath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\Resources\\TestSyntax.txt");

            IWebElement openFileInput = driver.FindElementById("open-file-input");
            openFileInput.SendKeys(filepath);

            Task.Delay(5000).Wait();

            IWebElement driverWord = driver.FindElementByClassName("ace_keyword");
            IWebElement navigatorWord = navigator.FindElementByClassName("ace_keyword");

            Assert.Equal("rgba(200, 0, 164, 1)", driverWord.GetCssValue("color"));
            Assert.Equal("rgba(200, 0, 164, 1)", navigatorWord.GetCssValue("color"));

            driver.Quit();
            navigator.Quit();
        }

        [Fact]
        public void VerifySaveDriver()
        {
            string folderPath = Path.GetFullPath("C:\\tmp-selenium");
            Directory.CreateDirectory(folderPath);

            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", folderPath);
            options.AddUserProfilePreference("disable-popup-blocking", "true");
            ChromeDriver driver = GetDriverConnection(options);

            string expected = "this_is_a_save_test_driver";
            IWebElement driverEditor = driver.FindElementById("code-editor");
            Actions actions = new Actions(driver);
            actions.MoveToElement(driverEditor);
            actions.Click();
            actions.SendKeys(expected);
            actions.Build().Perform();
            Task.Delay(5000).Wait();

            IWebElement driverDownload = driver.FindElementByClassName("download");
            driverDownload.Click();
            Task.Delay(5000).Wait();

            IWebElement group = driver.FindElementByClassName("group");
            string groupId = group.Text.Split(null)[2];
            string fileName = "code-" + groupId + ".txt";

            string actual = File.ReadAllText(Path.Combine(folderPath, fileName));
            Assert.Equal(expected, actual);

            Directory.Delete(folderPath, true);

            driver.Close();
        }

        [Fact]
        public void VerifySaveNavigator()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;

            string folderPath = Path.GetFullPath("C:\\tmp-selenium");
            Directory.CreateDirectory(folderPath);

            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", folderPath);
            options.AddUserProfilePreference("disable-popup-blocking", "true");
            ChromeDriver navigator = GetNavigatorConnection(url, options);

            string expected = "this_is_a_save_test_navigator";
            IWebElement driverEditor = driver.FindElementById("code-editor");
            Actions actions = new Actions(driver);
            actions.MoveToElement(driverEditor);
            actions.Click();
            actions.SendKeys(expected);
            actions.Build().Perform();
            Task.Delay(5000).Wait();

            IWebElement navigatorDownload = navigator.FindElementByClassName("download");
            navigatorDownload.Click();
            Task.Delay(5000).Wait();

            IWebElement group = driver.FindElementByClassName("group");
            string groupId = group.Text.Split(null)[2];
            string fileName = "code-" + groupId + ".txt";

            string actual = File.ReadAllText(Path.Combine(folderPath, fileName));
            Assert.Equal(expected, actual);

            Directory.Delete(folderPath, true);

            driver.Close();
            navigator.Close();
        }

        [Fact]
        public void VerifyFileOpenTwice()
        {
            ChromeDriver driver = GetDriverConnection();

            string filepath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\Resources\\TestTextFile.txt");

            IWebElement openFileInput = driver.FindElementById("open-file-input");
            openFileInput.SendKeys(filepath);

            Task.Delay(5000).Wait();

            IWebElement line = driver.FindElementByClassName("ace_identifier");

            Assert.Equal("some_text_file_for_testing_purposes", line.Text);

            IWebElement driverEditor = driver.FindElementById("code-editor");
            Actions actions = new Actions(driver);
            actions.MoveToElement(driverEditor);
            actions.Click();
            actions.SendKeys("_new_stuff");
            actions.Build().Perform();
            Task.Delay(5000).Wait();

            line = driver.FindElementByClassName("ace_identifier");
            Assert.Equal("some_text_file_for_testing_purposes_new_stuff", line.Text);

            openFileInput = driver.FindElementById("open-file-input");
            openFileInput.SendKeys(filepath);

            Task.Delay(5000).Wait();

            line = driver.FindElementByClassName("ace_identifier");
            Assert.Equal("some_text_file_for_testing_purposes", line.Text);

            driver.Quit();
        }
    }
}
