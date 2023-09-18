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

        public NewsAgent(string name, string url)
        {
            this.name = name;
            this.url = url;
        }

        internal async System.Threading.Tasks.Task Download()
        {
            using var client = new HttpClient();
            string content = await client.GetStringAsync(url);
            Console.WriteLine(content);
        }
    }
}
