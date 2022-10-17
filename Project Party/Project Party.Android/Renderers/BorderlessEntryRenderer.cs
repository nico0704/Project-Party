using Android.Content;
using Android.Views;
using EntryAutoComplete;
using EntryAutoComplete.Sample.Droid.Renderers;
using Project_Party.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace EntryAutoComplete.Sample.Droid.Renderers
{

    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;

                
            }
        }
    }

}