using Microsoft.Practices.Unity;
using Xamarin.Forms;
using XamarinFormsFlux.Ui;

namespace XamarinFormsFlux
{
	public partial class App : Application
	{
		public UnityContainer container;

		public App()
		{
			InitializeComponent();
			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
