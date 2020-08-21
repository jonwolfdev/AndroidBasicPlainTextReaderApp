using BasicPlainTextReaderApp.Library;
using BasicPlainTextReaderApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace BasicPlainTextReaderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchedTextPage : ContentPage
    {
        readonly SearchedViewModel _vm;
        readonly TextModel _data;
        readonly List<TapGestureRecognizer> _gestures;

        public SearchedTextPage(TextModel data, string search)
        {
            InitializeComponent();
            _vm = BindingContext as SearchedViewModel;

            _gestures = new List<TapGestureRecognizer>();
            
            _data = data;
            var occurrences = Helper.SearchAllStrings(data.Text, search, Constants.SearchTextGetFromSides);
            ScrollViewCtrl.Content = CreateStackLayout(occurrences);
            _vm.Title = search;
            _vm.QuantityFound = occurrences.Count.ToString();
        }

        private async void SearchItem_Clicked(object sender, EventArgs e)
        {
            string search = await DisplayPromptAsync("Search for", "Enter the text to search for", "Search", "Cancel", "search for...");
            if (string.IsNullOrEmpty(search))
            {
                return;
            }

            var occurrences = Helper.SearchAllStrings(_data.Text, search, Constants.SearchTextGetFromSides);
            ScrollViewCtrl.Content = CreateStackLayout(occurrences);
            _vm.Title = search;
            _vm.QuantityFound = occurrences.Count.ToString();
        }

        private StackLayout CreateStackLayout(IReadOnlyList<string> occurrences)
        {
            if (_gestures.Count >= 1)
            {
                _gestures.ForEach(x => x.Tapped -= Gesture_Tapped);
                _gestures.Clear();
            }

            var sl = new StackLayout();
            
            if(occurrences.Count > 0)
            {
                foreach (var str in occurrences)
                {
                    var label = new Label()
                    {
                        Text = str,
                        Padding = new Thickness(2, 5, 2, 5),
                        BackgroundColor = Color.Accent
                    };
                    var gesture = new TapGestureRecognizer();
                    gesture.Tapped += Gesture_Tapped;
                    _gestures.Add(gesture);
                    label.GestureRecognizers.Add(gesture);

                    sl.Children.Add(label);
                }
            }
            else
            {
                var label = new Label()
                {
                    Text = "Text was not found",
                    Padding = new Thickness(2, 5, 2, 5),
                    HorizontalTextAlignment = TextAlignment.Center
                };
                
                sl.Children.Add(label);
            }
            
            
            return sl;
        }

        private async void Gesture_Tapped(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                if (string.IsNullOrEmpty(label.Text))
                    return;

                await Clipboard.SetTextAsync(label.Text);
                await DisplayAlert("Copied to clipboard", "Text was copied to clipboard!", "Close");
            }
        }
    }
}