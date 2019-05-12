using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5HomeworkWeek1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
		public 客戶銀行資訊 Find(int? id)
		{
			return Where(n => n.Id == id.Value).FirstOrDefault();
		}
	}

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}