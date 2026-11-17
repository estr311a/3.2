using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace notatnik;

public partial class MainWindow : Window
{
    private readonly string _notesPath = Path.Combine(Environment.CurrentDirectory, "notatki.md");
    private readonly List<Note> _notes = new();
    private readonly ObservableCollection<string> _savedNotes = new();

    public MainWindow()
    {
        InitializeComponent();
        SavedNotesList.ItemsSource = _savedNotes;
        LoadNotes();
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        var title = TitleInput.Text?.Trim();
        var content = ContentInput.Text;

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
        {
            StatusText.Text = "Podaj tytuł i treść notatki.";
            return;
        }

        _notes.Insert(0, new Note(DateTime.Now, title, content));
        try
        {
            SaveNotes();
            TitleInput.Clear();
            ContentInput.Clear();
            StatusText.Text = "Zapisano";
        }
        catch (Exception ex)
        {
            _notes.RemoveAt(0);
            StatusText.Text = $"Nie udało się zapisać: {ex.Message}";
        }
    }

    private void LoadNotes()
    {
        if (!File.Exists(_notesPath))
        {
            return;
        }

        var text = File.ReadAllText(_notesPath, Encoding.UTF8).Replace("\r\n", "\n").TrimEnd('\n');
        var blocks = Regex.Split(text,
            @"(?m)(?=^(?:Data: \d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}|Tytuł: ))");

        foreach (var rawBlock in blocks)
        {
            var block = rawBlock.Trim('\n');
            var lines = block.Split('\n', 4);
            if (lines.Length == 4 &&
                DateTime.TryParseExact(lines[0].Replace("Data: ", string.Empty), "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var createdAt) &&
                lines[1].StartsWith("Tytuł: ", StringComparison.Ordinal) &&
                lines[2] == "Treść:")
            {
                _notes.Add(new Note(createdAt, lines[1]["Tytuł: ".Length..], lines[3]));
            }
            else if (lines.Length >= 2 &&
                     lines[0].StartsWith("Tytuł: ", StringComparison.Ordinal))
            {
                var contentStart = block.IndexOf("\nTreść: ", StringComparison.Ordinal);
                if (contentStart >= 0)
                {
                    _notes.Add(new Note(File.GetLastWriteTime(_notesPath),
                        lines[0]["Tytuł: ".Length..],
                        block[(contentStart + "\nTreść: ".Length)..]));
                }
            }
        }

        foreach (var note in _notes)
        {
            _savedNotes.Add(note.Title);
        }
    }

    private void SaveNotes()
    {
        var entries = _notes.Select(note =>
            $"Data: {note.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}\n" +
            $"Tytuł: {note.Title}\nTreść:\n{note.Content}");
        File.WriteAllText(_notesPath, string.Join("\n\n", entries) + "\n", new UTF8Encoding(false));
        _savedNotes.Insert(0, _notes[0].Title);
    }

    private sealed record Note(DateTime CreatedAt, string Title, string Content);
}
