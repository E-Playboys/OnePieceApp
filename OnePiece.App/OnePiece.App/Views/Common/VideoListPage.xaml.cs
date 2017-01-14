using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnePiece.App.ViewModels;
using Xamarin.Forms;

namespace OnePiece.App.Views
{
    public partial class VideoListPage : ContentPage
    {
        private readonly VideoListPageViewModel _viewModel;

        private bool _isDataLoaded;

        /// <summary>
        /// Single-season or Multi-season layout
        /// </summary>
        public bool IsMultiSeason { get; set; }

        /// <summary>
        /// Relative API url to the datasource
        /// </summary>
        public string DataSource { get; set; }

        public VideoListPage()
        {
            InitializeComponent();

            _viewModel = (VideoListPageViewModel) BindingContext;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.IsMultiSeason = IsMultiSeason;
            _viewModel.DataSource = DataSource;

            if (_isDataLoaded)
                return;

            await _viewModel.LoadAsync();
            _isDataLoaded = true;
        }
    }
}
