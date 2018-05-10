using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using OnePiece.App.Controls;
using OnePiece.App.Droid.Renderers;

[assembly: ExportRenderer(typeof(GradientStackLayout), typeof(GradientStackLayoutRenderer))]
namespace OnePiece.App.Droid.Renderers
{
    public class GradientStackLayoutRenderer : VisualElementRenderer<StackLayout>
    {
        private Color StartColor { get; set; }
        private Color EndColor { get; set; }
        private bool IsHorizontal { get; set; }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            var gradient = this.IsHorizontal ? new Android.Graphics.LinearGradient(0, 0, Width, 0,
                   this.StartColor.ToAndroid(),
                   this.EndColor.ToAndroid(),
                   Android.Graphics.Shader.TileMode.Clamp) : new Android.Graphics.LinearGradient(0, 0, 0, Height,
                   this.StartColor.ToAndroid(),
                   this.EndColor.ToAndroid(),
                   Android.Graphics.Shader.TileMode.Clamp);

            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                var stack = e.NewElement as GradientStackLayout;
                this.StartColor = stack.StartColor;
                this.EndColor = stack.EndColor;
                this.IsHorizontal = stack.IsHorizontal;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }
    }
}