﻿namespace TheConsultancyFirm.Models
{
    public class NewsItemTag
    {
        public int NewsItemId { get; set; }
        public NewsItem NewsItem { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
