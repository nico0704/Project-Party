﻿using GoogleApi;
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
using Xamarin.Forms.PancakeView;

namespace Project_Party.ViewModels
{
    public class MapPageViewModel : BaseViewModel
    {
        public Command LoadLocationsCommand { get; }
        
        
        public Command PinClickedCommand { get; }

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
        
        /*
        public Map Map { get; private set; }

        
        public Grid Content { get; private set; }
        */
        public MapPageViewModel()
        {
            /*
            Content = new Grid();
            
            Map = new Map();
            
            SetupContent();
            */
            Title = "Map";
            Pins = new LinkedList<Pin>();
            ExecuteLoadItemsCommand();
            PinClickedCommand = new Command(async () => await PinClicked());
            LoadLocationsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            //Map.PinClicked += PinClicked;
        }


        /*
      async Task SetupContent()
      {

          ExecuteLoadItemsCommand();

          RowDefinition row = new RowDefinition();
          Content.RowDefinitions.Add(row);

          row = new RowDefinition();

          row.Height = 10;
          Content.RowDefinitions.Add(row);
          row = new RowDefinition();

          row.Height = 270;
          Content.RowDefinitions.Add(row);


          Content.Children.Add(Map);
          Grid.SetRow(Map, 0);
          Grid.SetRowSpan(Map, 3);
      }

      async Task ShowParty()
      {
          PancakeView frame = new PancakeView();
          frame.CornerRadius = new CornerRadius(10, 10, 0,0);
          frame.Margin = 0;
          frame.Padding = 0;
          frame.BackgroundColor = Color.White;

          Content.BackgroundColor = Color.White;

          //Small the Map
          Grid.SetRowSpan(Map, 2);

          //Frame gets bigger
          Grid.SetRow(frame, 1);
          Grid.SetRowSpan(frame, 2);

          Grid grid = new Grid();
          frame.Content = grid;

          RowDefinition row = new RowDefinition();
          grid.RowDefinitions.Add(row);

          row = new RowDefinition();
          row.Height = 15;
          grid.RowDefinitions.Add(row);

          row = new RowDefinition();
          grid.RowDefinitions.Add(row);

          Label label = new Label()
          {
              Text = "Test",

          } ;
          Image image = new Image();
          image.Source = ImageSource.FromUri(new Uri("https://st.depositphotos.com/1002111/2556/i/600/depositphotos_25565783-stock-photo-young-party-people.jpg"));
          image.Aspect = Aspect.AspectFill;

          grid.Children.Add(image);
          Grid.SetRow(image, 0);
          Grid.SetRowSpan(image, 2);
          //grid.Children.Add(label);

          Content.Children.Add(frame);


      }
      */
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

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true; 
            try
            {
                Pins.Clear();
                
                var locations = await DataStore.GetItemsAsync(20);
                
                foreach (var location in locations)
                {
                    Pins.AddLast(new Pin()
                    {
                        Label = location.Name,
                        Position = location.PartyPositon,
                        //Icon = BitmapDescriptorFactory.DefaultMarker(Color.Gray),
                        Type = PinType.Place,
                        Icon = BitmapDescriptorFactory.FromBundle("location"),
                        Tag = location.Id
                    }) ;
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
