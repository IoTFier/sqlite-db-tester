using System;
using Xamarin.Forms;
using IntroToSQLite.Interfaces;
using SQLite;
using IntroToSQLite.Droid;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

[assembly: Dependency(typeof(SQLiteStuff_Android))]
namespace IntroToSQLite.Droid
{
    public class SQLiteStuff_Android : ISQLiteStuff
    {
        SQLiteConnection connection;
        public SQLiteStuff_Android()
        {
        }

        public SQLiteConnection GetConnection ()
        {
            var fileName = "introToDroid.db";
            var dbsPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database");
            System.IO.Directory.CreateDirectory(dbsPath);
            var path = Path.Combine (dbsPath, fileName);
            //var platform = new Platform.XamarinAndroid.SQLitePlatformAndroid ();
            if(connection == null){
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
