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
        public KeywordsController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var repository = uow.GetRepository<Keyword>();
            await repository.GetAllAsync();
            return View();
        }
    }
}