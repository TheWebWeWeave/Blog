using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;

namespace t3winc.blog.xunit.function.fixtures
{
    public class ChromeFixture : IDisposable
    {
        private RemoteWebDriver driver;

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