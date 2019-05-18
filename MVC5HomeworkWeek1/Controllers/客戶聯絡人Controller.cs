using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using MVC5HomeworkWeek1.Models;

namespace MVC5HomeworkWeek1.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
		readonly 客戶聯絡人Repository repo;
		readonly 客戶資料Repository repoCustomer;

		public 客戶聯絡人Controller()
		{
			repo = RepositoryHelper.Get客戶聯絡人Repository();
			repoCustomer = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
		}

		// GET: 客戶聯絡人
		public ActionResult Index(string searchString)
        {
			if (!string.IsNullOrEmpty(searchString))
			{
				return View(repo.Where(n => n.姓名.Contains(searchString)).Include(n => n.客戶資料).ToList());
			}

			var contacts = repo.Include(n => n.客戶資料).ToList();
            return View(contacts);
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var 客戶聯絡人 = repo.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }

            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶聯絡人);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var 客戶聯絡人 = repo.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
				repo.Update(客戶聯絡人);
				repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = repo.Find(id);
			repo.Delete(客戶聯絡人);
			repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }

		public ActionResult Export()
		{
			using (XLWorkbook wb = new XLWorkbook())
			{
				var data = repo.All().Select(c => new { c.姓名, c.職稱, c.電話, c.手機 });

				var ws = wb.Worksheets.Add("custdata", 1);
				ws.Cell(1, 1).InsertData(data);

				using (MemoryStream memoryStream = new MemoryStream())
				{
					wb.SaveAs(memoryStream);
					memoryStream.Seek(0, SeekOrigin.Begin);
					return File(memoryStream.ToArray(), "application/vnd.ms-excel", "contact.xlsx");
				}
			}
		}
	}
}
