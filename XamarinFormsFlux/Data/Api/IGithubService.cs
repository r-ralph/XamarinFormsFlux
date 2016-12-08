using System;
using System.Collections.Generic;
using XamarinFormsFlux.Model;

namespace XamarinFormsFlux.Data.Api
{
	public interface IGithubService
	{
		IObservable<IList<Item>> GetRepos(string userName);
	}
}
