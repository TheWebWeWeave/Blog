using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;

namespace t3winc.blog.xunit.function.fixtures
{
    public class FirefoxFixture : IDisposable
    {
        private RemoteWebDriver driver;

        public FirefoxFixture()
        {
            var firefoxOption = new FirefoxOptions();
            firefoxOption.AddAdditionalOption("se:recordVideo", true);
            firefoxOption.AddArgument("--disable-dev-shm-usage");
            driver = new RemoteWebDriver(new Uri("https://testing.t3winc.com/"), firefoxOption);
        }

        public RemoteWebDriver Driver => driver;

        public void Dispose()
        {
            Driver.Quit();
        }
    }
}