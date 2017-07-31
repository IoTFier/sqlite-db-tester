using System;
using System.Collections.Generic;
using IntroToSQLite.Services;
using Xamarin.Forms;
using Autofac;
using ReactiveUI;
using IntroToSQLite.Models;
using System.Reactive.Linq;
using IntroToSQLite.Interfaces;

namespace IntroToSQLite.Pages
{
    public partial class RandomThoughtsPage : ContentPage, IRandomThoughtsPage, IViewFor<RandomThoughtsVM>
    {
		//private RandomThoughtDatabase _database;
		private ListView _thoughtList;
        IRandomThoughtsVM vm;

		public RandomThoughtsPage()
		{
            InitializeComponent();
			//_database = database;
            vm = App.Container.Resolve<IRandomThoughtsVM>();
            this.BindingContext = vm;
			Title = "Random Thoughts";
			//var thoughts = _database.GetThoughts();

			_thoughtList = new ListView();
            _thoughtList.SetBinding(ListView.ItemsSourceProperty, new Binding("ThoughtList"));
			_thoughtList.ItemTemplate = new DataTemplate(typeof(TextCell));
			_thoughtList.ItemTemplate.SetBinding(TextCell.TextProperty, "Thought");
			_thoughtList.ItemTemplate.SetBinding(TextCell.DetailProperty, "CreatedOn");

			var toolbarItem = new ToolbarItem
			{
				Name = "Add",
				//Command = new Command(() => Navigation.PushAsync(new ThoughtEntryPage(this)))
			};

			ToolbarItems.Add(toolbarItem);

			Content = _thoughtList;


		}

		//public void Refresh()
		//{
		//	_thoughtList.ItemsSource = _database.GetThoughts();
		//}

		public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<RandomThoughtsPage, RandomThoughtsVM>(x => x.ViewModel, default(RandomThoughtsVM));

		object IViewFor.ViewModel
		{
			get { return ViewModel; }
            set { ViewModel = (RandomThoughtsVM)value; }
		}
        public RandomThoughtsVM ViewModel
		{
            get { return (RandomThoughtsVM)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
    }
}
