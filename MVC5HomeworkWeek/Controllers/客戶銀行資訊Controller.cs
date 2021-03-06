﻿using System;
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
    public class 客戶銀行資訊Controller : Controller
    {
		readonly 客戶銀行資訊Repository repo;
		readonly 客戶資料Repository repoCustomer;

		public 客戶銀行資訊Controller()
		{
			repo = RepositoryHelper.Get客戶銀行資訊Repository();
			repoCustomer = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
		}

		// GET: 客戶銀行資訊
		public ActionResult Index(string searchString)
		{
			if (!string.IsNullOrEmpty(searchString))
			{
				return View(repo.Where(n => n.銀行名稱.Contains(searchString)).Include(n => n.客戶資料).ToList());
			}

			var banks = repo.Include(n => n.客戶資料).ToList();
			return View(banks);
		}

        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bank = repo.Find(id);
            if (bank == null)
            {
                return HttpNotFound();
            }
            return View(bank);
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶銀行資訊/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶銀行資訊);
				repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = repo.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 banksData)
        {
            if (ModelState.IsValid)
            {
				repo.Update(banksData);
				repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", banksData.客戶Id);
            return View(banksData);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶銀行資訊 = repo.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = repo.Find(id);
            repo.Delete(客戶銀行資訊);
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
				var data = repo.All().Select(c => new { c.銀行代碼, c.銀行名稱, c.帳戶名稱, c.帳戶號碼});

				var ws = wb.Worksheets.Add("custdata", 1);
				ws.Cell(1, 1).InsertData(data);

				using (MemoryStream memoryStream = new MemoryStream())
				{
					wb.SaveAs(memoryStream);
					memoryStream.Seek(0, SeekOrigin.Begin);
					return File(memoryStream.ToArray(), "application/vnd.ms-excel", "banks.xlsx");
				}
			}
		}
	}
}
