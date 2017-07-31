using System;
using System.IO;
using IntroToSQLite.Interfaces;
using IntroToSQLite.iOS;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteStuff_iOS))]
namespace IntroToSQLite.iOS
{
    public class SQLiteStuff_iOS : ISQLiteStuff
    {
        SQLiteConnection connection;

        public SQLiteStuff_iOS()
        {
        }

		public SQLiteConnection GetConnection()
		{
			//var fileName = "RandomThought.db3";
			//var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			//var libraryPath = Path.Combine(documentsPath, "..", "Library");
			//var path = Path.Combine(libraryPath, fileName);

			//var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
			//var connection = new SQLite.Net.SQLiteConnection(platform, path);

			//return connection;

			var fileName = "introToDroid.db";
			var dbsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(dbsPath, "..", "Library");
			//System.IO.Directory.CreateDirectory(dbsPath);
            var path = Path.Combine(libraryPath, fileName);
			//var platform = new Platform.XamarinAndroid.SQLitePlatformAndroid ();
			if (connection == null)
			{
				connection = new SQLiteConnection(path);
			}
			return connection;
		}

		public long GetDBSize()
		{
			var fileInfo = new FileInfo(connection.DatabasePath);
			//return File.ReadAllBytes(connection.DatabasePath).LongCount();
			return fileInfo.Length;
		}

	}
}
