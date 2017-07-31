using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace IntroToSQLite.Droid
{
    [Activity(Label = "IntroToSQLite.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
   //         string applicationFolderPath
   //          = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database");
			//// Create the folder path.
            //System.IO.Directory.CreateDirectory(applicationFolderPath);
            //App.directoryPath = System.IO.Path.Combine(applicationFolderPath, "introToDroid.db");
            LoadApplication(new App());
        }
    }
}
