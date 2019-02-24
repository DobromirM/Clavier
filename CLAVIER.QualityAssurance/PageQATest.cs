using Xunit;
using OpenQA.Selenium.Chrome;

namespace CLAVIER.QualityAssurance
{
    [Collection("QA")]
    public class PageQATest : BaseQATest
    {
        [Fact]
        public void VerifyConnection()
        {
            ChromeDriver user = GetDriverConnection();
            string title = user.Title;
            Assert.Equal("Clavier", title);
            user.Quit();
        }

        [Fact]
        public void VerifySource()
        {
            ChromeDriver user = GetDriverConnection();
            string pageSource = user.PageSource;
            Assert.NotNull(pageSource);
            user.Quit();
        }
    }
}
