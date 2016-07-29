using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWebWeWeave.BlogAutomation
{
    public class HomePage
    {
        public static string PostTitle { get; set; }
        public static int TagCount { get; set; }
        public static string TagName { get; set; }

        public static void Goto()
        {
            MenuSelector.Select("Home");
        }

        public static void FindTitle()
        {
            var postTitle = Driver.Instance.FindElements(By.ClassName("article-title"))[0];
            PostTitle = postTitle.Text;
        }

        public static void OpenFirstBlog()
        {
            var postTitle = Driver.Instance.FindElements(By.ClassName("article-title"))[0];
            postTitle.Click();
        }

        public static void GetTagCount()
        {
            var tags = Driver.Instance.FindElements(By.ClassName("article-tag-list-item"));
            TagCount = tags.Count;
        }

        public static void ClickFirstTag()
        {
            var tag = Driver.Instance.FindElements(By.ClassName("article-tag-list-item"))[0];
            TagName = tag.Text;
            tag.Click();
        }

        public static bool PostFound(string post)
        {
            var postTitles = Driver.Instance.FindElements(By.ClassName("article-title"));
            foreach (var title in postTitles)
            {
                if (title.Text == post)
                    return true;
            }
            return false;
        }
    }
}
