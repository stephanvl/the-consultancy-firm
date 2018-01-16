﻿using System.Collections.Generic;
using TheConsultancyFirm.Models;

namespace TheConsultancyFirm.ViewModels
{
    public class HomeViewModel
    {
        public List<Customer> Customers { get; set; }
        public List<Solution> Solutions { get; set; }
        public List<NewsItem> NewsItems { get; set; }
    }
}
