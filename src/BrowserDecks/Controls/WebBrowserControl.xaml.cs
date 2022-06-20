using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace BrowserDecks.Controls
{
    /// <summary>
    /// Interaction logic for WebBrowserControl.xaml
    /// </summary>
    public partial class WebBrowserControl : INotifyPropertyChanged
    {
        private Uri _webUri = new("about:blank");
        private bool _isTitleBarVisible = true;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsTitleBarVisible
        {
            get => _isTitleBarVisible;
            set
            {
                _isTitleBarVisible = value;
                OnPropertyChanged();
            }
        }

        public Uri WebUri
        {
            get => _webUri;
            set
            {
                _webUri = value.IsAbsoluteUri ? value : new Uri("https://" + value);
                OnPropertyChanged();
            } 
        }

        public WebBrowserControl()
        {
            InitializeComponent();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter when sender is TextBox textBox:
                {
                    var prop = TextBox.TextProperty;
                    var binding = BindingOperations.GetBindingExpression(textBox, prop);
                    binding?.UpdateSource();
                    break;
                }
                case Key.F9 when sender is UserControl:
                    IsTitleBarVisible = !IsTitleBarVisible;
                    break;
            }
        }
    }
}
