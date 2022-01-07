using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog_xunit
{
    public class DriverFixture : IDisposable
    {
        RemoteWebDriver driver;
        public DriverFixture()
        {
            var chromeOption = new ChromeOptions();
            chromeOption.AddAdditionalOption("se:recordVideo", true);
            chromeOption.AddArgument("--disable-dev-shm-usage");
            driver = new RemoteWebDriver(new Uri("http://localhost:4444/"), chromeOption);
        }

        public RemoteWebDriver Driver => driver;

        public void Dispose()
        {
            Driver.Quit();
        }
    }
}
