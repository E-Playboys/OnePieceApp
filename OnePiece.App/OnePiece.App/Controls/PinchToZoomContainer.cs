using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace OnePiece.App.Controls
{
    public class PinchToZoomContainer : ContentView
    {
        private double startScale, currentScale, xOffset, yOffset, startX, startY;

        public PinchToZoomContainer()
        {
            var pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += OnPinchUpdated;
            GestureRecognizers.Add(pinchGesture);
        }

        public void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            Element parent = Parent;
            var content = Content;

            while (parent != null)
            {
                if (parent is ListView)
                {
                    content = parent as ListView;
                    break;
                }
                parent = parent.Parent;
            }

            switch (e.Status)
            {
                case GestureStatus.Started:
                    // Store the current scale factor applied to the wrapped user interface element,
                    // and zero the components for the center point of the translate transform.
                    startScale = content.Scale;
                    content.AnchorX = 0;
                    content.AnchorY = 0;

                    break;

                case GestureStatus.Running:
                    // Calculate the scale factor to be applied.
                    currentScale += (e.Scale - 1) * startScale;
                    currentScale = Math.Max(1, currentScale);

                    // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                    // so get the X pixel coordinate.
                    double renderedX = content.X + xOffset;
                    double deltaX = renderedX / Width;
                    double deltaWidth = Width / (content.Width * startScale);
                    double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                    // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                    // so get the Y pixel coordinate.
                    double renderedY = content.Y + yOffset;
                    double deltaY = renderedY / Height;
                    double deltaHeight = Height / (content.Height * startScale);
                    double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                    // Calculate the transformed element pixel coordinates.
                    double targetX = xOffset - (originX * content.Width) * (currentScale - startScale);
                    double targetY = yOffset - (originY * content.Height) * (currentScale - startScale);

                    // Apply translation based on the change in origin.
                    content.TranslationX = Math.Min(0, Math.Max(targetX, -content.Width * (currentScale - 1)));
                    content.TranslationY = Math.Min(0, Math.Max(targetY, -content.Height * (currentScale - 1)));

                    // Apply scale factor.
                    content.Scale = currentScale;

                    break;

                case GestureStatus.Completed:
                    // Store the translation delta's of the wrapped user interface element.
                    xOffset = content.TranslationX;
                    yOffset = content.TranslationY;

                    break;
            }
        }
    }
}
