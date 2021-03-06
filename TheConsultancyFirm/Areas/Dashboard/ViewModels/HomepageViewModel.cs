﻿using System.Collections.Generic;
using TheConsultancyFirm.Models;

namespace TheConsultancyFirm.Areas.Dashboard.ViewModels
{
    public class HomepageViewModel
    {
        public List<Case> Cases { get; set; }
        public List<Solution> Solutions { get; set; }
        public CarouselBlock CarouselBlock { get; set; }
        public List<NewsItem> NewsItems { get; set; }
    }
}
