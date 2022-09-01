using GoogleApi;
using Plugin.Geolocator;
using Project_Party.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Bindings;

namespace Project_Party.ViewModels
{
    public class MapPageViewModel : BaseViewModel
    {
        public Command LoadLocationsCommand { get; }
        public Map Map { get; private set; }
        public MapPageViewModel()
        {
            Map = new Map();
            
            Map.IsShowingUser = true;
            Map.MyLocationEnabled = true;
            SetMapToUser();
            SetMapStyle(MapType.Street);
            ExecuteLoadItemsCommand();
            LoadLocationsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {

                Map.Pins.Clear();
                
                var locations = await DataStore.GetItemsAsync(20);
                foreach (var location in locations)
                {
                    Map.Pins.Add(new Pin()
                    {
                        Label = location.Name,
                        Position = location.PartyPositon,
                    });
                }
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

    }
}
