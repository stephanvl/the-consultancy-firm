﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheConsultancyFirm.Areas.Dashboard.ViewModels;
using TheConsultancyFirm.Models;
using TheConsultancyFirm.Repositories;
using TheConsultancyFirm.Services;

namespace TheConsultancyFirm.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class SolutionsController : Controller
    {
        private readonly ISolutionRepository _solutionRepository;
        private readonly IUploadService _uploadService;
        private readonly IItemTranslationRepository _itemTranslationRepository;

        public SolutionsController(ISolutionRepository solutionRepository, IUploadService uploadService, IItemTranslationRepository itemTranslationRepository)
        {
            _solutionRepository = solutionRepository;
            _uploadService = uploadService;
            _itemTranslationRepository = itemTranslationRepository;
        }

        // GET: Dashboard/Solutions
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page,
            bool showDisabled = false)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["LastModifiedSortParm"] = sortOrder == "LastModified" ? "last_desc" : "LastModified";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewBag.ShowDisabled = showDisabled;
            var solutions = _solutionRepository.GetAll().Where(s => !s.Deleted && (s.Enabled || showDisabled) && (string.IsNullOrEmpty(searchString) || s.Title.Contains(searchString)));

            switch (sortOrder)
            {
                case "name_desc":
                    solutions = solutions.OrderByDescending(c => c.Title);
                    break;
                case "Date":
                    solutions = solutions.OrderBy(c => c.Date);
                    break;
                case "date_desc":
                    solutions = solutions.OrderByDescending(c => c.Date);
                    break;
                case "LastModified":
                    solutions = solutions.OrderBy(c => c.LastModified);
                    break;
                case "last_desc":
                    solutions = solutions.OrderByDescending(c => c.LastModified);
                    break;
                default:
                    solutions = solutions.OrderBy(c => c.Title);
                    break;
            }

            return View(new SolutionViewModel
            {
                SolutionsList = await PaginatedList<Solution>.Create(solutions, page ?? 1, 5),
                SolutionsWithoutTranslation = await _itemTranslationRepository.GetSolutionsWithoutTranslation()
            });
        }

        // GET: Dashboard/Downloads/Deleted
        public async Task<IActionResult> Deleted()
        {
            return View(await _solutionRepository.GetAll().Where(s => s.Deleted).OrderByDescending(c => c.Date).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> TranslationChoice(int choice, int selectBox = 0)
        {
            if (choice == 0) return RedirectToAction("Create");
            if (selectBox == 0) return NotFound();
            var id = await _solutionRepository.CreateCopy(selectBox);
            return RedirectToAction("TranslationEdit", new { id = id });
        }

        public async Task<IActionResult> TranslationEdit(int id)
        {
            return View(await _solutionRepository.Get(id));
        }

        [HttpPost, ActionName("SaveTranslation")]
        [ValidateAntiForgeryToken]
        public async Task<ObjectResult> EditTranslationPost(int? id)
        {
            var solution = await _solutionRepository.Get(id ?? 0, true);

            if (solution == null) return new NotFoundObjectResult(null);

            // Bind POST variables Title, CustomerId, Image and TagIds to the model.
            await TryUpdateModelAsync(solution, string.Empty, n => n.Title, n => n.SharingDescription);

            if (!ModelState.IsValid) return new BadRequestObjectResult(ModelState);

            try
            {
                solution.LastModified = DateTime.UtcNow;
                await _solutionRepository.Update(solution);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SolutionExists(solution.Id))
                {
                    return new NotFoundObjectResult(null);
                }

                throw;
            }

            return new ObjectResult(solution.Id);
        }


        // GET: Dashboard/Solutions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solution = await _solutionRepository.Get((int)id,true);

            if (solution == null)
            {
                return NotFound();
            }

            return View(solution);
        }

        // GET: Dashboard/Solutions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Solutions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ObjectResult> Create([Bind("Title,Image, CustomerIds, TagIds, SharingDescription, Summary")] Solution solution)
        {
            if (solution.Image == null)
                ModelState.AddModelError(nameof(solution.Image), "The Image field is required.");
            else
            {
                if (!(new[] {".png", ".jpg", ".jpeg"}).Contains(Path.GetExtension(solution.Image.FileName)?.ToLower()))
                    ModelState.AddModelError(nameof(solution.Image),
                        "Invalid image type, only png and jpg images are allowed");

                if (solution.Image.Length < 1)
                    ModelState.AddModelError(nameof(solution.Image), "Filesize too small");
            }

            if (!ModelState.IsValid) return new BadRequestObjectResult(ModelState);

            if (solution.Image != null)
            {
                solution.PhotoPath = await _uploadService.Upload(solution.Image, "/images/uploads/solutions");
            }

            solution.SolutionTags = solution.TagIds?.Select(tagId => new SolutionTag { Solution = solution, TagId = tagId }).ToList();
            solution.CustomerSolutions = solution.CustomerIds?.Select(customerId =>
                new CustomerSolution {CustomerId = customerId, Solution = solution}).ToList();

            solution.LastModified = DateTime.UtcNow;
            solution.Date = DateTime.UtcNow;
            solution.Language = "nl";

            try
            {
                await _solutionRepository.Create(solution);
                return new ObjectResult(solution.Id);
            }
            catch (DbUpdateException)
            {
                return new ObjectResult(null)
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
        }

        // GET: Dashboard/Solutions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solution = await _solutionRepository.Get((int) id, true);

            if (solution == null)
            {
                return NotFound();
            }
            return View(solution);
        }

        // POST: Dashboard/Solutions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            var solution = await _solutionRepository.Get(id ?? 0, true);

            if (solution == null) return new NotFoundObjectResult(null);

            // Bind POST variables Title, CustomerId, Image and TagIds to the model.
            await TryUpdateModelAsync(solution, string.Empty, s => s.Title, s => s.CustomerIds, s => s.Image, s => s.TagIds, c => c.SharingDescription, c => c.Summary);

            if (solution.Image != null)
            {
                if (!(new[] { ".png", ".jpg", ".jpeg" }).Contains(Path.GetExtension(solution.Image.FileName)?.ToLower()))
                    ModelState.AddModelError(nameof(solution.Image), "Invalid image type, only png and jpg images are allowed");

                if (solution.Image.Length == 0)
                    ModelState.AddModelError(nameof(solution.Image), "Filesize too small");
            }

            if (!ModelState.IsValid) return new BadRequestObjectResult(ModelState);

            if (solution.Image != null)
            {
                if (solution.PhotoPath != null)
                    await _uploadService.Delete(solution.PhotoPath);
                solution.PhotoPath = await _uploadService.Upload(solution.Image, "/images/uploads/solutions");
            }

            solution.SolutionTags.RemoveAll(ct => !(solution.TagIds?.Contains(ct.TagId) ?? false));
            solution.SolutionTags.AddRange(solution.TagIds?.Except(solution.SolutionTags.Select(ct => ct.TagId))
                                        .Select(tagId => new SolutionTag { Solution = solution, TagId = tagId }) ?? new List<SolutionTag>());

            solution.CustomerSolutions.RemoveAll(ct => !(solution.CustomerIds?.Contains(ct.CustomerId) ?? false));
            solution.CustomerSolutions.AddRange(solution.CustomerIds?.Except(solution.CustomerSolutions.Select(ct => ct.CustomerId))
                                               .Select(customerId => new CustomerSolution { Solution = solution, CustomerId = customerId }) ?? new List<CustomerSolution>());

            try
            {
                solution.LastModified = DateTime.UtcNow;
                await _solutionRepository.Update(solution);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SolutionExists(solution.Id))
                {
                    return new NotFoundObjectResult(null);
                }

                throw;
            }

            return new ObjectResult(solution.Id);
        }

        // GET: Dashboard/Solutions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var solution = await _solutionRepository.Get((int)id, true);
            if (solution == null)
            {
                return NotFound();
            }

            return View(solution);
        }

        // POST: Dashboard/Solutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var solution = await _solutionRepository.Get(id ?? 0, true);
            if (solution == null)
            {
                return NotFound();
            }

            solution.Deleted = true;

            await _solutionRepository.Update(solution);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Restore(int? id)
        {
            var solution = await _solutionRepository.Get(id ?? 0);
            if (solution == null)
            {
                return NotFound();
            }

            solution.Deleted = false;

            await _solutionRepository.Update(solution);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ToggleEnable(int? id)
        {
            var solution = await _solutionRepository.Get(id ?? 0);
            if (solution == null)
            {
                return NotFound();
            }

            solution.Enabled = !solution.Enabled;

            await _solutionRepository.Update(solution);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SolutionExists(int id)
        {
            return await _solutionRepository.Get(id, true) == null;
        }
    }
}
