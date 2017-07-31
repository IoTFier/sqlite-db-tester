using Xamarin.Forms;
using IntroToSQLite.Pages;
using SQLite;
using IntroToSQLite.Services;
using Autofac;
using IntroToSQLite.Interfaces;

namespace IntroToSQLite
{
    public partial class App : Application
    {
        //public static string directoryPath { get; set; }
        //RandomThoughtDatabase database;
        public static IContainer Container { get; set; }

        public App()
        {
            InitializeComponent();
            SetupIOC();
            //MainPage = new IntroToSQLitePage();
            //database = new RandomThoughtDatabase();
            MainPage = new NavigationPage(new ThoughtEntryPage());
        }
		private void SetupIOC()
		{
			var builder = new ContainerBuilder();
            builder.RegisterType<RandomThoughtsVM>().As<IRandomThoughtsVM>();
            builder.RegisterType<ThoughtEntryVM>().As<IThoughtEntryVM>();
            builder.RegisterType<RandomThoughtDatabase>().As<IRandomThoughtDatabase>().SingleInstance();
			Container = builder.Build();
		}
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
