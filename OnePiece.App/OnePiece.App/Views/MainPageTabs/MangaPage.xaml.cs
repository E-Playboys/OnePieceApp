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

            ToolbarItems.Add(new IconToolbarItem
            {
                IconColor = Color.White,
                Icon = "typcn-filter",
                Command = new Command(ShowChapterPicker)
            });

            ChapterPicker.SelectedIndexChanged += ChapterPicker_SelectedIndexChanged;
        }

        private async void ChapterPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var context = BindingContext as MangaPageViewModel;
            if (context == null || ChapterPicker.SelectedIndex == -1) return;
            var item = ChapterPicker.Items[ChapterPicker.SelectedIndex];
            var chapterId = context.ChapterNameIdMap[item];

            await context.OpenChapter(chapterId);
            ChapterPicker.Unfocus();
        }

        private void ShowChapterPicker(object obj)
        {
            ChapterPicker.Focus();
        }

        protected override async void OnAppearing()
        {
            var context = BindingContext as MangaPageViewModel;
            if (context != null)
            {
                await context.LoadMangas();

                // Initialize chapter picker
                await context.LoadChapterPicker();
                var chapterNames = context.ChapterNameIdMap.Select(r => r.Key);

                foreach (var chapterName in chapterNames)
                {
                    ChapterPicker.Items.Add(chapterName);
                }
            }
            base.OnAppearing();
        }
    }
}
