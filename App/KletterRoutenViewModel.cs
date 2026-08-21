using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using KletterRoutenBasis.Models;

namespace KletterRoutenBasis.ViewModels
{
    public partial class KletterRoutenViewModel : ViewModelBase
    {
        /* public KletterRoutenViewModel()
        {
            
        }

        public KletterRoutenViewModel(KletterRouten item)
        {
            IsChecked = item.IsChecked;
            Content = item.Content;
        } */

        private bool _isChecked;

        public bool IsChecked {
            get { return _isChecked; }
            set { SetProperty(ref _isChecked, value); }
        }

        [ObservableProperty]
        public partial string? Content { get; set; }
    }

}
