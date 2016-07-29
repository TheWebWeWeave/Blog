using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheWebWeWeave.BlogAutomation;

namespace TheWebWeWeave.BlogUITests
{
    [TestClass]
    public class LinkTests
    {
        [TestInitialize]
        public void Init()
        {
            Driver.Initialize();
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void Click_On_Blog_About_Menu_Opens_About_Page()
        {
            AboutPage.Goto();
            AboutPage.FindTitle();

            Assert.AreEqual(AboutPage.PostTitle, "About Me","About Me Page Failed to Open");
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void Blog_Should_Have_One_or_more_Tags()
        {
            HomePage.Goto();
            HomePage.OpenFirstBlog();
            HomePage.GetTagCount();

            Assert.IsTrue(HomePage.TagCount > 0,"Blog Post has no Tags");
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void Blog_Found_From_Tag_Page()
        {
            HomePage.Goto();
            HomePage.OpenFirstBlog();
            HomePage.FindTitle();
            HomePage.ClickFirstTag();

            Assert.IsTrue(ArchivePages.IsPostInArchive(HomePage.PostTitle), 
                String.Format("The post \"{0}\" not found in the \"{1}\" tag page", 
                HomePage.PostTitle, HomePage.TagName));
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void Chocolatey_Post_Exists_And_Was_Deployed()
        {
            string post = "How I Use Chocolatey in my Releases";
            HomePage.Goto();
            Assert.IsTrue(HomePage.PostFound(post), String.Format("The post \"{0}\" was not found", post));
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Close();
        }
    }
}
