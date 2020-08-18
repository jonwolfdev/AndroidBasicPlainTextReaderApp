using System.ComponentModel;
using Xamarin.Forms;
using BasicPlainTextReaderApp.ViewModels;

namespace BasicPlainTextReaderApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}