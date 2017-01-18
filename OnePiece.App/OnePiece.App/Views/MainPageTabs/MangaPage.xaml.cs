using OnePiece.App.Controls;
using OnePiece.App.ViewModels;
using System.Linq;
using Xamarin.Forms;
using System;
using FormsPlugin.Iconize;
using System.Collections.Generic;
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
            var index = context.ChapterNameIdMap[item];

            var book = new MangaBook
            {
                Title = "Magi",
                ChapterNum = $"Chapter {index}",
                PrevChapter = index - 1,
                NextChapter = index + 1
            };
            await context.OpenChapter(book);
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
                if (!context.MangaBooks.Any())
                {
                    await context.LoadMangaBooks();
                }

                // Load chapter picker
                var chapterPickerList = await context.LoadChapterPicker();
                foreach (var chap in chapterPickerList)
                {
                    ChapterPicker.Items.Add(chap);
                }
            }
            base.OnAppearing();
        }
    }
}
