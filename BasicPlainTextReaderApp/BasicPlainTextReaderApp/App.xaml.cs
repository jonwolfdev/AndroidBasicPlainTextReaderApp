using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BasicPlainTextReaderApp.Views;

namespace BasicPlainTextReaderApp
{
    public partial class App : Application
    {
        string _text;
        public App(string text = null)
        {
            InitializeComponent();

            _text = text;
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
