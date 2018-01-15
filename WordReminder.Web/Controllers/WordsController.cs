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
    public class WordsController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<Keyword> keywordRepository;
        public WordsController(IUnitOfWork uow)
        {
            this.uow = uow;
            keywordRepository = this.uow.GetRepository<Keyword>();
        }
        
        public IActionResult Index()
        {
            var words=keywordRepository.GetAll();
            return View();
        }
    }
}