using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace CLAVIER.QualityAssurance
{
    public class BaseQATest
    {
        private readonly string _driverUrl = "https://localhost:5001/join?group=";
        private readonly string _userUrl = "https://localhost:5001/";

        protected ChromeDriver GetDriverConnection(ChromeOptions options = null)
        {
            ChromeDriver driver;

            if(options == null)
            {
                driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                {
                    Url = _driverUrl
                };
            }
            else
            {
                driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options)
                {
                    Url = _driverUrl
                };
            }
           
            //Wait for 3 seconds to give the browser enought time to completely load the page
            Task.Delay(3000).Wait();
            return driver;
        }

        protected ChromeDriver GetNavigatorConnection(string url, ChromeOptions options = null)
        {
            ChromeDriver driver;

            if (options == null)
            {
                driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                {
                    Url = url
                };
            }
            else
            {
                driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options)
                {
                    Url = url
                };
            }

            //Wait for 3 seconds to give the browser enought time to completely load the page
            Task.Delay(3000).Wait();
            return driver;
        }

        protected ChromeDriver GetUserConnection()
        {
            ChromeDriver user = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            {
                Url = _userUrl
            };

            //Wait for 3 seconds to give the browser enought time to completely load the page
            Task.Delay(3000).Wait();
            return user;
        }
    }
}
