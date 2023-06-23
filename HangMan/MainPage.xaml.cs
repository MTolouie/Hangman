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

    List<char> Guessed = new List<char>();

    public string Answer { get; set; } = string.Empty;

    Materials materials = new Materials();

    public MainPage()
    {
        InitializeComponent();
        materials.Letters.AddRange("abcdefghijklmnopqrstuvwxyz");
        BindingContext = materials;
        PickWord();
        CalculateWord(Answer, Guessed);
    }

    private void PickWord()
    {
        Answer = Words[new Random().Next(0, Words.Count)];
    }

    private void CalculateWord(string answer,List<char> guessed)
    {
        var temp = answer.Select(x => (guessed.IndexOf(x) >= 0 ? x : '_'));
        materials.Spotlight = string.Join(' ',temp);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null)
        {
            btn.IsEnabled = false;
            var letter = btn.Text;
            HandleGuess(letter[0]);
        }
    }

    private void HandleGuess(char letter)
    {
        if (Guessed.IndexOf(letter) == -1)
        {
            Guessed.Add(letter);
        }
        if(Answer.IndexOf(letter) >= 0)
        {
            CalculateWord(Answer, Guessed);
            CheckIfGameWon();
        }
    }

    private void CheckIfGameWon()
    {
        if (materials.Spotlight.Replace(" ", "") == Answer)
        {
            materials.Message = "You Won";
        }
    }
}

