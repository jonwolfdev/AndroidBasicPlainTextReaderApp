using BasicPlainTextReaderApp.Library;
using BasicPlainTextReaderApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BasicPlainTextReaderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextPage : ContentPage
    {
        readonly TextPageViewModel _vm;
        public TextPage(TextModel model = null)
        {
            InitializeComponent();

            _vm = BindingContext as TextPageViewModel;
            _vm.InfoItem = InfoItem;
            _vm.TModel = model;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (_vm.TModel == null)
                return;

            await DisplayAlert("Extra details",
                "Type: " + _vm.TModel.Type + Environment.NewLine +
                "DataPath: " + _vm.TModel.DataPath + Environment.NewLine +
                "DataString: " + _vm.TModel.DataString,
                "Close");
        }
    }
}