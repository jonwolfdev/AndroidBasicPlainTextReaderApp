using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace BasicPlainTextReaderApp.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.InstalledApp("dev.jonwolf.basicplaintextreaderapp").StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}