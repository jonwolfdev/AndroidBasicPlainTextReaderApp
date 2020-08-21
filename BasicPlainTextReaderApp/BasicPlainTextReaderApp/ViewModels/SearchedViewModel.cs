using System;
using System.Collections.Generic;
using System.Text;

namespace BasicPlainTextReaderApp.ViewModels
{
    public class SearchedViewModel : BaseViewModel
    {
        public string _quantityFound;
        public string QuantityFound { get { return _quantityFound; } set { SetProperty(ref _quantityFound, value); } }
    }
}
