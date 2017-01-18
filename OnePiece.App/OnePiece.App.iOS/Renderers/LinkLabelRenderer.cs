using Foundation;
using OnePiece.App.Controls;
using OnePiece.App.iOS.Renderers;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LinkLabel), typeof(LinkLabelRenderer))]
namespace OnePiece.App.iOS.Renderers
{
    class LinkLabelRenderer : LabelRenderer
    {
        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            var element = Element as LinkLabel;
            if (Control != null && element != null)
            {
                element.OnClicked(Control, EventArgs.Empty);
            }
        }
    }
}