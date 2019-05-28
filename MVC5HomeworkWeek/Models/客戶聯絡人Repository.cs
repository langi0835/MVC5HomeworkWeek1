using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data.Entity;

namespace MVC5HomeworkWeek.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
		public 客戶聯絡人 Find(int? id)
		{
			return Where(n => n.Id == id.Value).FirstOrDefault();
		}

		public override IQueryable<客戶聯絡人> All()
		{
			return Where(n => 1 == 1);
		}

		public override IQueryable<客戶聯絡人> Where(Expression<Func<客戶聯絡人, bool>> expression)
		{
			return base.Where(expression).Where(n => !n.是否已刪除);
		}

		public override IQueryable<客戶聯絡人> Include<TProperty>(Expression<Func<客戶聯絡人, TProperty>> path)
		{
			return base.Include(path).Where(n => !n.是否已刪除);
		}

		public IQueryable<客戶聯絡人> GetContacts(string searchString)
		{
			if (!string.IsNullOrEmpty(searchString))
			{
				return Where(n => n.姓名.Contains(searchString)).Include(n => n.客戶資料);
			}

			return Include(n => n.客戶資料);
		}

		public override void Delete(客戶聯絡人 entity)
		{
			entity.是否已刪除 = true;
			Update(entity);
		}

		public void BulkUpdate(IEnumerable<客戶聯絡人> contacts)
		{
			foreach (var data in contacts)
			{
				var contact = Find(data.Id);
				contact.職稱 = data.職稱;
				contact.手機 = data.手機;
				contact.電話 = data.電話;
				Update(contact);
			}

		}
	}

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}