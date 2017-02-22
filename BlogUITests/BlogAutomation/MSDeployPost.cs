using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TheWebWeWeave.BlogAutomation
{
    public class MSDeployPost
    {
        public static bool DoesInternalLinkWork(string linkText, string pageTitle)
        {
            var link = Driver.Instance.FindElement(By.LinkText(linkText));
            link.Click();

            var postTitles = Driver.Instance.FindElements(By.ClassName("article-title"));
            foreach (var title in postTitles)
            {
                if (title.Text == pageTitle)
                    return true;
            }
            return false;
        }
    }
}
