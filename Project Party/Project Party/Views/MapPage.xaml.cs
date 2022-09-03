
using Plugin.Geolocator;
using Project_Party.Models;
using Project_Party.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

using Xamarin.Forms.Xaml;

namespace Project_Party.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        MapPageViewModel vm;

        private bool ClickedPin = false;
        public MapPage()
        {
            InitializeComponent();
            Map.PinClicked += PinClicked;
            Map.MapClicked += MapClicked;
            vm = new MapPageViewModel();
            GetPins();
            this.BindingContext = vm;
            
            SetMapToUser();
            //SetMapToUser();
        }

        public async void SetMapToUser()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                                         Distance.FromMiles(1)));
        }

        public void GetPins()
        {
            
            var pins = vm.Pins;

            foreach(Pin pin in pins)
            {
                Map.Pins.Add(pin);
            }
            
        }
       
        public void PinClicked (Object sender, PinClickedEventArgs e)
        {
            vm.SelectedPin = e.Pin;
            vm.PinClickedCommand.Execute(null);
            ClickedPin = true;
            Console.WriteLine(vm.SelectedParty.Name);
            Grid.SetRowSpan(Map, 2);
            DetailedParty.IsVisible = true;
        }

        public void MapClicked(Object sender, MapClickedEventArgs e)
        {
            if(ClickedPin == true)
            {
                Grid.SetRowSpan(Map, 3);
                DetailedParty.IsVisible = false;
                ClickedPin = false;
            }
           
        }
    }
}