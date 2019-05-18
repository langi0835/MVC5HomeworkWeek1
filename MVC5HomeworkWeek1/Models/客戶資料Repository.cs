using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MVC5HomeworkWeek1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
		public IQueryable<客戶資料> GetCustomers(string searchString)
		{
			if (!string.IsNullOrEmpty(searchString))
				return Where(n => n.客戶名稱.Contains(searchString));
			else
				return All();
		}

		public 客戶資料 Find(int? id)
		{
			return Where(n => n.Id == id.Value).FirstOrDefault();
		}

		public override IQueryable<客戶資料> All()
		{
			return Where(n => 1 == 1);
		}

		public override IQueryable<客戶資料> Where(Expression<Func<客戶資料, bool>> expression)
		{
			return base.Where(expression).Where(n=>!n.是否已刪除);
		}

		public override IQueryable<客戶資料> Include<TProperty>(Expression<Func<客戶資料, TProperty>> path)
		{
			return Include(path).Where(n => 1 == 1);
		}

		public override void Delete(客戶資料 entity)
		{
			entity.是否已刪除 = true;
			Update(entity);
		}
	}

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}