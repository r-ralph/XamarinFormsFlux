using System.Threading.Tasks;

namespace XamarinFormsFlux.Util
{
	public class Log
	{
		public static void Debug(string s)
		{
			System.Diagnostics.Debug.WriteLine(GetCurrentThreadId() + s);
		}

		public static string GetCurrentThreadId()
		{
			return string.Format("[{0}] ", TaskScheduler.Current.Id);
		}
	}
}
