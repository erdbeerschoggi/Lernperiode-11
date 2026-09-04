using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KletternRoutenApp.Models;
using System;
using System.Collections.Generic;
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

    [ObservableProperty]
    public partial string? NewItemGym { get; set; }

    [ObservableProperty]
    public partial string? NewItemSchwierigkeitsgrad { get; set; }

    [ObservableProperty]
    public partial DateTimeOffset? NewItemDatum { get; set; } = DateTimeOffset.Now;

    // Optionen für die Schwierigkeitsgrad-Auswahl (französische Skala)
    public List<string> SchwierigkeitsgradOptions { get; } = new()
    {
        "3", "4", "5a", "5b", "5c", "6a", "6a+", "6b", "6b+", "6c", "6c+",
        "7a", "7a+", "7b", "7b+", "7c", "7c+", "8a"
    };

    [RelayCommand(CanExecute = nameof(CanAddItem))]
    private void AddItem()
    {
        ToDoItems.Add(new KletterRoutenViewModel(new KletterRouten
        {
            IsChecked = false,
            Content = NewItemContent,
            Gym = NewItemGym,
            Schwierigkeitsgrad = NewItemSchwierigkeitsgrad,
            Datum = NewItemDatum
        }));

        NewItemContent = string.Empty;
        NewItemGym = string.Empty;
        NewItemSchwierigkeitsgrad = null;
        NewItemDatum = DateTimeOffset.Now;
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