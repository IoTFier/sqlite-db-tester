using System;
using ReactiveUI;
using IntroToSQLite.Services;
using Xamarin.Forms;
using IntroToSQLite.Models;
using IntroToSQLite.Interfaces;

namespace IntroToSQLite.Pages
{
    public class RandomThoughtsVM : ReactiveObject, IRandomThoughtsVM
    {
        private IRandomThoughtDatabase _randomThoughtDatabase;

		private int _threshold;
		public int Threshold
		{
			get { return _threshold; }
			set { this.RaiseAndSetIfChanged(ref _threshold, value); }
		}

        //private ListView _thoughtList;
        //public ListView ThoughtList
        //{
        //    get { return _thoughtList; }
        //    set { this.RaiseAndSetIfChanged(ref _thoughtList, value);}
        //}
        public ReactiveList<RandomThought> ThoughtList { get; set; }

        public RandomThoughtsVM(IRandomThoughtDatabase randomThoughtDatabase)
        {
            _randomThoughtDatabase = randomThoughtDatabase;
            ThoughtList = new ReactiveList<RandomThought>();
            var thoughts = _randomThoughtDatabase.GetThoughts();
            ThoughtList.AddRange(thoughts);
			
        }
    }
}
