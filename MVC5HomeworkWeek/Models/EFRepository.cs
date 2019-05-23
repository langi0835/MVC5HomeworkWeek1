using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;

namespace MVC5HomeworkWeek.Models
{
	public class EFRepository<T> : IRepository<T>
		where T : class
	{
		public IUnitOfWork UnitOfWork { get; set; }

		private IDbSet<T> _objectset;

		private IDbSet<T> ObjectSet
		{
			get
			{
				if (_objectset == null)
				{
					_objectset = UnitOfWork.Context.Set<T>();
				}
				return _objectset;
			}
		}

		public virtual IQueryable<T> All()
		{
			return ObjectSet.AsQueryable();
		}

		public virtual T First(Expression<Func<T, bool>> expression)
		{
			return ObjectSet.Where(expression).FirstOrDefault();
		}

		public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression)
		{
			return ObjectSet.Where(expression);
		}

		public virtual IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path)
		{
			return ObjectSet.Include(path);
		}


		public virtual void Add(T entity)
		{
			ObjectSet.Add(entity);
		}

		/// <summary>
		/// 更新一筆Entity內容。
		/// </summary>
		/// <param name="entity">要更新的內容</param>
		public void Update(T entity)
		{
			UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
		}

		/// <summary>
		/// 更新一筆資料
		/// </summary>
		/// <param name="entity">更新的內容</param>
		/// <param name="updateProperties">更新的欄位</param>
		//public void Update(T entity, Expression<Func<T, object>>[] updateProperties)
		//{
		//	UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;

		//	UnitOfWork.Context.Entry(entity).State = EntityState.Unchanged;

		//	if (updateProperties != null)
		//	{
		//		foreach (var property in updateProperties)
		//		{
		//			UnitOfWork.Context.Entry(entity).Property(property).IsModified = true;
		//		}
		//	}
		//}

		public virtual void Delete(T entity)
		{
			ObjectSet.Remove(entity);
		}

		public List<KeyValuePair<string, bool>> GetColumnNames(T data)
		{
			var dataType = data.GetType();
			return dataType.GetProperties().Select(n => new KeyValuePair<string, bool>(n.Name, true)).ToList();
		}

		public virtual IQueryable<T> OrderBy(IQueryable<T> obj, string sortKey, bool isDesc = false)
		{
			return obj.OrderBy(sortKey + (isDesc ? " descending" : ""));
		}

		public virtual IQueryable<T> OrderByDescending(IQueryable<T> obj, string sortKey)
		{
			return obj.OrderBy(sortKey + " descending");
		}
	}
}