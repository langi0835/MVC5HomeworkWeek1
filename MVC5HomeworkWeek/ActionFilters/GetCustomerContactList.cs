using MVC5HomeworkWeek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5HomeworkWeek.ActionFilters
{
	public class GetCustomerContactList : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var repo = RepositoryHelper.Get客戶聯絡人Repository();
			filterContext.Controller.ViewBag.CustomerContactList = repo.All().ToList();

			base.OnActionExecuting(filterContext);
		}
	}
}