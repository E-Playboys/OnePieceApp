using HtmlAgilityPack;
using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Linq;

namespace Onepiece.DataCollector
{
    public class MangaCollector
    {
        public static void Collect()
        {
            Account account = new Account(
                                  "onepiecewiki",
                                  "743237792715878",
                                  "zhPCn8CYxJ5dK1BtVH6iHVyi-N8");

            Cloudinary cloudinary = new Cloudinary(account);

            var web = new HtmlWeb();
            var htmlDoc = web.Load("https://truyenqq.com/truyen-tranh/dao-hai-tac-128.html").DocumentNode;

            var infos = htmlDoc.SelectNodes(@"//div[contains(@class,'info_text_dt')]/div/p/a");
            foreach(var info in infos)
            {
                var link = info.Attributes["href"].Value;
                var chapter = link.Split(".html")[0].Split("-").Last();
                Console.WriteLine(link);
                var doc = web.Load(link).DocumentNode;
                var images = doc.SelectNodes(@"//div[contains(@class,'dtl_img')]/div/img");
                int i = 0;
                foreach(var image in images)
                {
                    i++;
                    var src = image.Attributes["src"].Value;
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(src),
                        PublicId = $"Manga/{chapter}/m-{chapter}-{i}"
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    Console.WriteLine(uploadResult.Uri.AbsolutePath);
                }
            }
        }
    }
}
