using System;
using System.Collections.Generic;
using BasicPlainTextReaderApp.Library;
using BasicPlainTextReaderApp.ViewModels;
using BasicPlainTextReaderApp.Views;
using Xamarin.Forms;

namespace BasicPlainTextReaderApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        readonly App _parent;
        public AppShell(App parent)
        {
            InitializeComponent();
            _parent = parent;
            Shell.SetTabBarIsVisible(this, false);
        }

        private void OnMenuItemClicked(object sender, EventArgs e)
        {
            _parent.GoToTextPage();
        }
    }
}
