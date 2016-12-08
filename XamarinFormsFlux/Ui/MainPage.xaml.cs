using System.Reactive.Linq;
using Xamarin.Forms;
using System;
using XamarinFormsFlux.Util;

namespace XamarinFormsFlux.Ui
{
	public partial class MainPage : ContentPage
	{
		Lazy<MainComponent> component = new Lazy<MainComponent>(() => MainComponent.Initializer.init());

		public MainStore mainStore { get; set; }
		public MainAction mainAction { get; set; }

		public MainPage()
		{
			InitializeComponent();
			component.Value.Inject(this);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			InitListener();
			InitData();
		}

		void InitListener()
		{
			button.Clicked += async (sender, e) =>
			{
				Log.Debug("MainPage # InitListener # OnClick ");
				await mainAction.Name(entry.Text);
			};
		}

		void InitData()
		{
			mainStore.Repos()
					 .Subscribe((items) =>
					 {
						 Log.Debug("MainPage # InitData # New items arrived");
						 Device.BeginInvokeOnMainThread(() => { listView.ItemsSource = items; });
					 });
			mainStore.Errors()
					 .Subscribe((exception) =>
					 {
						 Log.Debug("MainPage # InitData # New exception arrived");
						 Device.BeginInvokeOnMainThread(() => { listView.ItemsSource = exception.ToString(); });
					 });
			mainStore.Name()
					 .Subscribe(async (name) =>
					 {
						 Log.Debug("MainPage # InitData # New name arrived");
						 await mainAction.Refresh();
					 });
		}
	}
}
