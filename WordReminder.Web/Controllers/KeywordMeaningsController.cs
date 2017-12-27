using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WordReminder.Biz.Repositories;
using WordReminder.Biz.UnitOfWorks;
using WordReminder.Data.Model;

namespace WordReminder.Web.Controllers
{
    [Authorize]
    public class KeywordMeaningsController : Controller
    {
        private readonly IUnitOfWork uow;
        private IRepository<KeywordMeaning> keywordMeaningRepository;

        public KeywordMeaningsController(IUnitOfWork uow)
        {
            this.uow = uow;
            keywordMeaningRepository = uow.GetRepository<KeywordMeaning>();
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return NotFound();

            var keywordMeanings = await this.keywordMeaningRepository.FindByAsync(q => q.KeywordId == id);

            ViewBag.KeywordId = id.Value;
            return View(keywordMeanings);
        }

        public IActionResult Create(int? id)
        {
            if (id == null) return NotFound();

            KeywordMeaning model = new KeywordMeaning();
            model.KeywordId = id.Value;

            PopulateKeywordTypeDropDownList();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KeywordId,Word,KeywordType")]KeywordMeaning model)
        {
            if (ModelState.IsValid)
            {
                if (!KeywordMeaningExisist(model.Word))
                {
                    keywordMeaningRepository.Add(new KeywordMeaning
                    {
                        KeywordId = model.KeywordId,
                        KeywordType = model.KeywordType,
                        Word = model.Word
                    });
                    await uow.SaveChangesAsync();
                    return RedirectToAction("Index", "KeywordMeanings", new { id = model.KeywordId }); 
                }
                ModelState.AddModelError("Word", string.Format("{0} is exsist.", model.Word));
            }
            PopulateKeywordTypeDropDownList();
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var keywordMeaning = await keywordMeaningRepository.GetByIdAsync(id.Value);
            PopulateKeywordTypeDropDownList(keywordMeaning.KeywordType);
            return View(keywordMeaning);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(KeywordMeaning model)
        {
            if (ModelState.IsValid)
            {
                if (!KeywordMeaningExisist(model.Word,model.KeywordMeaningId))
                {
                    keywordMeaningRepository.Edit(model);
                    await uow.SaveChangesAsync();
                    return RedirectToAction("Index", "KeywordMeanings", new { id = model.KeywordId }); 
                }
                ModelState.AddModelError("Word", string.Format("{0} is exsist.", model.Word));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var keywordMeaning = await keywordMeaningRepository.GetByIdAsync(id.Value);
            if (keywordMeaning == null) return NotFound();

            return View(keywordMeaning);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var keywordMeaning = await keywordMeaningRepository.GetByIdAsync(id);
            keywordMeaningRepository.Delete(keywordMeaning);
            await uow.SaveChangesAsync();
            return RedirectToAction("Index", "KeywordMeanings", new { id = keywordMeaning.KeywordId });
        }

        private void PopulateKeywordTypeDropDownList(object selectedValue = null)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var keywordTypes = Enum.GetValues(typeof(KeywordType));
            foreach (KeywordType item in keywordTypes)
            {
                list.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(KeywordType), item),
                    Value = item.ToString()
                });
            }

            ViewBag.KeywordType = new SelectList(list, "Value", "Text", selectedValue);
        }
        private bool KeywordMeaningExisist(string word)
        {
            var keywordMeaning = keywordMeaningRepository.Get(q => q.Word.ToLower().Equals(word.ToLower()));
            return keywordMeaning != null ? true : false;
        }
        private bool KeywordMeaningExisist(string word, int id)
        {
            var keywordMeaning = keywordMeaningRepository.Get(q => q.Word.ToLower().Equals(word.ToLower()));
            if (keywordMeaning == null) return false;

            if (keywordMeaning.KeywordMeaningId != id)
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