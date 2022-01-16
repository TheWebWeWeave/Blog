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
    public class DriverFixture : IDisposable
    {
        RemoteWebDriver driver;

        public DriverFixture()
        {
            var chromeOption = new ChromeOptions();
            chromeOption.AddAdditionalOption("se:recordVideo", true);
            chromeOption.AddArgument("--disable-dev-shm-usage");
            driver = new RemoteWebDriver(new Uri("https://testing.t3winc.com/"), chromeOption);          
        }

        // public DriverFixture(BrowserType browserType)
        // {
        //     var browserOption = GetBrowserOptions(browserType);
        //     browserOption.AddArgument("--disable-dev-shm-usage");
        //     driver = new RemoteWebDriver(new Uri("https://testing.t3winc.com/"), browserOption);
        // }

        public RemoteWebDriver Driver => driver;

        public void Dispose()
        {
            Driver.Quit();
        }

        private dynamic GetBrowserOptions(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    {
                        var chromeOption = new ChromeOptions();
                        chromeOption.AddAdditionalOption("se:recordVideo", true);
                        return chromeOption;
                    }
                case BrowserType.Firefox:
                    {
                        var firefoxOption = new FirefoxOptions();
                        firefoxOption.AddAdditionalOption("se:recordVideo", true);
                        return firefoxOption;
                    }
                case BrowserType.Safari:
                    return new SafariOptions();
                case BrowserType.Edge:
                    return new EdgeOptions();
                default:
                    return new ChromeOptions();
            }
        }
    }

    public enum BrowserType 
    {
        Chrome,
        Firefox,
        Safari,
        Edge
    }
}
