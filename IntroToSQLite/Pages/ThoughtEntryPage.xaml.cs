using System;
using System.Collections.Generic;
using IntroToSQLite.Services;
using Xamarin.Forms;
using IntroToSQLite.Interfaces;
using Autofac;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using Newtonsoft.Json;
using ReactiveUI;
using System.Reflection;

namespace IntroToSQLite.Pages
{
    public partial class ThoughtEntryPage : ContentPage
    {
        private IThoughtEntryVM vm;
        private IDisposable waitSub;
        private BehaviorSubject<bool> _dispose;
        Editor textArea;
        Slider speed;

        private string jsonString;

        public ThoughtEntryPage()
        {
            Title = "SQLite Tester";
            vm = App.Container.Resolve<IThoughtEntryVM>();
            this.BindingContext = vm;
            var textAreaLabel = new Label
            {
                Text = "Enter example Json object below",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            textArea = new Editor
            {
                HeightRequest = 100

            };
            textArea.Completed += TextArea_Completed;

            var buttonSize = new Button
            {
                Text = "DB Size",
                VerticalOptions = LayoutOptions.Center
            };
            buttonSize.SetBinding(Button.CommandProperty, new Binding("GetSizeCommand"));

            var size = new Label
            {
				VerticalOptions = LayoutOptions.Center
            };
            size.SetBinding(Label.TextProperty, new Binding("DbSize"));

            var sizeSL = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            sizeSL.Children.Add(buttonSize);
            sizeSL.Children.Add(size);


            var buttonGetItem = new Button
            {
				Text = "Retrieve",
				VerticalOptions = LayoutOptions.Center
            };
            buttonGetItem.SetBinding(Button.CommandProperty, new Binding("GetItemCommand"));
            var getItem = new Label
            {
				Text = "Item: ",
				VerticalOptions = LayoutOptions.Center
            };

            var item = new Entry
            {
				Placeholder = "Enter Item ID",
				VerticalOptions = LayoutOptions.Center
            };
            item.SetBinding(Entry.TextProperty, new Binding("GetItemId"));
			

			var getItemSL = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

            getItemSL.Children.Add(getItem);
			getItemSL.Children.Add(item);
            getItemSL.Children.Add(buttonGetItem);

			var pluckedItemSL = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
                //BindingContext = "PluckedRandomThought"
			};

            var pluckedItemLabel = new Label
            {
				Text = "Plucked Item: ",
				VerticalOptions = LayoutOptions.Center
            };
            var pluckedItemThought = new Label
            {
				VerticalOptions = LayoutOptions.Center
            };
            pluckedItemThought.SetBinding(Label.TextProperty, new Binding("PluckedRandomThought.Thought"));
			
			var pluckedItemCreatedOn = new Label();
			pluckedItemCreatedOn.SetBinding(Label.TextProperty, new Binding("PluckedRandomThought.CreatedOn"));



            pluckedItemSL.Children.Add(pluckedItemLabel);
            pluckedItemSL.Children.Add(pluckedItemThought);
            pluckedItemSL.Children.Add(pluckedItemCreatedOn);


            var numberOfRowsSL = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var getNumberOfRows = new Button
            {
				Text = "Get Rows",
				VerticalOptions = LayoutOptions.Center
            };
            getNumberOfRows.SetBinding(Button.CommandProperty, new Binding("GetNumberOfRowsCommand"));

            var numberOfRowsLabel = new Label
            {
				VerticalOptions = LayoutOptions.Center
            };
            numberOfRowsLabel.SetBinding(Label.TextProperty, new Binding("NumberOfRows"));

            numberOfRowsSL.Children.Add(getNumberOfRows);
            numberOfRowsSL.Children.Add(numberOfRowsLabel);
            var dropTableButton = new Button
            {
				Text = "Drop Table",
				VerticalOptions = LayoutOptions.Center
            };
            dropTableButton.SetBinding(Button.CommandProperty, new Binding("DropTableCommand"));

            speed = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                VerticalOptions = LayoutOptions.CenterAndExpand

            };
            speed.SetBinding(Slider.ValueProperty, new Binding("Speed"));

            speed.ValueChanged += OnSpeedValueChanged;

            Content = new StackLayout
            {
                Spacing = 20,
                Padding = new Thickness(20),
                Children = {textAreaLabel, textArea, sizeSL, getItemSL, pluckedItemSL, numberOfRowsSL, dropTableButton, speed },
            };


        }

        private void OnSpeedValueChanged(object sender, EventArgs args)
        {
            if(waitSub != null)
            {
                _dispose.OnNext(false);
            }
            var slider = (Slider)sender;
            ((ThoughtEntryVM)this.BindingContext).Speed = slider.Value;
            InitiateAddingItemsToDB();
        }
        private void KickOff(object sender, EventArgs args)
        {
            InitiateAddingItemsToDB();
        }

        private void InitiateAddingItemsToDB()
        {
			_dispose?.Dispose();
			_dispose = null;
			waitSub?.Dispose();
			waitSub = null;

			_dispose = new BehaviorSubject<bool>(true);
            var currentSliderSpeed = ((ThoughtEntryVM)this.BindingContext).Speed;
            var adjustedSpeed = currentSliderSpeed * 2;
			waitSub?.Dispose();
            if(Convert.ToInt32(currentSliderSpeed) != 100 && Convert.ToInt32(currentSliderSpeed) != 0){
                
				waitSub = Observable
                .Interval(TimeSpan.FromMilliseconds(adjustedSpeed))
				.TakeUntil(_dispose.Where(p => !p))
				//.Take(1)
				.Subscribe(val =>
				{
					if (jsonString == null)
					{
						jsonString = JsonConvert.SerializeObject(new List<string>() { "Me", "You", "Us" });
					}
					var newobj = JsonConvert.DeserializeObject(jsonString);
					((ThoughtEntryVM)this.BindingContext).AutoThought = JsonConvert.SerializeObject(newobj);
                    ((ThoughtEntryVM)this.BindingContext).AutoGetNumberOfRows = true;
                    ((ThoughtEntryVM)this.BindingContext).AutoGetDBSize = true;
				});
            }
			
        }

        void TextArea_Completed(object sender, EventArgs e)
        {
            var editorBox = (Editor)sender;
            jsonString = editorBox.Text;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
			_dispose?.Dispose();
			_dispose = null;
            waitSub?.Dispose();
            waitSub = null;
            textArea.Completed -= TextArea_Completed;
            speed.ValueChanged -= OnSpeedValueChanged;
        }
    }



}