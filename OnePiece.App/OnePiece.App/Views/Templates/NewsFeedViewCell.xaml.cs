using OnePiece.App.DataModels;
using Plugin.DeviceInfo;
using System;
using System.Linq;
using Xamarin.Forms;

namespace OnePiece.App.Views.Templates
{
    public partial class NewsFeedViewCell : ViewCell
    {
        public NewsFeedViewCell()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var context = BindingContext as NewsFeed;
            if (context != null)
            {
                var media = context.Medias.FirstOrDefault();
                if(media != null)
                {
                    MediaPanel.RowHeight = (Convert.ToInt32(App.ScreenWidth) * media.Height / media.Width) - 10; 
                }
            }
        }
    }
}
