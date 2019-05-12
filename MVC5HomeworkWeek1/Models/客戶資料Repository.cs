using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5HomeworkWeek1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
		public 客戶資料 Find(int? id)
		{
			return Where(n => n.Id == id.Value).FirstOrDefault();
		}
	}

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}