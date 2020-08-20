using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BasicPlainTextReaderApp.Views;
using BasicPlainTextReaderApp.Library;

namespace BasicPlainTextReaderApp
{
    public partial class App : Application
    {
        readonly TextModel _data;
        public App(TextModel data = null)
        {
            InitializeComponent();

            _data = data;
            MainPage = new AppShell(this);
        }

        public void GoToCurrentTextPage()
        {
            var page = new TextPage(_data);
            Shell.Current.Navigation.PushAsync(page);
            Shell.Current.FlyoutIsPresented = false;
        }

        protected override void OnStart()
        {
            if(_data != null)
            {
                GoToCurrentTextPage();
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
