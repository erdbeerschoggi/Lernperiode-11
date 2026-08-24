using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KletternRoutenApp.Models;
using System.Collections.ObjectModel;

namespace KletternRoutenApp.ViewModels;


public partial class MainViewModel : ViewModelBase
{
    public ObservableCollection<KletterRoutenViewModel> ToDoItems { get; } = new ObservableCollection<KletterRoutenViewModel>();
   
    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddItemCommand))] 
    public partial string? NewItemContent { get; set; }

    [RelayCommand(CanExecute = nameof(CanAddItem))]
    private void AddItem()
    {
        ToDoItems.Add(new KletterRoutenViewModel(new KletterRouten
        {
            IsChecked = false,
            Content = NewItemContent
        }));

        NewItemContent = string.Empty;
    }

    private bool CanAddItem()
    {
        return !string.IsNullOrWhiteSpace(NewItemContent);
    }

    [RelayCommand]
    private void DeleteItem(KletterRoutenViewModel item)
    {
        ToDoItems.Remove(item);
    }
}
