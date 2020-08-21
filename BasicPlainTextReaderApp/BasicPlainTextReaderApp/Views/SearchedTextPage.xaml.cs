using BasicPlainTextReaderApp.Library;
using BasicPlainTextReaderApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BasicPlainTextReaderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchedTextPage : ContentPage
    {
        readonly SearchedViewModel _vm;
        TextModel _data;
        public SearchedTextPage(TextModel data, string search)
        {
            InitializeComponent();
            _vm = BindingContext as SearchedViewModel;

            _vm.Title = search;
            _data = data;
            var occurrences = Helper.SearchAllStrings(data.Text, search, Constants.SearchTextGetFromSides);
            ScrollViewCtrl.Content = CreateStackLayout(occurrences);

            // await DisplayAlert("Copied to clipboard", "All displayed text here was copied to clipboard!", "Close");
        }

        private async void SearchItem_Clicked(object sender, EventArgs e)
        {
            string search = await DisplayPromptAsync("Search for", "Enter the text to search for", "Search", "Cancel", "search for...");
            if (string.IsNullOrEmpty(search))
            {
                return;
            }

            _vm.Title = search;
            var occurrences = Helper.SearchAllStrings(_data.Text, search, Constants.SearchTextGetFromSides);
            ScrollViewCtrl.Content = CreateStackLayout(occurrences);
        }

        private StackLayout CreateStackLayout(IReadOnlyList<string> occurrences)
        {
            var sl = new StackLayout();
            
            foreach (var str in occurrences)
            {
                sl.Children.Add(new Label()
                {
                    Text = str,
                    Padding = new Thickness(2, 5, 2, 5),
                    BackgroundColor = Color.Accent
                });
            }
            
            return sl;
        }
    }
}