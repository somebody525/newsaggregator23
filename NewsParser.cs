using System;
using System.Collections.Generic;

namespace NewsAggregator
{
    public interface NewsParser
    {
        public List<News> ExtractNews(String content);
    }
}
