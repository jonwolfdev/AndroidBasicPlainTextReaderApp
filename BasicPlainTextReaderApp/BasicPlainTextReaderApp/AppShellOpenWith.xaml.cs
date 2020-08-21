using BasicPlainTextReaderApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BasicPlainTextReaderApp
{
    public partial class AppShellOpenWith : Xamarin.Forms.Shell
    {
        readonly App _parent;
        readonly TextModel _data;
        bool _sentData;
        public AppShellOpenWith(App parent, TextModel data)
        {
            InitializeComponent();
            _parent = parent;
            _data = data;
            Shell.SetTabBarIsVisible(this, false);
        }
        
        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
            if (_sentData)
                return;
            _sentData = true;

            MessagingCenter.Send<TextModel>(_data, "TextData");
        }
        public void SetTextData()
        {
            _sentData = false;
        }
        private void OnMenuItemClicked(object sender, EventArgs e)
        {
            _parent.GoToAboutPage();
        }
    }
}