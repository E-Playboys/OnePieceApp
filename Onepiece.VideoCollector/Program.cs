using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace Onepiece.VideoCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Collect (1) or Download (2): ");
            var action = Console.ReadLine();

            FirefoxProfile profile = new FirefoxProfile();
            profile.SetPreference("browser.download.dir", "D:\\Temp");  // folder
            profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "video/mp4,application/x-troff-msvideo,video/avi,video/msvideo,video/x-msvideo,application/zip,application/x-rar-compressed,application/octet-stream");  // MIME type
            profile.SetPreference("pdfjs.disabled", true);  // disable the built-in viewer
            profile.SetPreference("browser.download.folderList", 2);
            profile.SetPreference("browser.download.panel.shown", false);

            //DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
            //capabilities.SetCapability(FirefoxDriver.ProfileCapabilityName, profile);
            //capabilities.SetCapability(CapabilityType.ELEMENT_SCROLL_BEHAVIOR, 1);

            var driver = new FirefoxDriver(profile);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            if (action == "1")
            {
                CollectFromFcOnePiece(driver);
                CollectFromOnePieceFc(driver);
            }
            else if (action == "2")
            {
                Directory.CreateDirectory("Videos");

                DownloadVideo(driver);
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

                                //string previousLink = null;
                                var epLinkNodes = linkHtmlDoc.SelectNodes(@"//a[@class=""filename""]");
                                foreach (var epLinkNode in epLinkNodes)
                                {
                                    //driver.FindElement(By.Name("url")).Clear();
                                    //driver.FindElement(By.Name("url")).SendKeys(epLinkNode.Attributes["href"].Value);
                                    //driver.FindElement(By.TagName("button")).Click();
                                    //var downloadLink = driver.FindElement(By.XPath("//h4/a")).GetAttribute("href");

                                    //while (previousLink == downloadLink)
                                    //{
                                    //    downloadLink = driver.FindElement(By.XPath("//h4/a")).GetAttribute("href");
                                    //}

                                    //previousLink = downloadLink;
                                    //downloadLinkData += $"{seasonNo}_{epLinkNode.InnerText}_{downloadLink}" + Environment.NewLine;

                                    downloadLinkData += $"{seasonNo}_{epLinkNode.InnerText}_{epLinkNode.Attributes["href"].Value}" + Environment.NewLine;
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
                    //driver.FindElement(By.Name("url")).Clear();
                    //driver.FindElement(By.Name("url")).SendKeys(fshareLinkNode.Attributes["href"].Value);
                    //driver.FindElement(By.TagName("button")).Click();
                    //var downloadLink = driver.FindElement(By.XPath("//h4/a")).GetAttribute("href");

                    //// if there is no file in fshare then fallback to zing video
                    //if (!downloadLink.Contains("fshare.vn"))
                    //{
                    //    var iframeNode = htmlDoc.SelectSingleNode(@"//iframe");
                    //    if (iframeNode == null)
                    //    {
                    //        continue;
                    //    }

                    //    var iframeDoc = web.Load(iframeNode.Attributes["src"].Value).DocumentNode;
                    //    var videoNode = iframeDoc.SelectSingleNode(@"//video/source");

                    //    downloadLink = videoNode.Attributes["src"].Value;
                    //}

                    //downloadLinkData += $"1_{i}_{downloadLink}" + Environment.NewLine;

                    downloadLinkData += $"1_{i}_{fshareLinkNode.Attributes["href"].Value}" + Environment.NewLine;
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

        private static void DownloadVideo(FirefoxDriver driver)
        {
            driver.Url = "https://www.fshare.vn/";
            driver.FindElementsById("LoginForm_email")[1].Clear();
            driver.FindElementsById("LoginForm_email")[1].SendKeys("troyuit@gmail.com");
            driver.FindElementsById("LoginForm_password")[1].Clear();
            driver.FindElementsById("LoginForm_password")[1].SendKeys("thuyphuong");
            driver.FindElementByName("yt0").Click();

            Thread.Sleep(TimeSpan.FromSeconds(10));

            // data 1
            var linkData = File.ReadAllLines("LinkData.txt").Concat(File.ReadAllLines("LinkData_2.txt")).ToList();
            var fshareLinks = linkData.Where(x => x.Contains("fshare")).ToList();
            var zingLinks = linkData.Where(x => !x.Contains("fshare")).ToList();

            // fshare
            var totalCount = fshareLinks.Count();
            var pageSize = 2; // firefox limit
            var pageCount = (totalCount + pageSize - 1) / pageSize;

            for (int i = 0; i <= pageCount; i++)
            {
                var links = fshareLinks.Skip(i * pageCount).Take(pageSize);
                foreach (var link in links)
                {
                    var downloadLink = link.Split('_')[2];
                    driver.Url = downloadLink;
                    driver.FindElementByClassName("btn-download-vip").Click();
                }

                // wait for download completes
                Thread.Sleep(TimeSpan.FromMinutes(10));
            }

            // zing
            var tasks = new List<Task>();
            totalCount = zingLinks.Count();
            pageSize = 5;
            pageCount = (totalCount + pageSize - 1) / pageSize;

            for (int i = 0; i <= pageCount; i++)
            {
                var links = zingLinks.Skip(i * pageCount).Take(pageSize);
                foreach (var link in links)
                {
                    var downloadLink = link.Split('_')[2];
                    var task = Task.Run(() =>
                    {
                        using (var wc = new WebClient())
                        {
                            wc.DownloadFile(new Uri(downloadLink), $@"Videos\{link.Split('_')[0]}_{link.Split('_')[1]}.{link.Split('.').Last()}");
                        }
                    });

                    tasks.Add(task);
                }

                Task.WaitAll(tasks.ToArray());
            }
        }
    }
}
