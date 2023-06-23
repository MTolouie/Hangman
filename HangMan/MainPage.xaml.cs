using System.Data;
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

    public int Mistakes { get; set; } = 0;

    public int MaxWrong { get; set; } = 6;

    public MainPage()
    {
        InitializeComponent();
        materials.Letters.AddRange("abcdefghijklmnopqrstuvwxyz");
        materials.CurrentImage = "img0.jpg";
        BindingContext = materials;
        PickWord();
        CalculateWord(Answer, Guessed);
    }

    private void PickWord()
    {
        Answer = Words[new Random().Next(0, Words.Count)];
    }

    private void CalculateWord(string answer, List<char> guessed)
    {
        var temp = answer.Select(x => (guessed.IndexOf(x) >= 0 ? x : '_'));
        materials.Spotlight = string.Join(' ', temp);
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

        if (Answer.IndexOf(letter) >= 0)
        {
            CalculateWord(Answer, Guessed);
            CheckIfGameWon();
        }
        else if(Answer.IndexOf(letter) == -1)
        {
            Mistakes++;
            UpdateStatus();
            materials.CurrentImage = $"img{Mistakes}.jpg";
            ChekIfGameLost();
        }

        
    }

    private void UpdateStatus()
    {
        materials.GameStatus = $"Error {Mistakes} Of {MaxWrong}";
    }

    private void ChekIfGameLost()
    {
        if (Mistakes == MaxWrong)
        {
            materials.Message = "You Lost";
            DisabaleLetters();
        }
    }

    private void DisabaleLetters()
    {
        foreach (var child in LettersContainer.Children)
        {
            var btn = child as Button;
            btn.IsEnabled = false;
        }
    }

    private void CheckIfGameWon()
    {
        if (materials.Spotlight.Replace(" ", "") == Answer)
        {
            materials.Message = "You Won";
            DisabaleLetters();
        }
    }

    private void Reset_Clicked(object sender, EventArgs e)
    {
        Mistakes = 0;
        Guessed = new List<char>();
        materials.CurrentImage = "img0.jpg";
        materials.Message = "";
        PickWord();
        CalculateWord(Answer,Guessed);
        UpdateStatus();
        EnableLetters();


    }

    private void EnableLetters()
    {
        foreach (var child in LettersContainer.Children)
        {
            var btn = child as Button;
            btn.IsEnabled = true;
        }
    }
}

