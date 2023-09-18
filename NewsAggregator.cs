using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace NewsAggregator
{
    class NewsAggregator
    {
        private const string URLS = "urls.txt";

        int poolSize;
        List<String> urls;

        public NewsAggregator(int poolSize, List<string> urls)
        {
            this.poolSize = poolSize;
            this.urls = urls;
            ThreadPool.SetMaxThreads(poolSize, poolSize);
        }

        static int finishCount;

        public void Run()
        {
            finishCount = 0;
            int i = 0;
            foreach (string url in urls)
            {
                Console.WriteLine(url);
                ThreadPool.QueueUserWorkItem(new WaitCallback(Process),
                    new ProcParam("" + i++, url));
            }
            // wait until all threads are finished
            while (finishCount < urls.Count)
            {
                Thread.Sleep(10);
            };
        }

        class ProcParam
        {
            public ProcParam(string name, string url)
            {
                this.name = name;
                this.url = url;
            }
            public string name;
            public string url;
        }

        static async void Process(object procParam)
        {
            ProcParam param = (ProcParam)procParam;
            NewsAgent agent = new NewsAgent(param.name, param.url);
            await agent.Download();
            finishCount++;
        }

        static void Main(string[] args)
        {
            List<string> urls = new List<string>();
            using (StreamReader reader = new StreamReader(URLS))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    urls.Add(line);
                }
            }
            NewsAggregator aggregator = new NewsAggregator(6, urls);
            aggregator.Run();

            //Tester.MainTest(args);
        }
    }
}
