using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using XamarinFormsFlux.Data.Api;
using XamarinFormsFlux.Model;
using XamarinFormsFlux.Util;

namespace XamarinFormsFlux.Data.Repository
{
	public class GithubRepository
	{
		IGithubService githubService;

		public GithubRepository([Dependency("GithubService")] IGithubService githubService)
		{
			this.githubService = githubService;
		}

		public IObservable<IList<Item>> GetRepos(string name)
		{
			Log.Debug("GithubRepository # GetRepos");
			return githubService.GetRepos(name);
		}
	}
}
