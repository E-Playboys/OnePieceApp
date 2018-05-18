using OnePiece.App.Controls;
using OnePiece.App.ViewModels;
using System.Linq;
using Xamarin.Forms;
using System;
using FormsPlugin.Iconize;
using OnePiece.App.Models;

namespace OnePiece.App.Views
{
    public partial class MangaPage : TabContentPage
    {
        public MangaPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var context = BindingContext as MangaPageViewModel;
            if (context != null)
            {
                await context.LoadMangas();
            }
            base.OnAppearing();
        }
    }
}
