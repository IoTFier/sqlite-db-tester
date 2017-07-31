using System;
using System.Collections.Generic;
using System.Linq;
using IntroToSQLite;

using Foundation;
using UIKit;

namespace IntroToSQLite.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();



			string applicationFolderPath
			 = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database");
			// Create the folder path.
			System.IO.Directory.CreateDirectory(applicationFolderPath);
			//App.directoryPath = System.IO.Path.Combine(applicationFolderPath, "introToDroid.db");


            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
