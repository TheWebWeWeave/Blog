using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWebWeWeave.BlogAutomation
{
    public class AboutPage
    {
        public static string PostTitle { get; set; }
        public static void Goto()
        {
            MenuSelector.Select("About");
        }

        public static void FindTitle()
        {
            var postTitle = Driver.Instance.FindElements(By.ClassName("article-title"))[0];
            PostTitle = postTitle.Text;
        }
    }
}
