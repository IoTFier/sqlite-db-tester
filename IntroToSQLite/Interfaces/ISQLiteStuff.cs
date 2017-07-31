using System;
using SQLite;

namespace IntroToSQLite.Interfaces
{
    public interface ISQLiteStuff
    {
        SQLiteConnection GetConnection();
        long GetDBSize();
    }
}
