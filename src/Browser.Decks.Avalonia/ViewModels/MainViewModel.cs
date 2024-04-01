using ReactiveUI;
using System;
using System.Windows.Input;

namespace Browser.Decks.Avalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
    private const string DefaultUrl = "https://www.google.com";

    public ICommand MyCommand { get; }

    public string StringUrl { get; set; } = DefaultUrl;

    public Uri Url { get; set; } = new(DefaultUrl);

    public MainViewModel()
    {
        MyCommand = ReactiveCommand.Create(() =>
        {
            if (Uri.TryCreate(StringUrl, UriKind.Absolute, out var newUrl))
            {
                Url = newUrl;
                OnPropertyChanged(nameof(Url));
            }

            if (Uri.TryCreate(StringUrl, UriKind.Relative, out _))
            {
                StringUrl = "https://" + StringUrl;
                Url = new Uri(StringUrl);
                OnPropertyChanged(nameof(Url));
                OnPropertyChanged(nameof(StringUrl));
            }
        });
    }

}
