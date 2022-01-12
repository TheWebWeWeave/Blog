using blog_xunit.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Net.Http;
using Xunit;

namespace blog_xunit;

public class FunctionalTests : IClassFixture<DriverFixture>
{
    private readonly DriverFixture driverFixture;

    public FunctionalTests(DriverFixture driverFixture)
    {
        this.driverFixture = driverFixture;
    }


    [Fact]
    public void AboutPageIsReachable()
    {
        driverFixture.Driver.Navigate().GoToUrl("https://blog.t3winc.com/");
        driverFixture.Driver.FindElementByClassName("navbar-burger").Click();
        driverFixture.Driver.FindElementByXPath("//body/nav[1]/div[2]/div[1]/a[2]").Click();
        var postTitle = driverFixture.Driver.FindElementByClassName("title");

        Assert.Equal("About Donald on Software", postTitle.Text);
    }

    [Theory]
    [CsvData("./Parameters.csv")]
    public void PageExitsFromArchive(string Category, string Post_Title, string Archive_Year)
    {
        driverFixture.Driver.Navigate().GoToUrl("https://blog.t3winc.com/");

        driverFixture.Driver.FindElementByLinkText(Archive_Year).Click();
        driverFixture.Driver.FindElementByLinkText(Post_Title).Click();
        var postTitle = driverFixture.Driver.FindElementByCssSelector("h1.title");

        Assert.Equal(Post_Title, postTitle.Text);

        FindBrokenImages imageTest = new FindBrokenImages();
        var broken = imageTest.TestForBrokenImages(driverFixture);

        Assert.Equal(0, broken.Result);
    }

    [Theory]
    [CsvData("./Parameters.csv")]
    public void PageExitsFromCategory(string Category, string Post_Title, string Archive_Year)
    {
        driverFixture.Driver.Navigate().GoToUrl("https://blog.t3winc.com/");

        driverFixture.Driver.FindElementByLinkText(Category).Click();
        driverFixture.Driver.FindElementByLinkText(Post_Title).Click();
        var postTitle = driverFixture.Driver.FindElementByCssSelector("h1.title");

        Assert.Equal(Post_Title, postTitle.Text);
    }

    [Theory]
    [CsvData("./Parameters.csv")]
    public void NoImagesAreBrokenOnThePage(string Category, string Post_Title, string Archive_Year)
    {
        int broken_images = 0;
        FindBrokenImages imageTest = new FindBrokenImages();
        driverFixture.Driver.Navigate().GoToUrl("https://blog.t3winc.com/");

        driverFixture.Driver.FindElementByLinkText(Category).Click();
        driverFixture.Driver.FindElementByLinkText(Post_Title).Click();
        broken_images = imageTest.TestForBrokenImages(driverFixture).Result;

        Assert.Equal(0, broken_images);
    }

    [Theory]
    [CsvData("./Parameters.csv")]
    public void NoLinksAreBrokenOnThePage(string Category, string Post_Title, string Archive_Year)
    {
        int broken_links = 0;
        FindBrokenLinks linkTest = new FindBrokenLinks();
        driverFixture.Driver.Navigate().GoToUrl("https://blog.t3winc.com/");

        driverFixture.Driver.FindElementByLinkText(Archive_Year).Click();
        driverFixture.Driver.FindElementByLinkText(Post_Title).Click();
        broken_links = linkTest.TestForBrokenLinks(driverFixture).Result;

        Assert.Equal(0, broken_links);
    }

    [Fact]
    public void QuickTest()
    {
        int broken_links = 0;
        FindBrokenLinks linkTest = new FindBrokenLinks();
        IWebDriver driver = new ChromeDriver("C:/tools/selenium/");
        driver.Navigate().GoToUrl("https://blog.t3winc.com");

        driver.FindElement(By.LinkText("2016")).Click();
        driver.FindElement(By.LinkText("Some MSDeploy Tricks I have Learned")).Click();
        broken_links = linkTest.TestForBrokenLinks(driver).Result;

        Assert.Equal(0, broken_links);
    }

}