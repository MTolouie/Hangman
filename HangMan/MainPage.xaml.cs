using System.Diagnostics;
using HangMan.Models;

namespace HangMan;

public partial class MainPage : ContentPage
{

    List<string> Words = new List<string>()
    {
        "python",
        "javascript",
        "maui",
        "csharp",
        "mongodb",
        "sql",
        "xaml",
        "word",
        "excel",
        "powerpoint",
        "code",
        "hotreload",
        "snippets"
    };

    List<char> guess = new List<char>();

    public string Answer { get; set; } = string.Empty;

    Materials materials = new Materials();

    public MainPage()
    {
        InitializeComponent();
        BindingContext = materials;
        PickWord();
        CalculateWord(Answer,guess);
    }

    private void PickWord()
    {
        Answer = Words[new Random().Next(0, Words.Count)];
        Debug.WriteLine(Answer);
    }

    private void CalculateWord(string answer,List<char> guessed)
    {
        var temp = answer.Select(x => (guessed.IndexOf(x) >= 0 ? x : '_'));
        materials.Spotlight = string.Join(' ',temp);
    }

}

