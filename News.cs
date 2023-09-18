﻿using System;
using System.Collections.Generic;

namespace NewsAggregator
{
    public class News
    {

        public String company;
        public DateTime date;
        public String title;
        public String summary;
        public String link;
        public String content;
        public HashSet<String> keywords;

        public String ToString()
        {
            return "News(" + company + ", " + date + ", " + title + ", " + link + ", " + keywords + ")";
        }
    }
}
