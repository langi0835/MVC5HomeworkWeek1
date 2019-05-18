using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5HomeworkWeek1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
		public 客戶聯絡人 Find(int? id)
		{
			return Where(n => n.Id == id.Value).FirstOrDefault();
		}

	}

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}