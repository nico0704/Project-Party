
using Plugin.Geolocator;
using Project_Party.Models;
using Project_Party.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

using Xamarin.Forms.Xaml;
using static GoogleApi.GoogleMaps;
using Distance = Xamarin.Forms.GoogleMaps.Distance;

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
            GetStartPins();
            this.BindingContext = vm;
            
            SetMapToUser();
            //SetMapToUser();
            Map.CameraIdled += StoppedMoving;
        }

        public async void SetMapToUser()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                                        Distance.FromMiles(1)));
        }

    
        public void GetStartPins()
        {
            if (vm.Pins == null)
                return;

            var pins = vm.Pins;
            Console.WriteLine(vm.Pins.Count);
            foreach(Pin pin in pins)
            {
                Map.Pins.Add(pin);
            }
            
        }

        public void GetPins()
        {
            if(vm.Pins == null)
                return ;

            var pins = vm.Pins;
            Map.Pins.Clear();
            Console.WriteLine(vm.Pins.Count);
            foreach (Pin pin in pins)
            {
                Map.Pins.Add(pin);
            }

        }

        [Obsolete]
        public void StoppedMoving(Object sender, CameraIdledEventArgs e)
        {
            Console.WriteLine("Target: " + Map.CameraPosition.Target);
            Console.WriteLine("Zoom: " + Map.CameraPosition.Zoom);
            Console.WriteLine("Radius: " + Map.VisibleRegion.Radius.Meters);

            vm.VisibleRegion = Map.VisibleRegion;
            vm.CameraStoppedCommand.Execute(null);
            Task.Run(() =>
            {
                var result = vm.Ready();
                Device.BeginInvokeOnMainThread(() =>
                {
                    
                    Map.Pins.Clear();
                    foreach(var pin in vm.Pins)
                    {
                        Map.Pins.Add(pin);
                    }
                });
            });
            
            Console.WriteLine("Stopped Moving" + Map.Pins.Count);
            
            
        }

        public void PinClicked (Object sender, PinClickedEventArgs e)
        {
            vm.SelectedPin = e.Pin;
            vm.PinClickedCommand.Execute(null);
            ClickedPin = true;
            Console.WriteLine(vm.SelectedParty.Name);
            DetailedParty.IsVisible = true;
        }

        public void MapClicked(Object sender, MapClickedEventArgs e)
        {
            if(ClickedPin == true)
            {
                DetailedParty.IsVisible = false;
                ClickedPin = false;
            }
           
        }

       
    }
}