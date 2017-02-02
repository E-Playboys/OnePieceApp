using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnePiece.App.Controls
{
    public class VideoView : ContentView
    {
        public static readonly BindableProperty AutoPlayProperty = BindableProperty.Create(nameof(AutoPlay), typeof(bool), typeof(VideoView), false);
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(ImageSource), typeof(VideoView));

        public bool AutoPlay
        {
            get { return (bool) GetValue(AutoPlayProperty); }
            set { SetValue(AutoPlayProperty, value); }
        }

        [TypeConverter(typeof(ImageSourceConverter))]
        public ImageSource Source
        {
            get { return (ImageSource) GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
    }
}
