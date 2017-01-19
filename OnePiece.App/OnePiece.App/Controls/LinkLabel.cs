using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace OnePiece.App.Controls
{
    public class LinkLabel : Label
    {
        private TapGestureRecognizer _tapCommand { get; set; }

        #region Bindable Properties

        public static BindableProperty TapCommandProperty = BindableProperty.Create(nameof(TapCommand),
            typeof(ICommand), typeof(LinkLabel));

        #endregion

        public event EventHandler Clicked;

        public ICommand TapCommand
        {
            get { return (ICommand)GetValue(TapCommandProperty); }
            set { SetValue(TapCommandProperty, value); }
        }

        public void OnClicked(object sender, EventArgs args)
        {
            Clicked?.Invoke(sender, args);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(TapCommand))
            {
                if (_tapCommand != null)
                {
                    GestureRecognizers.Remove(_tapCommand);
                }

                _tapCommand = new TapGestureRecognizer
                {
                    Command = TapCommand
                };

                GestureRecognizers.Add(_tapCommand);
            }
        }
    }
}
