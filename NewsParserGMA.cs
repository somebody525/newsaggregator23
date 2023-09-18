using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewsAggregator
{
    class NewsParserGMA : NewsParser
    {

        public List<News> ExtractNews(String content)
        {
            List<News> list = new List<News>();

            int start = content.IndexOf("<item>");
            content = content.Substring(start);
            //System.out.println(content);
            Regex titlePat = new Regex("<title><!\\[CDATA\\[(.+?)]]></title>.*?<link>(.+?)</link>.*?<pubDate>(.+?)</pubDate>", RegexOptions.Singleline);
            MatchCollection matches = titlePat.Matches(content);
            foreach (Match match in matches)
            {
                News news = new News();
                news.company = "GMANews";
                news.title = match.Groups[1].Value;
                news.link = match.Groups[2].Value;
                try
                {
                    news.date = DateTime.ParseExact(match.Groups[3].Value, "ddd, dd MMM yyyy HH:mm:ss zzz", null);
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.StackTrace);
                }
                list.Add(news);
                //System.out.println(news);
            }
            return list;
        }
    }
}
