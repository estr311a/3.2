using System;
using System.Collections.ObjectModel;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace notatnik;

public partial class MainWindow : Window
{
    private readonly string _notesPath = Path.Combine(Environment.CurrentDirectory, "notatki.md");
    private readonly ObservableCollection<string> _savedNotes = new();

    public MainWindow()
    {
        InitializeComponent();
        SavedNotesList.ItemsSource = _savedNotes;
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        var title = TitleInput.Text?.Trim();
        var content = ContentInput.Text;

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
        {
            return;
        }

        var entry = $"Tytuł: {title}{Environment.NewLine}Treść: {content}{Environment.NewLine}{Environment.NewLine}";
        File.AppendAllText(_notesPath, entry);
        _savedNotes.Add(title);
        TitleInput.Clear();
        ContentInput.Clear();
    }
}
