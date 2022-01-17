using t3winc.blog.xunit.function.fixtures;
using t3winc.blog.xunit.function.helper;
using Xunit;

namespace blog_xunit;

public class EdgeTests : IClassFixture<EdgeFixture>
{
    private readonly EdgeFixture driverFixture;

    public EdgeTests(EdgeFixture driverFixture)
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
    [CsvData("./Data/Parameters.csv")]
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
}