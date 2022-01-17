using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog_xunit
{
    public class ChromeFixture : IDisposable
    {
        RemoteWebDriver driver;

        public ChromeFixture()
        {
            var chromeOption = new ChromeOptions();
            chromeOption.AddAdditionalOption("se:recordVideo", true);
            chromeOption.AddArgument("--disable-dev-shm-usage");
            driver = new RemoteWebDriver(new Uri("https://testing.t3winc.com/"), chromeOption);
        }

        public RemoteWebDriver Driver => driver;

        public void Dispose()
        {
            Driver.Quit();
        }
    }
}
