using t3winc.blog.xunit.function.fixtures;
using t3winc.blog.xunit.function.helper;
using Xunit;

namespace t3winc.blog.xunit.function;

public class ChromeTests : IClassFixture<ChromeFixture>
{
    private readonly ChromeFixture driverFixture;

    public ChromeTests(ChromeFixture driverFixture)
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
}