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
            Console.Write("Collect (1) or Download (2): ");
            var action = Console.ReadLine();

            if (action == "1")
            {
                var driver = new FirefoxDriver { Url = "http://www.linkvip.info/" };
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

                //CollectFromFcOnePiece(driver);
                CollectFromOnePieceFc(driver);
            }
            else if (action == "2")
            {
                Directory.CreateDirectory("Videos");

                DownloadVideo();
            }
        }

        private static void CollectFromFcOnePiece(FirefoxDriver driver)
        {
            var downloadLinkData = "";

            var web = new HtmlWeb();
            var htmlDoc = web.Load("http://fconepiece.com/one-piece-vietsub/").DocumentNode;

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

        private static void CollectFromOnePieceFc(FirefoxDriver driver)
        {
            var downloadLinkData = "";

            var web = new HtmlWeb();
            for (int i = 575; i <= 813; i++)
            {
                var htmlDoc = web.Load($"http://onepiecefc.com/vi/one-piece-anime/episode-{i}/").DocumentNode;

                var fshareLinkNode = htmlDoc.SelectSingleNode(@"//a[contains(@title,'Fshare')]");
                if (fshareLinkNode != null)
                {
                    driver.FindElement(By.Name("url")).Clear();
                    driver.FindElement(By.Name("url")).SendKeys(fshareLinkNode.Attributes["href"].Value);
                    driver.FindElement(By.TagName("button")).Click();
                    var downloadLink = driver.FindElement(By.XPath("//h4/a")).GetAttribute("href");

                    // if there is no file in fshare then fallback to zing video
                    if (!downloadLink.Contains("fshare.vn"))
                    {
                        var iframeNode = htmlDoc.SelectSingleNode(@"//iframe");
                        if (iframeNode == null)
                        {
                            continue;
                        }

                        var iframeDoc = web.Load(iframeNode.Attributes["src"].Value).DocumentNode;
                        var videoNode = iframeDoc.SelectSingleNode(@"//video/source");

                        downloadLink = videoNode.Attributes["src"].Value;
                    }

                    downloadLinkData += $"1_{i}_{downloadLink}" + Environment.NewLine;
                }
                else
                {
                    var iframeNode = htmlDoc.SelectSingleNode(@"//iframe");
                    if (iframeNode == null)
                    {
                        continue;
                    }

                    var iframeDoc = web.Load(iframeNode.Attributes["src"].Value).DocumentNode;
                    var videoNode = iframeDoc.SelectSingleNode(@"//video/source");

                    downloadLinkData += $"1_{i}_{videoNode.Attributes["src"].Value}" + Environment.NewLine;
                }
            }

            File.WriteAllText("LinkData_2.txt", downloadLinkData);
        }

        private static void DownloadVideo()
        {
            // data 1
            var tasks = new List<Task>();
            var linkData = File.ReadAllLines("LinkData.txt");
            foreach (var linkInfo in linkData)
            {
                var downloadLink = linkInfo.Split('_')[2];

                var task = Task.Run(() =>
                {
                    using (WebClient wc = new WebClient())
                    {
                        wc.DownloadFile(new Uri(downloadLink), $@"Videos\{linkInfo.Split('_')[0]}_{linkInfo.Split('_')[1]}");
                    }
                });

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            // data 2
            tasks = new List<Task>();
            linkData = File.ReadAllLines("LinkData_2.txt");
            foreach (var linkInfo in linkData)
            {
                // skip if there is no video to download
                if (linkInfo.Length < 10)
                {
                    continue;
                }

                var downloadLink = linkInfo.Split('_')[2];

                var task = Task.Run(() =>
                {
                    using (WebClient wc = new WebClient())
                    {
                        wc.DownloadFile(new Uri(downloadLink), $@"Videos\{linkInfo.Split('_')[0]}_{linkInfo.Split('_')[1]}.{linkInfo.Split('.').Last()}");
                    }
                });

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
