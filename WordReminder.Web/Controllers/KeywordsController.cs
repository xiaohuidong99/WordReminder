using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordReminder.Biz.Repositories;
using WordReminder.Biz.UnitOfWorks;
using WordReminder.Data.Model;

namespace WordReminder.Web.Controllers
{
    [Authorize]
    public class KeywordsController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<Keyword> keywordRepository;
        public KeywordsController(IUnitOfWork uow)
        {
            this.uow = uow;
            keywordRepository = uow.GetRepository<Keyword>();
        }

        public async Task<IActionResult> Index()
        {
            return View(await keywordRepository.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Word")]Keyword model)
        {
            if (ModelState.IsValid)
            {
                keywordRepository.Add(model);
                await uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var keyword = await keywordRepository.GetByIdAsync(id.Value);
            if (keyword == null) return NotFound();

            return View(keyword);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("KeywordId,Word")]Keyword model)
        {
            if (ModelState.IsValid)
            {
                keywordRepository.Edit(model);
                await uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var keyword = await keywordRepository.GetByIdAsync(id.Value);
            if (keyword == null) return NotFound();

            return View(keyword);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var keyword = await keywordRepository.GetByIdAsync(id);
            keywordRepository.Delete(keyword);
            await uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}