using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWebWeWeave.BlogAutomation
{
    public class MenuSelector
    {
        public static void Select(string menuLinkText)
        {
            Driver.Instance.Navigate().GoToUrl(Driver.BaseAddress);
            Driver.Instance.FindElement(By.LinkText(menuLinkText)).Click();
        }
    }
}
