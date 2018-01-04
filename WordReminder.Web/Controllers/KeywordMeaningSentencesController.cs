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
        private readonly IRepository<KeywordMeaning> keywordMeaningRepository;
        public KeywordMeaningSentencesController(IUnitOfWork uow)
        {
            this.uow = uow;
            sentenceRepository = uow.GetRepository<KeywordMeaningSentence>();
            keywordMeaningRepository = uow.GetRepository<KeywordMeaning>();
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return NotFound();

            var meaning = await keywordMeaningRepository.GetByIdAsync(id.Value);
            var sentences = await sentenceRepository.FindByAsync(q => q.KeywordMeaningId == id.Value);

            ViewBag.KeywordMeaningId = meaning.KeywordId;
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
                if (!KeywordMeaningSentenceExisist(model.Sentence))
                {
                    sentenceRepository.Add(new KeywordMeaningSentence { KeywordMeaningId = model.KeywordMeaningId, Sentence = model.Sentence });

                    await uow.SaveChangesAsync();
                    return RedirectToAction("Index", new { id = model.KeywordMeaningId }); 
                }
                ModelState.AddModelError("Sentence", string.Format("{0} is exsist.", model.Sentence));
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
                if (!KeywordMeaningSentenceExisist(model.Sentence,model.KeywordMeaningSentenceId))
                {
                    sentenceRepository.Edit(model);
                    await uow.SaveChangesAsync();
                    return RedirectToAction("Index", new { id = model.KeywordMeaningId }); 
                }
                ModelState.AddModelError("Word", string.Format("{0} is exsist.", model.Sentence));
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
        private bool KeywordMeaningSentenceExisist(string sentence)
        {
            var meaningSentence = sentenceRepository.Get(q => q.Sentence.ToLower().Equals(sentence.ToLower()));
            return meaningSentence != null ? true : false;
        }
        private bool KeywordMeaningSentenceExisist(string sentence, int id)
        {
            var meaningSentence = sentenceRepository.Get(q => q.Sentence.ToLower().Equals(sentence.ToLower()));
            if (meaningSentence == null) return false;

            if (meaningSentence.KeywordMeaningSentenceId != id)
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