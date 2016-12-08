using SQLite;
using System;
using System.Reactive.Linq;
using XamarinFormsFlux.Model;
using XamarinFormsFlux.Util;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using System.Reactive.Subjects;
using System.Linq;

namespace XamarinFormsFlux.Ui
{
	public class MainDispatcher
	{
		readonly SQLiteDatabase db;

		readonly ISubject<string> triggers;

		internal readonly ISubject<string> nameSubject;

		internal readonly ISubject<Exception> errorSubjecct;

		public MainDispatcher([Dependency("SQLiteDatabase")] SQLiteDatabase db)
		{
			Log.Debug("MainDispatcher # Constructor");
			this.db = db;
			triggers = new Subject<string>();
			nameSubject = new BehaviorSubject<string>("");
			errorSubjecct = new Subject<Exception>();
		}

		public IObservable<IList<Item>> Repos()
		{
			Log.Debug("MainDispatcher # Repos");
			return triggers.AsObservable()
						   .SelectMany(_ => nameSubject.Take(1))
						   .Select((name) =>
					{
						Log.Debug("MainDispatcher # Repos # Next");
						IList<Item> items = new List<Item>();
						using (SQLiteConnection connection = db.CreateConnection())
						{
							foreach (Item item in (from x in connection.Table<Item>()
												   where x.UserName == name
												   orderby x.Id
												   select x))
							{
								items.Add(item);
							}
						}
						return items;
					});
		}

		public int Dispatch(IList<Item> items)
		{
			int count = 0;
			Log.Debug("MainDispatcher # Dispatch # Start transaction");
			using (SQLiteConnection connection = db.CreateConnection())
			{
				connection.BeginTransaction();
				foreach (var i in items)
				{
					count += connection.InsertOrReplace(i);
				}
				connection.Commit();
				triggers.OnNext("");
			}
			Log.Debug("MainDispatcher # Dispatch # End transaction # " + count);
			return count;
		}
	}
}
