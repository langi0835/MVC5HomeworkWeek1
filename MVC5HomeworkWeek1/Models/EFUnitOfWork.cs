using System.Data.Entity;

namespace MVC5HomeworkWeek1.Models
{
	public class EFUnitOfWork : IUnitOfWork
	{
		public DbContext Context { get; set; }

		public EFUnitOfWork()
		{
			Context = new 客戶資料Entities();
		}

		public void Commit()
		{
			Context.SaveChanges();

			// Update可能修改到，要復原
			if (Context.Configuration.ValidateOnSaveEnabled == false)
			{
				Context.Configuration.ValidateOnSaveEnabled = true;
			}
		}
		
		public bool LazyLoadingEnabled
		{
			get { return Context.Configuration.LazyLoadingEnabled; }
			set { Context.Configuration.LazyLoadingEnabled = value; }
		}

		public bool ProxyCreationEnabled
		{
			get { return Context.Configuration.ProxyCreationEnabled; }
			set { Context.Configuration.ProxyCreationEnabled = value; }
		}
		
		public string ConnectionString
		{
			get { return Context.Database.Connection.ConnectionString; }
			set { Context.Database.Connection.ConnectionString = value; }
		}
	}
}
