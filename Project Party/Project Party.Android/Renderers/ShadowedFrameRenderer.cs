﻿using Android.Content;
using Android.Graphics;
using Android.Support.V4.View;
using EntryAutoComplete;
using EntryAutoComplete.Sample.Droid.Renderers;
using Project_Party.Controls;
using Xamarin.Forms;
using FrameRenderer = Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer;

[assembly: ExportRenderer(typeof(ShadowedFrame), typeof(ShadowedFrameRenderer))]

namespace EntryAutoComplete.Sample.Droid.Renderers
{
    public class ShadowedFrameRenderer : FrameRenderer
    {
        public ShadowedFrameRenderer(Context context) : base(context)
        {
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            //UpdateShadow();
        }

        private void UpdateShadow()
        {
            // we need to reset the StateListAnimator to override the setting of Elevation on touch down and release.
            Control.StateListAnimator = new Android.Animation.StateListAnimator();

            // set the elevation manually
            /*
            ViewCompat.SetElevation(this, 4.0f);
            ViewCompat.SetElevation(Control, 4.0f);
            */
        }
    }
}