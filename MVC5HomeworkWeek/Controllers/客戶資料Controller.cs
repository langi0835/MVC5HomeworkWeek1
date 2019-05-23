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
using MVC5HomeworkWeek.Models;

namespace MVC5HomeworkWeek.Controllers
{
    public class 客戶資料Controller : Controller
    {
		readonly 客戶資料Repository repo;
		public 客戶資料Controller()
		{
			repo = RepositoryHelper.Get客戶資料Repository();
		}

        // GET: 客戶資料
        public ActionResult Index(string searchString)
        {
			List<客戶資料> customers;
			customers = repo.GetCustomers(searchString).ToList();
			
			return View(customers);
        }


        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var 客戶資料 = repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
				repo.Add(客戶資料);
				repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
				repo.Update(客戶資料);
				repo.UnitOfWork.Commit();

				return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo.Find(id);
            repo.Delete(客戶資料);
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
				var data = repo.All().Select(c => new { c.客戶名稱, c.電話, c.地址 });

				var ws = wb.Worksheets.Add("custdata", 1);
				ws.Cell(1, 1).InsertData(data);

				using (MemoryStream memoryStream = new MemoryStream())
				{
					wb.SaveAs(memoryStream);
					memoryStream.Seek(0, SeekOrigin.Begin);
					return File(memoryStream.ToArray(), "application/vnd.ms-excel", "customers.xlsx");
				}
			}
		}
    }
}
