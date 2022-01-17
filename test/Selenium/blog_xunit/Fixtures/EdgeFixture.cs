using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using System;

namespace t3winc.blog.xunit.function.fixtures
{
    public class EdgeFixture : IDisposable
    {
        private RemoteWebDriver driver;

        public EdgeFixture()
        {
            var edgeOption = new EdgeOptions();
            edgeOption.AddAdditionalOption("se:recordVideo", true);
            edgeOption.AddArgument("--disable-dev-shm-usage");
            driver = new RemoteWebDriver(new Uri("https://testing.t3winc.com/"), edgeOption);
        }

        public RemoteWebDriver Driver => driver;

        public void Dispose()
        {
            Driver.Quit();
        }
    }
}