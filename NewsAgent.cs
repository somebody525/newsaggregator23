using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NewsAggregator
{
    class NewsAgent
    {
        private string name;
        private string url;
        private NewsParser parser;

        public NewsAgent(string name, string url)
        {
            this.name = name;
            this.url = url;
            if (url.StartsWith("https://data.gmanetwork.com/"))
                parser = new NewsParserGMA();
        }

        internal async System.Threading.Tasks.Task Download()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            string content = await client.GetStringAsync(url);
            //if (parser != null)
            //    newsList = parser.extractNews(content);
            //else
            //    newsList = new ArrayList<>();
            List<News> newsList = null;
            if (parser != null)
            {
                newsList = parser.ExtractNews(content);
                foreach (News news in newsList)
                {
                    Console.WriteLine(news.ToString());
                }
            }
            else
            {
                Console.WriteLine("TODO");
            }
        }
    }
}
