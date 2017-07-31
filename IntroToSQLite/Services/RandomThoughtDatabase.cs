﻿using System;
using System.Collections.Generic;
using System.Linq;
using IntroToSQLite.Interfaces;
using IntroToSQLite.Models;
using SQLite;
using Xamarin.Forms;

namespace IntroToSQLite.Services
{
    public class RandomThoughtDatabase : IRandomThoughtDatabase
    {
		private SQLiteConnection _connection;

		public RandomThoughtDatabase()
		{
            _connection = DependencyService.Get<ISQLiteStuff>().GetConnection();
            //_connection = new SQLiteConnection(App.directoryPath);
            _connection.CreateTable<RandomThought>();
		}

        public string GetDBSize(string theType)
        {
            var size = DependencyService.Get<ISQLiteStuff>().GetDBSize();
            if(theType == "megabytes")
            {
                return (size / 1024).ToString();
            }
            else {
                return size.ToString();
            }
        }

		public IEnumerable<RandomThought> GetThoughts()
		{
            return (from t in _connection.Table<RandomThought>()
					select t).ToList();
		}

		public RandomThought GetThought(int id)
		{
			return _connection.Table<RandomThought>().FirstOrDefault(t => t.ID == id);
		}

		public void DeleteThought(int id)
		{
			_connection.Delete<RandomThought>(id);
		}

		public void AddThought(string thought)
		{
			var newThought = new RandomThought
			{
				Thought = thought,
				CreatedOn = DateTime.Now
			};

			_connection.Insert(newThought);
		}
        public void DropTable()
        {
            const string command = "DROP TABLE IF EXISTS RandomThought";
            _connection.Execute(command);
        }
        public int QueryNumRows()
        {
			//var whateve = _connection.Query<long>("SELECT COUNT(*) FROM RandomThought"); 
            const string command = "SELECT COUNT(*) FROM RandomThought";

			return  _connection.ExecuteScalar<int>(command);

        }
    }
}
