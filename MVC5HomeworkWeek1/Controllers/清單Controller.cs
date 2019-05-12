using MVC5HomeworkWeek1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5HomeworkWeek1.Controllers
{
    public class 清單Controller : Controller
    {
        // GET: 清單
        public ActionResult Index()
        {
			var repo = RepositoryHelper.Get客戶清單Repository();			

			return View(repo.All().ToList());
        }
		
    }
}