using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Microsoft.Practices.Unity;
using XamarinFormsFlux.Model;
using XamarinFormsFlux.Util;

namespace XamarinFormsFlux.Ui
{
	public class MainStore
	{
		readonly MainDispatcher dispatcher;

		public MainStore([Dependency("MainDispatcher")] MainDispatcher dispatcher)
		{
			Log.Debug("MainStore # Constructor");
			this.dispatcher = dispatcher;
		}

		public IObservable<IList<Item>> Repos()
		{
			Log.Debug("MainStore # Repos");
			return dispatcher.Repos();
		}

		public IObservable<string> Name()
		{
			Log.Debug("MainStore # Name");
			return dispatcher.nameSubject.AsObservable();
		}

		public IObservable<Exception> Errors()
		{
			Log.Debug("MainStore # Errors");
			return dispatcher.errorSubjecct.AsObservable();
		}
	}
}
