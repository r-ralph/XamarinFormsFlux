using PCLStorage;
using SQLite;
using XamarinFormsFlux.Model;
using XamarinFormsFlux.Util;

namespace XamarinFormsFlux
{
	public class SQLiteDatabase
	{
		const  string databaseFileName = "items.db";

		public SQLiteConnection CreateConnection()
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			ExistenceCheckResult result = AsyncHelpers.RunSync(() => rootFolder.CheckExistsAsync(databaseFileName));
			if (result == ExistenceCheckResult.NotFound)
			{
				IFile file = AsyncHelpers.RunSync(() => rootFolder.CreateFileAsync(databaseFileName, CreationCollisionOption.ReplaceExisting));
				SQLiteConnection connection = new SQLiteConnection(file.Path);
				connection.CreateTable<Item>();
				return connection;
			}
			else
			{
				IFile file = AsyncHelpers.RunSync(() => rootFolder.CreateFileAsync(databaseFileName, CreationCollisionOption.OpenIfExists));
				return new SQLiteConnection(file.Path);
			}
		}
	}
}
