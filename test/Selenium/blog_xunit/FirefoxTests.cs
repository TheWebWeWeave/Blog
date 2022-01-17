using t3winc.blog.xunit.function.fixtures;
using t3winc.blog.xunit.function.helper;
using Xunit;

namespace blog_xunit;

public class FirefoxTests : IClassFixture<FirefoxFixture>
{
    private readonly FirefoxFixture driverFixture;

    public FirefoxTests(FirefoxFixture driverFixture)
    {
        this.driverFixture = driverFixture;
    }

    [Theory]
    [CsvData("./Data/Parameters.csv")]
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
    [CsvData("./Data/Parameters.csv")]
    public void PageExitsFromCategory(string Category, string Post_Title, string Archive_Year)
    {
        driverFixture.Driver.Navigate().GoToUrl("https://blog.t3winc.com/");

        driverFixture.Driver.FindElementByLinkText(Category).Click();
        driverFixture.Driver.FindElementByLinkText(Post_Title).Click();
        var postTitle = driverFixture.Driver.FindElementByCssSelector("h1.title");

        Assert.Equal(Post_Title, postTitle.Text);
    }
}