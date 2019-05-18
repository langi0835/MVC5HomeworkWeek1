

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MVC5HomeworkWeek1.Models
{ 
	public interface IRepository<T> 
	{
		IUnitOfWork UnitOfWork { get; set; }
		IQueryable<T> All();
		IQueryable<T> Where(Expression<Func<T, bool>> expression);
		IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path);
		void Add(T entity);
		void Delete(T entity);
	}
}

