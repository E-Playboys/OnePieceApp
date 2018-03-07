using System;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using OnePiece.App.Controls;
using Xamarin.Forms;
using OnePiece.App.Droid.Renderers;
using Android.Content;

[assembly: ExportRenderer(typeof(Seeker), typeof(SeekerRenderer))]
namespace OnePiece.App.Droid.Renderers
{
    public class SeekerRenderer : SliderRenderer
    {
        public SeekerRenderer(Context context) : base(context)
        {
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            // Cast your element here
            var element = (Seeker)Element;
            if (Control != null)
            {
                var seekBar = Control;

                seekBar.StartTrackingTouch += (sender, args) =>
                {
                    element.TouchDownEvent(this, EventArgs.Empty);
                };

                seekBar.StopTrackingTouch += (sender, args) =>
                {
                    element.TouchUpEvent(this, EventArgs.Empty);
                };
                // On Android you need to check if ProgressChange by user
                seekBar.ProgressChanged += delegate (object sender, SeekBar.ProgressChangedEventArgs args)
                {
                    if (args.FromUser)
                        element.Value = (element.Minimum + ((element.Maximum - element.Minimum) * (args.Progress) / 1000.0));
                };
            }
        }
    }
}