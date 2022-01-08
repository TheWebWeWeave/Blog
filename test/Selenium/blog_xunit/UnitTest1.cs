using System;
using System.Diagnostics;
using Xunit;

namespace blog_xunit;

public class UnitTest1 : IClassFixture<DriverFixture>
{
    private readonly DriverFixture driverFixture;

    public UnitTest1(DriverFixture driverFixture)
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
}