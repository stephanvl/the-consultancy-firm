﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheConsultancyFirm.Models;

namespace TheConsultancyFirm.Repositories
{
    public interface INewsItemRepository
    {
        Task<NewsItem> Get(int id, bool includeInactive = false);
        IQueryable<NewsItem> GetAll();
        Task<List<NewsItem>> GetHomepageItems(string language);
        Task Create(NewsItem newsItem);
        Task Update(NewsItem newsItem);
        Task Delete(int id);
        Task<NewsItem> GetLatest();
        Task<int> CreateCopy(int id);
    }
}
