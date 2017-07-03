using OnePiece.App.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnePiece.App.ViewModels
{
    public abstract class BaseViewModel : BindableBase, INavigationAware
    {
        protected readonly IAppService AppService;

        protected BaseViewModel(IAppService appService)
        {
            AppService = appService;

            NavigateCommand = new DelegateCommand<string>(NavigateAsync);
            OpenUrlCommand = new DelegateCommand<string>(OpenUrlAsync);
        }

        #region Properties

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public bool IsNotBusy
        {
            get { return !_isBusy; }
        }

        #endregion

        #region Commands

        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand<string> OpenUrlCommand { get; set; }

        #endregion

        private async void NavigateAsync(string target)
        {
            await AppService.Navigation.NavigateAsync(target);
        }

        private void OpenUrlAsync(string url)
        {
            Uri uri = new Uri(url);
            Device.OpenUri(uri);
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            CancelTasks();
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        #region Task Safe

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public async Task RunSafe(Task task, bool checkNetworkReachable = false, bool notifyOnError = true)
        {
            //if (checkNetworkReachable && !App.IsNetworkReachable)
            //{
            //    MessagingCenter.Send<BaseViewModel, Exception>(this, AppMessages.EXCEPTION_OCCURRED, new WebException("Please connect to the network!"));
            //    return;
            //}

            Exception exception = null;

            try
            {
                await Task.Run(() =>
                {
                    if (!_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        task.Start();
                        task.Wait(_cancellationTokenSource.Token);
                    }
                });
            }
            catch (TaskCanceledException)
            {
                Debug.WriteLine("Task Cancelled");
            }
            catch (AggregateException e)
            {
                var ex = e.InnerException;
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                exception = ex;
            }
            catch (Exception e)
            {
                exception = e;
            }

            if (exception != null)
            {
                // TODO: Hockey App to report exception
                Debug.WriteLine(exception);

                if (notifyOnError)
                {
                    NotifyException(exception);
                }
            }
        }

        private void NotifyException(Exception exception)
        {
            //MessagingCenter.Send<BaseViewModel, Exception>(this, AppMessages.EXCEPTION_OCCURRED, exception);
        }

        public void CancelTasks()
        {
            if (!_cancellationTokenSource.IsCancellationRequested && _cancellationTokenSource.Token.CanBeCanceled)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }

        #endregion
    }
}
