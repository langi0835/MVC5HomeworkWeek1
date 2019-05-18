using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MVC5HomeworkWeek1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
		public 客戶銀行資訊 Find(int? id)
		{
			return Where(n => n.Id == id.Value).FirstOrDefault();
		}

		public override IQueryable<客戶銀行資訊> All()
		{
			return Where(n => 1 == 1);
		}

		public override IQueryable<客戶銀行資訊> Where(Expression<Func<客戶銀行資訊, bool>> expression)
		{
			return base.Where(expression).Where(n => !n.是否已刪除);
		}

		public override IQueryable<客戶銀行資訊> Include<TProperty>(Expression<Func<客戶銀行資訊, TProperty>> path)
		{
			return Include(path).Where(n => 1 == 1);
		}

		public override void Delete(客戶銀行資訊 entity)
		{
			entity.是否已刪除 = true;
			Update(entity);
		}
	}

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}