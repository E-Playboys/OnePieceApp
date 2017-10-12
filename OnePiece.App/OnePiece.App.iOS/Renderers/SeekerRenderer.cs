using OnePiece.App.Controls;
using OnePiece.App.iOS.Renderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Seeker), typeof(SeekerRenderer))]
namespace OnePiece.App.iOS.Renderers
{
    public class SeekerRenderer : SliderRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);
            var slider = Control;
            // Cast your element here
            var element = (Seeker)Element;

            slider.TouchDown += (sender, args) =>
            {
                element.TouchDownEvent(this, EventArgs.Empty);
            };
            slider.TouchUpInside += (sender, args) =>
            {
                element.TouchUpEvent(this, EventArgs.Empty);
            };
            slider.TouchUpOutside += (sender, args) =>
            {
                element.TouchUpEvent(this, EventArgs.Empty);
            };
        }
    }
}
