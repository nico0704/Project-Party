using Project_Party.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project_Party.ViewModels
{
    public class MapPageViewModel : BaseViewModel
    {
        public ObservableCollection<Party> Locations { get; }
        public Command LoadLocationsCommand { get; }
        public MapPageViewModel()
        {
            Locations = new ObservableCollection<Party>();
            ExecuteLoadItemsCommand();
            LoadLocationsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {

                Locations.Clear();
                var locations = await DataStore.GetItemsAsync(true);
                foreach (var location in locations)
                {
                    Locations.Add(location);
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
    }
}
