using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnePiece.App.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("CustomEffects")]
[assembly: ExportEffect(typeof(ColorSliderEffect), "ColorSliderEffect")]
namespace OnePiece.App.iOS.Effects
{
    public class ColorSliderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var slider = (UISlider)Control;
            slider.ThumbTintColor = UIColor.FromRGB(255, 0, 0);
            slider.MinimumTrackTintColor = UIColor.FromRGB(255, 120, 120);
            slider.MaximumTrackTintColor = UIColor.FromRGB(255, 14, 14);
        }

        protected override void OnDetached()
        {
        }
    }
}