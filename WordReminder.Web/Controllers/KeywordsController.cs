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
            var list = await keywordRepository.GetAllAsync();
            return View(list);
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
                if (!KeywordExisist(model.Word))
                {
                    keywordRepository.Add(model);
                    await uow.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Word", string.Format("{0} is exsist.", model.Word));
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
                if (!KeywordExisist(model.Word, model.KeywordId))
                {
                    keywordRepository.Edit(model);
                    await uow.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Word", string.Format("{0} is exsist.", model.Word));
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

        private bool KeywordExisist(string word)
        {
            var keyword = keywordRepository.Get(q => q.Word.ToLower().Equals(word.ToLower()));
            return keyword != null ? true : false;
        }
        private bool KeywordExisist(string word, int id)
        {
            var keyword = keywordRepository.Get(q => q.Word.ToLower().Equals(word.ToLower()));
            if (keyword == null) return false;

            if (keyword.KeywordId != id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}