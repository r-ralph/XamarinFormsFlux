using Microsoft.Practices.Unity;
using XamarinFormsFlux.Data.Api;
using XamarinFormsFlux.Data.Repository;

namespace XamarinFormsFlux.Ui
{
	public class MainComponent
	{
		UnityContainer container;

		public MainComponent(UnityContainer container)
		{
			this.container = container;
		}

		public void Inject(MainPage mainPage)
		{
			mainPage.mainStore = container.Resolve<MainStore>();
			mainPage.mainAction = container.Resolve<MainAction>();
		}

		public class Initializer
		{
			public static MainComponent init()
			{
				UnityContainer container = new UnityContainer();
				container.RegisterType<IGithubService, GithubService>(
						"GithubService",
						new ContainerControlledLifetimeManager()
				);
				container.RegisterType<GithubRepository>(
						"GithubRepository",
						new ContainerControlledLifetimeManager(),
						new InjectionConstructor(typeof(GithubService))
				);
				container.RegisterType<SQLiteDatabase>(
						"SQLiteDatabase",
						new ContainerControlledLifetimeManager()
				);
				container.RegisterType<MainDispatcher>(
						"MainDispatcher",
						new ContainerControlledLifetimeManager(),
						new InjectionConstructor(typeof(SQLiteDatabase))
				);
				container.RegisterType<MainStore>(
						"MainStore",
						new ContainerControlledLifetimeManager(),
						new InjectionConstructor(typeof(MainDispatcher))
				);
				container.RegisterType<MainAction>(
						"MainAction",
						new ContainerControlledLifetimeManager(),
						new InjectionConstructor(typeof(MainDispatcher), typeof(GithubRepository))
				);
				return new MainComponent(container);
			}
		}
	}
}
