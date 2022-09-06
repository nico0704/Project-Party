
using System.Collections.Concurrent;
using Xamarin.Forms.GoogleMaps.Android.Factories;
using Xamarin.Forms.GoogleMaps;
using AndroidBitmapDescriptor = Android.Gms.Maps.Model.BitmapDescriptor;
using AndroidBitmapDescriptorFactory = Android.Gms.Maps.Model.BitmapDescriptorFactory;
using Project_Party.Droid;

namespace XFGoogleMapSample.Droid
{
    public sealed class CachingNativeBitmapDescriptorFactory : IBitmapDescriptorFactory
    {
        //private readonly ConcurrentDictionary<string, AndroidBitmapDescriptor> _cache
           // = new ConcurrentDictionary<string, AndroidBitmapDescriptor>();

        public AndroidBitmapDescriptor ToNative(BitmapDescriptor descriptor)
        {

            /*
            var defaultFactory = DefaultBitmapDescriptorFactory.Instance;

            if (!string.IsNullOrEmpty(descriptor.Id))
            {
                var cacheEntry = _cache.GetOrAdd(descriptor.Id, _ => defaultFactory.ToNative(descriptor));
                return cacheEntry;
            }

            return defaultFactory.ToNative(descriptor);
            */

            int resId = 0;
            switch (descriptor.Id)
            {
                case "location":
                    resId = Resource.Drawable.location;
                    break;
                case "type1":
                    resId = Resource.Drawable.location;
                    break;
            }

            return AndroidBitmapDescriptorFactory.FromResource(resId);
        }
    }
}