using BasicPlainTextReaderApp.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BasicPlainTextReaderApp.ViewModels
{
    public class TextPageViewModel : BaseViewModel
    {
        TextModel _model;
        public ToolbarItem InfoItem { get; set; }
        public TextPageViewModel()
        {
            ShowPlaceholderText = true;
            ShowText = false;
            Title = "No text!";
        }

        public TextModel TModel
        {
            get { return _model; }
            set
            {
                if (value != null)
                {
                    ShowPlaceholderText = false;
                    ShowText = true;

                    Title = value.DataPath;
                    InfoItem.IsEnabled = true;
                }
                else
                {
                    ShowPlaceholderText = true;
                    ShowText = false;
                    InfoItem.IsEnabled = false;
                }
                SetProperty(ref _model, value);
            }
        }
        bool _showPlaceholderText;
        bool _showText;
        public bool ShowPlaceholderText { get { return _showPlaceholderText; } set { SetProperty(ref _showPlaceholderText, value); } }
        public bool ShowText { get { return _showText; } set { SetProperty(ref _showText, value); } }
    }
}
