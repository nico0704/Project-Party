using Google.Apis.Util;
using GoogleApi.Entities.Maps.Common;
using Microsoft.Extensions.Options;
using Project_Party.Models;
using Project_Party.Services;
using Project_Party.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

using Xamarin.Forms.PancakeView;
using Xamarin.Forms.PlatformConfiguration.GTKSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Project_Party.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        double openY = (Device.RuntimePlatform == "Android") ? 20 : 60;

        //Can be between 0 and 1
        byte position = 0;
        private MainPageViewModel vm = new MainPageViewModel();
        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = vm;


        }

        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
           // A bug that after Ive set the SelectedItem, the Detail Page will show second time 
           //Not working any more need to find some other fix
           /*if (listView.SelectedItem == null) 
                   return;
           */
           Party selectedParty = (e.CurrentSelection.FirstOrDefault() as Party);
            Navigation.PushAsync(new SinglePartyPage(selectedParty));
            //listView.SelectedItem = null;


        }

        private void CarouselView_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            /*
            if(e.CurrentItem != null)
            {
                this.CustomTabsView.ScrollTo(e.CurrentItem, null, ScrollToPosition.Center, true);
            }
            */

        }


        private double lastTotalY = 0;
        void PanGestureRecognizer_PanUpdated(System.Object sender, Xamarin.Forms.PanUpdatedEventArgs e)
        {

            PanButtonBottom.IsVisible = false;
            PanButtonTop.IsVisible = false;
            
            //Verhalten bei Position 0
            if (e.StatusType == GestureStatus.Running && this.Height > ( e.TotalY * -1 ) + BottomToolbar.Height && e.TotalY + 10 < BottomToolbar.Height && position == 0)
            {
                
                if (e.TotalY < 0)
                {
                    FilterContent.IsVisible = false;
                    BottomContent.Opacity = (100 - (e.TotalY * -1)) / 100;
                    BottomToolbar.TranslationY = e.TotalY;
                }
                lastTotalY = e.TotalY;
            }
            //Verhalten bei Position 1
            else if (e.StatusType == GestureStatus.Running && position == 1 && e.TotalY >= 0)
            {
                FilterContent.IsVisible = false;
                BottomToolbar.TranslationY = (this.Height - BottomToolbar.Height - e.TotalY) * -1;
                lastTotalY = e.TotalY;
            }

            else if (e.StatusType == GestureStatus.Completed)
            {
                if (position == 0 && lastTotalY < -100 )
                {
                    MoveObject(BottomToolbar, 1);
                }
                else if (position == 1 && lastTotalY > 100)
                {
                    MoveObject(BottomToolbar, 0);
                }else
                {
                    MoveObject(BottomToolbar, position);
                }
                
            }

        }

        public async void MoveObject(Grid grid, byte toPosition)
        {
            if (toPosition == 0)
            {
                PanButtonTop.IsVisible = false;
                PanButtonBottom.IsVisible = true;
                
                Device.BeginInvokeOnMainThread(async () =>
                {

                    grid.TranslateTo(0, 0, 200, Easing.Linear);
                    await BottomContent.FadeTo(100, 200, Easing.Linear);
                });
                position = 0;
            }
               
            else if (toPosition == 1)
            {
                PanButtonTop.IsVisible = true;
                PanButtonBottom.IsVisible = false;
                
                Device.BeginInvokeOnMainThread(async () =>
                {
                    grid.TranslateTo(0, (this.Height - BottomToolbar.Height) * -1, 200, Easing.Linear);
                    await BottomContent.FadeTo(0, 200, Easing.Linear);
                });
                FilterContent.IsVisible = true;
                position = 1;
            }
        }
        private void DragStarted(object sender, DragEventArgs e)
        {
            
        }

        private void FilterButton_Tapped(object sender, EventArgs e)
        {
            MoveObject(BottomToolbar, 1);
        }

        private void MusicFitler_Tapped(object sender, EventArgs e)
        {
            PancakeView pancakeView = sender as PancakeView;
            var text = (pancakeView.GetChildren()[0] as Label).Text;
            if (pancakeView.Border.Color == Color.Gray)
            {
                pancakeView.Border.Color = Color.Purple;
                pancakeView.BackgroundColor = Color.LightPink;
                SQLFilter.Music.Add(text);
            }else
            {
                pancakeView.Border.Color = Color.Gray;
                pancakeView.BackgroundColor = Color.White;
                SQLFilter.Music.Remove(text);
            }

           

        }

        private void filterDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

        void OnToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                Entry_City.IsVisible = true;
                lbl_MyLocation.FontAttributes = FontAttributes.None;
                lbl_CustomLocation.FontAttributes = FontAttributes.Bold;

            } else
            {
                lbl_MyLocation.FontAttributes = FontAttributes.Bold;
                lbl_CustomLocation.FontAttributes = FontAttributes.None;
                Entry_City.IsVisible = false;
            }
                
        }

        private void Switch_CustomDate_Toggled(object sender, ToggledEventArgs e)
        {
            if(e.Value == true)
            {
                DatePicker_CustomDate.IsVisible = true;
            }else
            {
                DatePicker_CustomDate.IsVisible = false;
            }
           
        }

        private void Switch_EntryAge_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                Entry_MinAge.IsVisible = true;
            }
            else
            {
                Entry_MinAge.IsVisible = false;
            }
        }

        private void DatePicker_CustomDate_DateSelected(object sender, DateChangedEventArgs e)
        {
              vm.newDate = e.NewDate;
              vm.UpdateTabVmsCommand.Execute(this);
        }
    }
}