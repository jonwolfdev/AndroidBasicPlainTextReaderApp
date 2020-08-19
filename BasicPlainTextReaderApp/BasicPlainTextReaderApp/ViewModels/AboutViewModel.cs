using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BasicPlainTextReaderApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync(Constants.AboutUrl));
            OpenWebCommand = new Command(async () => await Shell.Current.GoToAsync("//LoginPage"));
        }

        public ICommand OpenWebCommand { get; }
    }
}