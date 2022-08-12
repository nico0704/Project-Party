
using Plugin.Geolocator;
using Project_Party.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Project_Party.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            this.BindingContext = new MapPageViewModel();
            SetMapOnCurrentPosition();


        }

        private async void SetMapOnCurrentPosition()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(new TimeSpan(10000));
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                                         Distance.FromMiles(1)));
        }
    }
}