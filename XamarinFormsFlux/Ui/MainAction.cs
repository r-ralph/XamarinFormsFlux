using System;
using Microsoft.Practices.Unity;
using System.Reactive.Linq;
using XamarinFormsFlux.Data.Repository;
using XamarinFormsFlux.Util;
using System.Threading.Tasks;

namespace XamarinFormsFlux.Ui
{
	public class MainAction
	{
		readonly MainDispatcher dispatcher;
		readonly GithubRepository repository;

		public MainAction([Dependency("MainDispatcher")] MainDispatcher dispatcher,
						  [Dependency("GithubRepository")] GithubRepository repository)
		{
			System.Diagnostics.Debug.WriteLine("MainAction # Constructor");
			this.dispatcher = dispatcher;
			this.repository = repository;
		}

		public async Task Refresh()
		{
			System.Diagnostics.Debug.WriteLine("MainAction # Refresh");
			await Task.Factory.StartNew(() =>
			{
				dispatcher.nameSubject
						  .Take(1)
						  .SelectMany((name) =>
							  {
								  Log.Debug("MainAction # Refresh # Name arrived");
								  return repository.GetRepos(name);
							  })
						  .Subscribe((items) =>
						  {
							  dispatcher.Dispatch(items);
						  }, (e) =>
						  {
							  dispatcher.errorSubjecct.OnNext(e);
						  });
			});
		}

		public async Task Name(string name)
		{
			System.Diagnostics.Debug.WriteLine("MainAction # Name");
			await Task.Factory.StartNew(() =>
			{
				dispatcher.nameSubject.OnNext(name);
			});
		}
	}
}
