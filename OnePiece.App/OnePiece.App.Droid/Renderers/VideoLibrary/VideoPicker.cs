using System;
using System.Threading.Tasks;
using Android.Content;
using Xamarin.Forms;
using OnePiece.App.Controls.VideoLibrary;

// Need application's MainActivity
using OnePiece.App.Droid.Renderers;

[assembly: Dependency(typeof(OnePiece.App.Droid.Renderers.VideoLibrary.VideoPicker))]

namespace OnePiece.App.Droid.Renderers.VideoLibrary
{
    public class VideoPicker : IVideoPicker
    {
        public Task<string> GetVideoFileAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("video/*");
            intent.SetAction(Intent.ActionGetContent);

            // Get the MainActivity instance
            MainActivity activity = MainActivity.Current;

            // Start the picture-picker activity (resumes in MainActivity.cs)
            activity.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Video"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            activity.PickImageTaskCompletionSource = new TaskCompletionSource<string>();

            // Return Task object
            return activity.PickImageTaskCompletionSource.Task;
        }
    }
}