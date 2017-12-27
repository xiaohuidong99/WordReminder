using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WordReminder.Biz.UnitOfWorks;
using WordReminder.Data.Model;
using WordReminder.Biz.Repositories;

namespace WordReminder.Web.Controllers
{
    [Authorize]
    public class KeywordMeaningSentencesController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<KeywordMeaningSentence> sentenceRepository;
        public KeywordMeaningSentencesController(IUnitOfWork uow)
        {
            this.uow = uow;
            sentenceRepository = uow.GetRepository<KeywordMeaningSentence>();
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return NotFound();

            var sentences = await sentenceRepository.FindByAsync(q => q.KeywordMeaningId == id.Value);

            ViewBag.KeywordMeaningId = id.Value;
            return View(sentences);
        }

        public IActionResult Create(int? id)
        {
            if (id == null) return NotFound();

            KeywordMeaningSentence model = new KeywordMeaningSentence();
            model.KeywordMeaningId = id.Value;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("KeywordMeaningId,Sentence")]KeywordMeaningSentence model)
        {
            if (ModelState.IsValid)
            {
                sentenceRepository.Add(new KeywordMeaningSentence { KeywordMeaningId = model.KeywordMeaningId, Sentence = model.Sentence });

                await uow.SaveChangesAsync();
                return RedirectToAction("Index", new { id = model.KeywordMeaningId });
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sentence = await sentenceRepository.GetByIdAsync(id.Value);
            if (sentence == null) return NotFound();

            return View(sentence);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("KeywordMeaningId,KeywordMeaningSentenceId,Sentence")]KeywordMeaningSentence model)
        {
            if (ModelState.IsValid)
            {
                sentenceRepository.Edit(model);
                await uow.SaveChangesAsync();
                return RedirectToAction("Index", new { id = model.KeywordMeaningId });
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var sentence = await sentenceRepository.GetByIdAsync(id.Value);
            if (sentence == null) return NotFound();

            return View(sentence);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var sentence = await sentenceRepository.GetByIdAsync(id);
            sentenceRepository.Delete(sentence);
            await uow.SaveChangesAsync();
            return RedirectToAction("Index", new { id = sentence.KeywordMeaningId });
        }
    }
}