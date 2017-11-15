using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Onepiece.VideoCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            CollectFromFcOnePiece();

            //var driver = new FirefoxDriver
            //{
            //    Url = "http://www.linkvip.info/"
            //};

            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            //driver.FindElement(By.Name("url")).SendKeys("https://www.fshare.vn/file/7IB03VNMG7");
            //driver.FindElement(By.TagName("button")).Click();

            //var link = driver.FindElement(By.XPath("//h4/a")).GetAttribute("href");
        }

        private static void CollectFromFcOnePiece()
        {
            var downloadLinkData = "";

            var web = new HtmlWeb();
            var htmlDoc = web.Load("http://fconepiece.com/one-piece-vietsub/").DocumentNode;

            var driver = new FirefoxDriver { Url = "http://www.linkvip.info/" };
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            var containerNode = htmlDoc.SelectSingleNode(@"//div[@class=""entry-content""]");
            foreach (var childNode in containerNode.ChildNodes)
            {
                if (childNode.InnerText.Contains("Season"))
                {
                    foreach (var temp in childNode.ChildNodes)
                    {
                        var content = WebUtility.HtmlDecode(temp.InnerText).Trim(' ', '+', '-', '–', ':');
                        if (content.Contains("Season"))
                        {
                            var regex = new Regex("[0-9]+");
                            var seasonNo = int.Parse(regex.Match(content)?.Value);

                            // get download link
                            var linkNode = childNode.SelectSingleNode("a");
                            if (linkNode != null)
                            {
                                var link = linkNode.Attributes["href"].Value;
                                var linkWeb = new HtmlWeb();
                                var linkHtmlDoc = linkWeb.Load(link).DocumentNode;

                                string previousLink = null;
                                var epLinkNodes = linkHtmlDoc.SelectNodes(@"//a[@class=""filename""]");
                                foreach (var epLinkNode in epLinkNodes)
                                {
                                    driver.FindElement(By.Name("url")).Clear();
                                    driver.FindElement(By.Name("url")).SendKeys(epLinkNode.Attributes["href"].Value);
                                    driver.FindElement(By.TagName("button")).Click();
                                    var downloadLink = driver.FindElement(By.XPath("//h4/a")).GetAttribute("href");

                                    while (previousLink == downloadLink)
                                    {
                                        downloadLink = driver.FindElement(By.XPath("//h4/a")).GetAttribute("href");
                                    }
                                    
                                    previousLink = downloadLink;
                                    downloadLinkData += $"{seasonNo}_{epLinkNode.InnerText}_{downloadLink}" + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
            }

            File.WriteAllText("LinkData.txt", downloadLinkData);
        }
    }
}
