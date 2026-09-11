using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KletternRoutenApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace KletternRoutenApp.ViewModels;


public partial class MainViewModel : ViewModelBase
{
    // Pfad zur JSON-Datei, in der die Routen dauerhaft gespeichert werden.
    private static readonly string SaveFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "KletternRoutenApp",
        "routen.json");

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

    [ObservableProperty]
    public partial string? NewItemKletterart { get; set; } = "Bouldern";

    public List<string> SchwierigkeitsgradOptions { get; } = new()
    {
        "3a", "3b", "3c", "4a", "4b", "4c", "5a", "5a+", "5b", "5b+", "5c", "5c+", "6a", "6a+", "6b", "6b+", "6c", "6c+",
        "7a", "7a+", "7b", "7b+", "7c", "7c+", "8a", "8a+", "8b", "8b+", "8c", "8c+", "9a", "9a+", "9b", "9b+", "9c"
    };

    public MainViewModel()
    {
        LoadItems();
    }

    [RelayCommand(CanExecute = nameof(CanAddItem))]
    private void AddItem()
    {
        ToDoItems.Add(new KletterRoutenViewModel(new KletterRouten
        {
            IsChecked = false,
            Content = NewItemContent,
            Gym = NewItemGym,
            Schwierigkeitsgrad = NewItemSchwierigkeitsgrad,
            Datum = NewItemDatum,
            Kletterart = NewItemKletterart
        }));

        NewItemContent = string.Empty;
        NewItemGym = string.Empty;
        NewItemSchwierigkeitsgrad = null;
        NewItemDatum = DateTimeOffset.Now;
        NewItemKletterart = "Bouldern";

        SaveItems();
    }

    [RelayCommand]
    private void SelectKletterart(string art)
    {
        NewItemKletterart = art;
    }

    private bool CanAddItem()
    {
        return !string.IsNullOrWhiteSpace(NewItemContent);
    }

    [RelayCommand]
    private void DeleteItem(KletterRoutenViewModel item)
    {
        ToDoItems.Remove(item);
        SaveItems();
    }

    private void SaveItems()
    {
        try
        {
            var directory = Path.GetDirectoryName(SaveFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var routenListe = ToDoItems.Select(vm => vm.GetKletterRouten()).ToList();

            var json = JsonSerializer.Serialize(routenListe, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SaveFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Speichern: {ex.Message}");
        }
    }

    private void LoadItems()
    {
        try
        {
            if (!File.Exists(SaveFilePath))
            {
                return;
            }

            var json = File.ReadAllText(SaveFilePath);
            var routenListe = JsonSerializer.Deserialize<List<KletterRouten>>(json);

            if (routenListe is null)
            {
                return;
            }

            foreach (var route in routenListe)
            {
                ToDoItems.Add(new KletterRoutenViewModel(route));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Laden: {ex.Message}");
        }
    }
}