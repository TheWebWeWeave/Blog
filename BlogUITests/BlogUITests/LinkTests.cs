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

        [TestMethod]
        [TestCategory("UITest")]
        public void MSDeploy_Tips_Post_Exists_And_Was_Deployed()
        {
            string post = "Some MSDeploy Tricks I've Learned";
            HomePage.Goto();
            Assert.IsTrue(HomePage.PostFound(post), String.Format("The post \"{0}\" was not found", post));
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void Git_SubModule_Post_Exists_And_Was_Deployed()
        {
            string post = "My Experience with Git Sub-modules";
            HomePage.Goto();
            Assert.IsTrue(HomePage.PostFound(post), string.Format("The post \"{0}\" was not found", post));
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void ThreeRulesForReleases_Exists_And_Was_Deployed()
        {
            string post = "My New 3 Rules for Releases";
            HomePage.Goto();
            Assert.IsTrue(HomePage.PostFound(post), string.Format("The post \"{0}\" was not found", post));
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void SecurityConfiguration_Exists_And_Was_Deployed()
        {
            string post = "Security Configuration for Teams";
            HomePage.Goto();
            Assert.IsTrue(HomePage.PostFound(post), string.Format("The post \"{0}\" was not found", post));
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void FirstInternalLinksMsDeploy_Works_And_IsValid()
        {
            string post = "Some MSDeploy Tricks I've Learned";
            HomePage.Goto();
            HomePage.GoToPost(post);
            Assert.IsTrue(MSDeployPost.DoesInternalLinkWork("In an earlier post I talked about Hexo the tool I use for this Blog", "A New Start on an Old Blog"));
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void SecondInternalLinksMsDeploy_Works_And_IsValid()
        {
            string post = "Some MSDeploy Tricks I've Learned";
            HomePage.Goto();
            HomePage.GoToPost(post);
            Assert.IsTrue(MSDeployPost.DoesInternalLinkWork("Hexo Post", "A New Start on an Old Blog"));
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void VersionArgument_Exists_And_Was_Deployed()
        {
            string post = "An Argument against the Date Based Version Number";
            HomePage.Goto();
            Assert.IsTrue(HomePage.PostFound(post), string.Format("The post \"{0}\" was not found", post));
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void FirstInternalLinksVersion_Works_And_IsValid()
        {
            string post = "An Argument against the Date Based Version Number";
            HomePage.Goto();
            HomePage.GoToPost(post);
            Assert.IsTrue(MSDeployPost.DoesInternalLinkWork("My New 3 Rules for Releases", "My New 3 Rules for Releases"));
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void SecondInternalLinksVersion_Works_And_IsValid()
        {
            string post = "An Argument against the Date Based Version Number";
            HomePage.Goto();
            HomePage.GoToPost(post);
            Assert.IsTrue(MSDeployPost.DoesInternalLinkWork("How I Use Chocolatey in my Releases", "How I Use Chocolatey in my Releases"));
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void Waterfall_Exits_And_Was_Deployed()
        {
            string post = "When is Waterfall a Good Choice";
            HomePage.Goto();
            HomePage.GoToPost(post);
            Assert.IsTrue(HomePage.PostFound(post), string.Format("The post \"{0}\" was not found", post));
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void Sending_Email_To_Developer_on_Failed_Build_Was_Deployed()
        {
            string post = "Sending an Email to the Developer when the Build Failed";
            HomePage.Goto();
            HomePage.GoToPost(post);
            Assert.IsTrue(HomePage.PostFound(post), string.Format("The post \"{0}\" was not found", post));
        }

        [TestMethod]
        [TestCategory("UITest")]
        public void One_Build_Definition_Was_Deployed()
        {
            string post = "One Build Definition to Support Multiple Branches";
            HomePage.Goto();
            HomePage.GoToPost(post);
            Assert.IsTrue(HomePage.PostFound(post), String.Format("The post \"{0}\" was not found", post));
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Close();
        }
    }
}
