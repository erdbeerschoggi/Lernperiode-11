using System;
using CommunityToolkit.Mvvm.ComponentModel;
using KletternRoutenApp.Models;

namespace KletternRoutenApp.ViewModels
{
    public partial class KletterRoutenViewModel : ViewModelBase
    {
        public KletterRoutenViewModel()
        {
            // leer
        }

        public KletterRoutenViewModel(KletterRouten item)
        {
            IsChecked = item.IsChecked;
            Content = item.Content;
        } 

        private bool _isChecked;

        public bool IsChecked {
            get { return _isChecked; }
            set { SetProperty(ref _isChecked, value); }
        }

        [ObservableProperty]
        public partial string? Content { get; set; }


        public KletterRouten GetKletterRouten()
    {
        return new KletterRouten()
        {
            IsChecked = this.IsChecked,
            Content = this.Content
        };
    }
    
    }


}