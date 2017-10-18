using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnePiece.App.Models;
using Xamarin.Forms;

namespace OnePiece.App.Views.Templates
{
    public partial class CachedImageViewCell : ViewCell
    {
        public CachedImageViewCell()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var context = BindingContext as Gif;
            if (context != null)
            {
                CachedImage.Source = context.Url;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var context = BindingContext as Gif;
            if (context != null)
            {
                CachedImage.Source = context.Url.Replace(".gif", ".jpeg");
            }
        }
    }
}
