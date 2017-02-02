using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace OnePiece.App.Droid.Helpers
{
    //Create static values for the ActionBar and DecorView
    //These will be utilized to hide the notification bar and ActionBar for fullscreen
    public static class StatusBarHelper
    {
        public static View DecorView
        {
            get;
            set;
        }
        public static ActionBar AppActionBar
        {
            get;
            set;
        }
    }
}