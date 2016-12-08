using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Newtonsoft.Json.Linq;
using XamarinFormsFlux.Model;
using XamarinFormsFlux.Util;

namespace XamarinFormsFlux.Data.Api
{
	public class GithubService : IGithubService
	{
		HttpClient client;

		public GithubService()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;
		}

		public IObservable<IList<Item>> GetRepos(string userName)
		{
			return Observable.Create((IObserver<IList<Item>> subscribe) =>
			{
				Log.Debug("Start api access...");
				Log.Debug("Name : " + userName);
				if (userName.Length == 0)
				{
					subscribe.OnNext(new List<Item>());
					subscribe.OnCompleted();
					return Disposable.Empty;
				}
				string url = "https://api.github.com/users/{0}/repos";
				Uri uri = new Uri(string.Format(url, userName));
				try
				{
					HttpResponseMessage response = AsyncHelpers.RunSync(() => client.GetAsync(uri));
					if (response.IsSuccessStatusCode)
					{
						IList<Item> items = new List<Item>();
						string content = AsyncHelpers.RunSync(() => response.Content.ReadAsStringAsync());
						JArray array = JArray.Parse(content);
						foreach (JToken token in array)
						{
							Item item = new Item();
							item.Id = token["id"].Value<int>();
							item.Name = token["name"].Value<string>();
							item.UserName = token["owner"]["login"].Value<string>();
							item.Url = token["html_url"].Value<string>();
							items.Add(item);
						}
						subscribe.OnNext(items);
						Log.Debug("Finish api access successfully!");
					}
					else
					{
						Log.Debug("Failed to get data from api.");
						subscribe.OnError(new Exception(response.ToString()));
					}
				}
				catch (Exception e)
				{
					Log.Debug("Failed to get data from api with exception.");
					subscribe.OnError(e);
				}
				subscribe.OnCompleted();
				return Disposable.Empty;
			});
		}
	}
}
