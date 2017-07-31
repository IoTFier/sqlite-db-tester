using System;
using System.Reactive;
using IntroToSQLite.Interfaces;
using IntroToSQLite.Models;
using ReactiveUI;
using Xamarin.Forms;
using System.Linq;

namespace IntroToSQLite.Pages
{
    public class ThoughtEntryVM: ReactiveObject, IThoughtEntryVM
    {
        private IRandomThoughtDatabase _database;

        public bool AutoGetNumberOfRows
        {
            set 
            {
                OperateOnAutoGetNumberOfRows(value);
            }
        }

		public bool AutoGetDBSize
		{
			set
			{
				OperateOnAutoGetDBSize(value);
			}
		}

		public string AutoThought
		{			
			set
			{
                OperateOnAutoThought(value);
			}
		}

		private string _getItemId;
		public string GetItemId
		{
			get { return _getItemId; }
			set
			{
                this.RaiseAndSetIfChanged(ref _getItemId, value);
			}
		}

		private RandomThought _pluckedRandomThought;
		public RandomThought PluckedRandomThought
		{
			get { return _pluckedRandomThought; }
			set
			{
				this.RaiseAndSetIfChanged(ref _pluckedRandomThought, value);
			}
		}

		private string _thought;
		public string Thought
		{
			get { return _thought; }
			set
			{
				this.RaiseAndSetIfChanged(ref _thought, value);
			}
		}

		private string _dbSize;
		public string DbSize
		{
			get { return _dbSize; }
			set
			{
				this.RaiseAndSetIfChanged(ref _dbSize, value);
			}
		}

        private string _numberOfRows;
        public string NumberOfRows
		{
			get { return _numberOfRows; }
			set
			{
				this.RaiseAndSetIfChanged(ref _numberOfRows, value);
			}
		}

		private double _speed;
		public double Speed
		{
			get { return _speed; }
			set
			{
				this.RaiseAndSetIfChanged(ref _speed, value);
			}
		}

		private readonly ReactiveCommand<Unit, Unit> _addCommand;
		public ReactiveCommand<Unit, Unit> AddCommand => this._addCommand;

		private readonly ReactiveCommand<Unit, Unit> _getSizeCommand;
		public ReactiveCommand<Unit, Unit> GetSizeCommand => this._getSizeCommand;

		private readonly ReactiveCommand<Unit, Unit> _getItemCommand;
		public ReactiveCommand<Unit, Unit> GetItemCommand => this._getItemCommand;

		private readonly ReactiveCommand<Unit, Unit> _getNumberOfRowsCommand;
        public ReactiveCommand<Unit, Unit> GetNumberOfRowsCommand => this._getNumberOfRowsCommand;

		private readonly ReactiveCommand<Unit, Unit> _dropTableCommand;
		public ReactiveCommand<Unit, Unit> DropTableCommand => this._dropTableCommand;


		public ThoughtEntryVM(IRandomThoughtDatabase database)
        {
            _database = database;
            Speed = 100;

            this._addCommand = ReactiveCommand.Create(
			  () =>
			  {
				   _database.AddThought(Thought);
			   });
			this._getSizeCommand = ReactiveCommand.Create(
			  () =>
			  {
                dbsize();
			  });
			this._getItemCommand = ReactiveCommand.Create(
			  () =>
			  {
                if(GetItemId != null && GetItemId != "")
                  PluckedRandomThought = _database.GetThought(Int32.Parse(GetItemId));
			  });
			this._dropTableCommand = ReactiveCommand.Create(
			  () =>
			  {
				  _database.DropTable();
			  });
            this._getNumberOfRowsCommand = ReactiveCommand.Create(
                () =>
                    {
                        numrows();
                    }
            );

        }
        private void dbsize()
        {
			var dbSizeInBytes = _database.GetDBSize("");
			var dbSizeInKiloBytes = Int32.Parse(dbSizeInBytes) / 1024;
			var dbSizeInMegaBytes = Int32.Parse(dbSizeInBytes) / 1048576;
			DbSize = $"Bytes: {dbSizeInBytes}, KB: {dbSizeInKiloBytes}, MB: {dbSizeInMegaBytes}";
        }
        private void numrows()
        {
			var num = _database.QueryNumRows();
			NumberOfRows = "Rows: " + num.ToString();
        }
        private void OperateOnAutoGetDBSize(bool val)
        {
            dbsize();
        }

        private void OperateOnAutoGetNumberOfRows(bool val)
        {
            numrows();
        }
        private void OperateOnAutoThought(string thought)
        {
            _database.AddThought(thought);
        }
    }
}
