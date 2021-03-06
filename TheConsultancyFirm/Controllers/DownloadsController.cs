﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheConsultancyFirm.Models;
using TheConsultancyFirm.Repositories;
using TheConsultancyFirm.ViewModels;

namespace TheConsultancyFirm.Controllers
{
    public class DownloadsController : Controller
    {
        private readonly IDownloadRepository _downloadRepository;
        private readonly IDownloadLogRepository _downloadLogRepository;
        private readonly IItemTranslationRepository _itemTranslationRepository;

        public DownloadsController(IDownloadRepository downloadRepository, IDownloadLogRepository downloadLogRepository, IItemTranslationRepository itemTranslationRepository)
        {
            _downloadRepository = downloadRepository;
            _downloadLogRepository = downloadLogRepository;
            _itemTranslationRepository = itemTranslationRepository;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var language = HttpContext?.Features?.Get<IRequestCultureFeature>()?.RequestCulture?.Culture
                               ?.TwoLetterISOLanguageName ?? "nl";
            var viewModel = new DownloadsViewModel
            {
                MostDownloaded = await _downloadRepository.GetAll().Where(d => d.Enabled && !d.Deleted && d.Language == language).OrderByDescending(d => d.AmountOfDownloads)
                    .FirstOrDefaultAsync(),
                MostRecent = await _downloadRepository.GetAll().Where(d => d.Enabled && !d.Deleted && d.Language == language).OrderByDescending(d => d.Date).FirstOrDefaultAsync()
            };
            var all = _downloadRepository.GetAll().Where(d => d.Id != viewModel.MostDownloaded.Id && d.Enabled && !d.Deleted && d.Language == language)
                .OrderBy(d => d.Date);
            viewModel.AllDownloads = await PaginatedList<Download>.Create(all, page ?? 1, 10);
            return View(viewModel);
        }

        [HttpGet("[controller]/{id}")]
        public async Task<IActionResult> Details(int id, int? page)
        {
            var selected = await _downloadRepository.Get(id);
            if (selected.Deleted || !selected.Enabled) return NotFound();

            var language = HttpContext?.Features?.Get<IRequestCultureFeature>()?.RequestCulture?.Culture
                               ?.TwoLetterISOLanguageName ?? "nl";

            if (selected.Language != language)
            {
                int itemTranslationId;
                itemTranslationId = language == "nl" ?
                    (await _itemTranslationRepository.GetAllDownloads()).FirstOrDefault(d => d.IdEn == selected.Id).IdNl :
                    (await _itemTranslationRepository.GetAllDownloads()).FirstOrDefault(d => d.IdNl == selected.Id).IdEn;
                return RedirectToAction("Details", new { Id = itemTranslationId});
            }
            var AllDownloads = _downloadRepository.GetAll().Where(d => d.Id != id && d.Enabled && !d.Deleted);

            var viewModel = new DownloadsViewModel
            {
                Selected = selected,
                AllDownloads = await PaginatedList<Download>.Create(AllDownloads.AsQueryable(), page ?? 1, 10)
            };

            return View("/Views/Downloads/Index.cshtml", viewModel);
        }

        [Route("api/[controller]/[action]/{id}")]
        public async Task LogDownload(int id)
        {
            var downloadLog = new DownloadLog
            {
                Date = DateTime.UtcNow,
                DownloadId = id
            };

            await _downloadLogRepository.Log(downloadLog);
        }
    }
}
