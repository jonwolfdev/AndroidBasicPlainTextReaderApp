using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BasicPlainTextReaderApp.Views;
using BasicPlainTextReaderApp.Library;

namespace BasicPlainTextReaderApp
{
    public partial class App : Application
    {
        public App(TextModel data = null)
        {
            InitializeComponent();

            if (data == null)
            {
                MainPage = new AppShell(this);
            }
            else
            {
                MainPage = new AppShellOpenWith(this, data);
            }
        }

        public void GoToTextPage()
        {
            var page = new TextPage();
            Shell.Current.Navigation.PushAsync(page);
            Shell.Current.FlyoutIsPresented = false;
        }
        public void GoToAboutPage()
        {
            var page = new AboutPage();
            Shell.Current.Navigation.PushAsync(page);
            Shell.Current.FlyoutIsPresented = false;
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            //TODO: not sure if this is needed
            //if (MainPage is AppShellOpenWith shell)
            //{
            //    shell.SetTextData();
            //}
        }
    }
}
