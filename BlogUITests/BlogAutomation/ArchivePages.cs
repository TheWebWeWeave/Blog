using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TheWebWeWeave.BlogAutomation
{
    public class ArchivePages
    {
        public static bool IsPostInArchive(string title)
        {
            var blocks = Driver.Instance.FindElements(By.ClassName("archive-article-title"));
            foreach(var block in blocks)
            {
                if (block.Text == title)
                    return true;
            }
            return false;
        }
    }
}
