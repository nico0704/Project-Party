using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Project_Party.Controls;
using Project_Party.Droid.Renderers;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(BorderlessPicker), typeof(BorderlessPickerRenderer))]
namespace Project_Party.Droid.Renderers
{
    public class BorderlessPickerRenderer : PickerRenderer
    {
        public BorderlessPickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            var picker = e.NewElement;


            if (Control != null)
            {
                Control.Background = null;
                picker.TextColor = Color.Black;
                picker.BackgroundColor = Color.Transparent;
                Control.SetHintTextColor(Android.Graphics.Color.White);
                Control.SetSingleLine(true);
               
                Control.SetTypeface(null, TypefaceStyle.Bold);
                Control.Gravity = GravityFlags.Center;
            }
        }
    }
}