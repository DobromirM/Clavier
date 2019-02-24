using Xunit;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading.Tasks;

namespace CLAVIER.QualityAssurance
{
    [Collection("QA")]
    public class NotesEditorQATest : BaseQATest
    {
        [Fact]
        public void CheckVisibility()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            IWebElement driverNotes = driver.FindElementById("note-editor");
            IWebElement navigatorNotes = navigator.FindElementById("note-editor");

            Assert.NotNull(driverNotes);
            Assert.NotNull(navigatorNotes);

            driver.Quit();
            navigator.Quit();
        }

        [Fact]
        public void VerifyNavigatorAbleToWrite()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            IWebElement navigatorNotes = navigator.FindElementById("note-editor");

            string expected = "The navigator should be able to write notes";
            navigatorNotes.SendKeys(expected);

            Assert.Equal(expected, navigatorNotes.GetAttribute("value"));

            driver.Quit();
            navigator.Quit();
        }

        [Fact]
        public void VerifyDriverUnableToWrite()
        {
            ChromeDriver driver = GetDriverConnection();

            IWebElement driverNotes = driver.FindElementById("note-editor");

            string notes = "The driver should not be able to make notes";
            driverNotes.SendKeys(notes);

            Assert.Equal("", driverNotes.GetAttribute("value"));

            driver.Quit();
        }

        [Fact]
        public void VerifyDriverNotesUpdates()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            IWebElement navigatorNotes = navigator.FindElementById("note-editor");
            IWebElement driverNotes = driver.FindElementById("note-editor");

            string expected = "Make sure that new notes appear on the drivers's screen";
            navigatorNotes.SendKeys(expected);

            //Wait 5 seconds before reading the text
            Task.Delay(5000).Wait();
            Assert.Equal(expected, driverNotes.GetAttribute("value"));

            driver.Quit();
            navigator.Quit();
        }

        [Fact]
        public void VerifyDriverCanFreeze()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            IWebElement navigatorNotes = navigator.FindElementById("note-editor");
            IWebElement driverNotes = driver.FindElementById("note-editor");

            string firstNote = "First Note";
            navigatorNotes.SendKeys(firstNote);

            //Wait 5 seconds before reading the text
            Task.Delay(5000).Wait();
            Assert.Equal(firstNote, driverNotes.GetAttribute("value"));

            //Freeze the notes
            IWebElement freezeButton = driver.FindElementByClassName("freeze-button");
            freezeButton.Click();

            string secondNote= "Second Note";
            navigatorNotes.SendKeys(secondNote);

            //Wait 5 seconds before reading the text
            Task.Delay(5000).Wait();
            Assert.Equal(firstNote, driverNotes.GetAttribute("value"));

            driver.Quit();
            navigator.Quit();
        }

        [Fact]
        public void VerifyDriverCanUnfreeze()
        {
            ChromeDriver driver = GetDriverConnection();
            string url = driver.Url;
            ChromeDriver navigator = GetNavigatorConnection(url);

            IWebElement navigatorNotes = navigator.FindElementById("note-editor");
            IWebElement driverNotes = driver.FindElementById("note-editor");

            string firstNote = "First Note";
            navigatorNotes.SendKeys(firstNote);

            //Wait 5 seconds before reading the text
            Task.Delay(5000).Wait();
            Assert.Equal(firstNote, driverNotes.GetAttribute("value"));

            //Freeze the notes
            IWebElement freezeButton = driver.FindElementByClassName("freeze-button");
            freezeButton.Click();

            string secondNote = "Second Note";
            navigatorNotes.SendKeys(secondNote);

            //Wait 5 seconds before reading the text
            Task.Delay(5000).Wait();
            Assert.Equal(firstNote, driverNotes.GetAttribute("value"));

            //Unfreeze the notes
            freezeButton.Click();

            Assert.Equal(firstNote + secondNote, driverNotes.GetAttribute("value"));

            driver.Quit();
            navigator.Quit();
        }

    }
}
