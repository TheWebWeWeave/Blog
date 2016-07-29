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

        //public static string BaseAddress { get; set; }
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
            
            //get { return "http://lab-dev.westus.cloudapp.azure.com"; }

            // This is for local development testing but Hexo must be in Server mode
            //get { return "http://localhost:4000"; }

            // This is for testing against production, but only do this after the new code has been deployed.
            //get { return "http://donaldonsoftware.azurewebsites.net/"; }

            // Use this one to verify that the dns setting is still working as expected.
            //get { return "http://www.donaldonsoftware.com/"; }
        }

        public static void Initialize()
        {

            Instance = new FirefoxDriver(new FirefoxBinary() { Timeout = TimeSpan.FromMinutes(2) }, new FirefoxProfile());
            // BaseAddress = (ConfigurationManager.AppSettings["BaseAddress"].ToString() != "__baseAddress__" ? ConfigurationManager.AppSettings["BaseAddress"].ToString() : "http://localhost:4000" );
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
