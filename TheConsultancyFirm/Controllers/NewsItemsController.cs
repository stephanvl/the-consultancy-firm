﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheConsultancyFirm.Common;
using TheConsultancyFirm.Repositories;
using TheConsultancyFirm.Services;
using TheConsultancyFirm.ViewModels;

namespace TheConsultancyFirm.Controllers
{
    public class NewsItemsController : Controller
    {
        private readonly IRelatedItemsRepository _relatedItemsRepository;
        private readonly INewsItemRepository _newsItemRepository;

        public NewsItemsController(IRelatedItemsRepository relatedItemsRepository, INewsItemRepository newsItemRepository)
        {
            _relatedItemsRepository = relatedItemsRepository;
            _newsItemRepository = newsItemRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[controller]/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            // Parse everything till the first '-' as integer into `caseId`
            int.TryParse(id.Split('-', 2)[0], out int newsItemId);

            var newsItem = await _newsItemRepository.Get(newsItemId);
            if (newsItem == null) return NotFound();

            // Force the right slug
            if (id != newsItem.Slug)
                return RedirectToAction("Details", new { id = newsItem.Slug });

            var relatedItems = await _relatedItemsRepository.GetRelatedItems(newsItem.Id, Enumeration.ContentItemType.NewsItem);

            return View(new NewsItemDetailViewModel
            {
                NewsItem = newsItem,
                ContentItems = relatedItems,
            });
        }
    }
}