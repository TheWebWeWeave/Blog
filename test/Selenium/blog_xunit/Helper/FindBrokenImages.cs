using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace blog_xunit.Helper
{
    internal class FindBrokenImages
    {
        public async Task<int> TestForBrokenImages(dynamic driver)
        {
            int broken_images = 0;
            using var client = new HttpClient();
            var image_list = driver.Driver.FindElementsByTagName("img");

            /* Loop through all the images */
            foreach (var img in image_list)
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(img.GetAttribute("src"));
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        System.Console.WriteLine($"Image at the link {img.GetAttribute("src")} is OK, status is {response.StatusCode}");
                    }
                    else
                    {
                        System.Console.WriteLine($"Image at the link {img.GetAttribute("src")} is Broken, status is {response.StatusCode}");
                        broken_images++;
                    }
                }
                catch (Exception ex)
                {
                    if ((ex is ArgumentException) || (ex is NotSupportedException))
                    {
                        System.Console.WriteLine("Exception occured\n");
                    }
                }
            }

            System.Threading.Thread.Sleep(2000);
            Console.WriteLine($"\nThe page {driver.Driver.Url} has {broken_images} broken images");
            return broken_images;
        }
    }
}
