using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using HtmlAgilityPack;
using OnePiece.Web.Data.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WatiN.Core;

namespace Onepiece.DataCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            CollectSeasons();
        }

        private static void CollectSeasons()
        {
            var seasons = new List<Season>();
            var videoSeasons = new List<VideoSeason>();

            CollectFromWiki(seasons, "https://en.wikipedia.org/wiki/List_of_One_Piece_episodes_(seasons_1–8)");
            CollectFromWiki(seasons, "https://en.wikipedia.org/wiki/List_of_One_Piece_episodes_(seasons_9-14)");
            CollectFromWiki(seasons, "https://en.wikipedia.org/wiki/List_of_One_Piece_episodes_(seasons_15–current)");

            CollectFromFcOnePiece(seasons, videoSeasons);
        }

        private static void CollectFromWiki(List<Season> seasons, string url)
        {
            var web = new HtmlWeb();
            var htmlDoc = web.Load(url).DocumentNode;

            var wikiTables = htmlDoc.SelectNodes(@"//table[contains(@class,'wikitable')]");
            foreach (var table in wikiTables)
            {
                // TV specials
                var tableTitle = table.SelectNodes(@"preceding-sibling::h2").Last().InnerText;
                if (tableTitle.Contains("TV specials"))
                {
                    var season = seasons.Find(x => x.No == -1);
                    if (season == null)
                    {
                        season = new Season
                        {
                            No = -1,
                            TitleEng = "TV specials & Videos",
                            Episodes = new List<Anime>()
                        };

                        seasons.Add(season);
                    }

                    var epRows = table.SelectNodes(@"tr[@class=""vevent""]");
                    foreach (var epRow in epRows)
                    {
                        // find the ep no
                        int.TryParse(epRow.ChildNodes[1].InnerText.Trim().Replace("SP", ""), out var epNo);
                        if (epNo == 0)
                        {
                            continue;
                        }

                        var epTitle = epRow.ChildNodes[3].FirstChild.InnerText.Trim('"');

                        var episode = new Anime
                        {
                            No = epNo,
                            TitleEng = epTitle,
                            Type = AnimeType.TvSpecial
                        };

                        season.Episodes.Add(episode);
                    }

                    continue;
                }

                // Movies
                if (tableTitle.Contains("Original video animations"))
                {
                    var season = seasons.Find(x => x.No == -1);

                    var epRows = table.SelectNodes(@"tr[@class=""vevent""]");
                    foreach (var epRow in epRows)
                    {
                        // find the ep no
                        int.TryParse(epRow.ChildNodes[1].InnerText.Trim(), out var epNo);
                        if (epNo == 0)
                        {
                            continue;
                        }

                        var epTitle = epRow.ChildNodes[3].FirstChild.InnerText.Trim('"');

                        var episode = new Anime
                        {
                            No = epNo,
                            TitleEng = epTitle,
                            Type = AnimeType.Movie
                        };

                        season.Episodes.Add(episode);
                    }

                    continue;
                }

                // Seasons
                tableTitle = table.SelectNodes(@"preceding-sibling::h3").Last().ChildNodes[1].InnerText;
                if (tableTitle.Contains("Season"))
                {
                    // find the season no
                    var regex = new Regex(@" [0-9]+ ");
                    var seasonNo = int.Parse(regex.Match(tableTitle).Value.Trim());

                    var season = new Season
                    {
                        No = seasonNo,
                        TitleEng = tableTitle,
                        Episodes = new List<Anime>()
                    };

                    seasons.Add(season);

                    var epRows = table.SelectNodes(@"tr[@class=""vevent""]");
                    foreach (var epRow in epRows)
                    {
                        // find the ep no
                        int.TryParse(epRow.ChildNodes[1].InnerText.Trim(), out var epNo);
                        if (epNo == 0)
                        {
                            continue;
                        }

                        var epTitle = seasonNo < 6 ? epRow.ChildNodes[5].FirstChild.InnerText.Trim('"')
                            : epRow.ChildNodes[3].FirstChild.InnerText.Trim('"');

                        var episode = new Anime
                        {
                            No = epNo,
                            TitleEng = epTitle
                        };

                        season.Episodes.Add(episode);
                    }
                }
            }
        }

        private static void CollectFromFcOnePiece(List<Season> seasons, List<VideoSeason> videoSeasons)
        {
            var web = new HtmlWeb();
            var htmlDoc = web.Load("http://fconepiece.com/one-piece-vietsub/").DocumentNode;

            var containerNode = htmlDoc.SelectSingleNode(@"//div[@class=""entry-content""]");
            Season currentSeason = null;
            foreach (var childNode in containerNode.ChildNodes)
            {
                if (childNode.InnerText.Contains("Season"))
                {
                    // find the season title
                    foreach (var temp in childNode.ChildNodes)
                    {
                        var content = WebUtility.HtmlDecode(temp.InnerText).Trim(' ', '+', '-', '–', ':');
                        if (content.Contains("Season"))
                        {
                            // remove the (xxx - zzz) in the season title
                            var regex = new Regex(@"\(.+\)");
                            content = regex.Replace(content, "").Trim();

                            // find the season no
                            regex = new Regex("[0-9]+");
                            var seasonNo = int.Parse(regex.Match(content)?.Value);

                            currentSeason = seasons.Find(x => x.No == seasonNo);
                            currentSeason.Title = content;
                        }
                    }
                }
                else
                {
                    if (currentSeason != null && childNode.FirstChild != null)
                    {
                        // the ep title
                        var content = WebUtility.HtmlDecode(childNode.FirstChild.InnerText).Trim(' ', '+', '-', '–', ':');

                        // remove the (xxx - zzz) in the ep title
                        var regex = new Regex(@"\(.+\)");
                        var title = regex.Replace(content, "").Trim();

                        // find the ep no
                        regex = new Regex(@"[0-9]+");
                        var titleNos = regex.Matches(content).ToList();
                        if (titleNos.Count >= 2)
                        {
                            titleNos = titleNos.TakeLast(2).ToList();

                            var fromEp = int.Parse(titleNos[0].Value);
                            var toEp = int.Parse(titleNos[1].Value);

                            var titlePart = 1;
                            for (int i = fromEp; i <= toEp; i++)
                            {
                                var ep = currentSeason.Episodes.Find(x => x.No == i);
                                if (ep == null)
                                {
                                    continue;
                                }

                                ep.Title = $"{title} - Part {titlePart++}";
                            }
                        }
                        else if (titleNos.Count == 1)
                        {
                            var epNo = int.Parse(titleNos[0].Value);
                            var ep = currentSeason.Episodes.Find(x => x.No == epNo);
                            if (ep == null)
                            {
                                continue;
                            }

                            ep.Title = $"{title} {epNo}";
                        }
                    }
                }
            }
        }

        private static void CollectFromFshare(List<VideoSeason> videoSeasons, string url, int seasonNo)
        {
            var videoSeason = new VideoSeason {No = seasonNo};

            var web = new HtmlWeb();
            var htmlDoc = web.Load(url).DocumentNode;

            var linkNodes = htmlDoc.SelectNodes(@"//a[@class=""filename""]");
            foreach (var node in linkNodes)
            {
                var link = node.Attributes["href"].Value;

                var ie = new IE("http://www.linkvip.info/");
                ie.TextField(Find.ByName("url")).TypeText(link);
                ie.Button(Find.By("type", x => x == "submit")).Click();
            }

            videoSeasons.Add(videoSeason);
        }

        class VideoSeason
        {
            public int No { get; set; }

            public List<VideoEp> Eps { get; set; } = new List<VideoEp>();
        }

        class VideoEp
        {
            public int No { get; set; }

            public string Url { get; set; }
        }
    }
}
