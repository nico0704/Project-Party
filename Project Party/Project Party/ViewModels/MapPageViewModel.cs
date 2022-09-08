using GoogleApi;
using Plugin.Geolocator;
using Project_Party.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.PancakeView;

namespace Project_Party.ViewModels
{
    public class MapPageViewModel : BaseViewModel
    {
        public Command LoadLocationsCommand { get; }


        public Command PinClickedCommand { get; }

        public Command CameraStoppedCommand { get; }

        MapSpan visibleRegion;

        public bool isBusy = false; 
        public MapSpan VisibleRegion
        {
            get { return visibleRegion; }
            set
            {
                SetProperty(ref visibleRegion, value);
            }
        }
        Pin selectedPin;
        public Pin SelectedPin
        {
            get { return selectedPin; }
            set
            {
                SetProperty(ref selectedPin, value);
            }
        }

        Party selectedParty;
        public Party SelectedParty
        {
            get { return selectedParty; }
            set
            {
                SetProperty(ref selectedParty, value);
            }
        }

        LinkedList<Pin> pins;
        public LinkedList<Pin> Pins
        {
            get { return pins; }
            set
            {
                SetProperty(ref pins, value);
            }
        }

        public MapPageViewModel()
        {

            Title = "Map";
            Pins = new LinkedList<Pin>();
            
            CameraStoppedCommand = new Command(async () => await CameraStopped());
            PinClickedCommand = new Command(async () => await PinClicked());
            LoadLocationsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            //Map.PinClicked += PinClicked;
        }

        async Task PinClicked()
        {
            if (selectedPin != null)
            {
                try
                {
                    SelectedParty = await DataStore.GetItem(Int32.Parse(SelectedPin.Tag.ToString()));
                    Console.WriteLine("Selected Party " + selectedPin.Tag + " Party Name " + SelectedParty.Name);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.ToString());
                }

            }
        }

        async Task CameraStopped()
        {
            isBusy = true;
            if (VisibleRegion == null)
                          return;

            var locations = await DataStore.GetItemsAsync(VisibleRegion);
            Pins = SetPinsByList(locations);
            isBusy = false;
        }
        async Task ExecuteLoadItemsCommand()
        {

            IsBusy = true;
            try
            {
                var locations = await DataStore.GetItemsAsync(20);
                Pins = SetPinsByList(locations);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }


        public LinkedList<Pin> SetPinsByList( IEnumerable<Party> locations )
        {
            Pins.Clear();
            var _pins = new LinkedList<Pin>();
            foreach ( var location in locations )
            {
                _pins.AddLast(new Pin( )
                {
                    Label = location.Name,
                    Position = location.PartyPositon,
                    //Icon = BitmapDescriptorFactory.DefaultMarker(Color.Gray),
                    Type = PinType.Place,
                    Icon = BitmapDescriptorFactory.FromBundle( "location" ),
                    Tag = location.Id
                });
            }
            return _pins;
        }

        public bool Ready()
        {
            while(isBusy == true)
            {
                Thread.Sleep(100);
            }
           
            return true;
           
        }
        /*
        public async void SetMapToUser()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                                         Distance.FromMiles(1)));
        }
        
        public void SetMapStyle(MapType mapType)
        {
            
            Map.MapType = mapType;
        }
        */

       
    }
}
