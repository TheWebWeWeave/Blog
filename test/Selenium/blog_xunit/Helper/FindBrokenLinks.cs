using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace blog_xunit.Helper
{
    internal class FindBrokenLinks
    {
        public async Task<int> TestForBrokenLinks(dynamic driver)
        {
            int broken_links = 0;
            using var client = new HttpClient();
            var link_list = driver.Driver.FindElementsByTagName("a");
            //var link_list = driver.FindElements(By.TagName("a"));

            /* Loop through all the links */
            foreach (var link in link_list)
            {
                try
                {                    
                    var uri = link.GetAttribute("href");
                    if (uri != null)
                        if (uri != "https://www.linkedin.com/in/donald-schulz-6b72833/")
                        {
                            HttpResponseMessage response = await client.GetAsync(uri);
                            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Moved)
                            {
                                System.Console.WriteLine($"Link at {link.GetAttribute("href")} is OK, status is {response.StatusCode}");
                            }
                            else
                            {
                                System.Console.WriteLine($"Link at {link.GetAttribute("href")} is Broken, status is {response.StatusCode}");
                                broken_links++;
                            }
                        }

                }
                catch (Exception ex)
                {
                    if ((ex is ArgumentNullException) || (ex is NotSupportedException))
                    {
                        System.Console.WriteLine("Exception occured\n");
                    }
                }
            }

            System.Threading.Thread.Sleep(2000);
            Console.WriteLine($"\nThe page {driver.Driver.Url} has {broken_links} broken links");
            //Console.WriteLine($"\nThe page {driver.Url} has {broken_links} broken links");
            return broken_links;
        }

        public async Task<int> TestForBrokenLinks(IWebDriver driver)
        {
            int broken_links = 0;
            using var client = new HttpClient();            
            var link_list = driver.FindElements(By.TagName("a"));

            /* Loop through all the links */
            foreach (var link in link_list)
            {
                try
                {
                    var uri = link.GetAttribute("href");
                    if (uri != null)
                        if (uri != "https://www.linkedin.com/in/donald-schulz-6b72833/")
                        {
                            HttpResponseMessage response = await client.GetAsync(uri);
                            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Moved)
                            {
                                System.Console.WriteLine($"Link at {link.GetAttribute("href")} is OK, status is {response.StatusCode}");
                            }
                            else
                            {
                                System.Console.WriteLine($"Link at {link.GetAttribute("href")} is Broken, status is {response.StatusCode}");
                                broken_links++;
                            }
                        }

                }
                catch (Exception ex)
                {
                    if ((ex is ArgumentNullException) || (ex is NotSupportedException))
                    {
                        System.Console.WriteLine("Exception occured\n");
                    }
                }
            }

            System.Threading.Thread.Sleep(2000);            
            Console.WriteLine($"\nThe page {driver.Url} has {broken_links} broken links");
            return broken_links;
        }
    }
}
