using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWebWeWeave.BlogAutomation
{
    public class Driver
    {
        public static IWebDriver Instance { get; set; }

        /// <summary>
        /// Only one of these
        /// </summary>
        public static string BaseAddress
        {
            get {
                string webSite = ConfigurationManager.AppSettings["BaseAddress"];
                string result = (webSite != "__baseAddress__" ? webSite : "http://localhost:4000");               
                return  result;
            }
            
        }

        public static void Initialize()
        {

            Instance = new FirefoxDriver(new FirefoxBinary() { Timeout = TimeSpan.FromMinutes(2) }, new FirefoxProfile());
            TurnOnWait();
        }

        public static void Close()
        {
            Instance.Close();
        }

        private static void TurnOnWait()
        {
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        private static void TurnOffWait()
        {
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(0));
        }
    }
}
