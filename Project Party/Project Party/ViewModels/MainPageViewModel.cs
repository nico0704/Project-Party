
using Project_Party.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using GooglePlaces.Xamarin;
using Project_Party.Controls;
using Org.BouncyCastle.Security;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;

namespace Project_Party.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {

        public Command UpdateAdressListCommand { get; }
        SearchMode _searchMode = SearchMode.All;

        public SearchMode SearchMode
        {
            get => _searchMode;
            set
            {
                SetProperty(ref _searchMode, value);
            }
        }

        string _searchAdress = "";
        public string SearchAdress
        {
            get => _searchAdress;
            set
            {
                Console.WriteLine(value);
                SetProperty(ref _searchAdress, value);
                UpdateAdressListCommand.Execute(value);
            }
        }

        ObservableCollection<string> _adressList = new ObservableCollection<string>();
        public ObservableCollection<string> AdressList
        {
            get
            {
                return _adressList;
            }
            set
            {
                SetProperty(ref _adressList, value);
            }
        }

        private DateTime _newDate;
        public DateTime newDate
        {
            get
            {
                return _newDate;
            }
            set
            {
                SetProperty(ref _newDate, value);
            }
        }


        ObservableCollection<Party> _partyList;
        public ObservableCollection<Party> PartyList
        {
            get
            {
                return _partyList;
            }
            set
            {
                SetProperty(ref _partyList, value);
            }
        }


        public Command InitPickerListCommand { get; }
        ObservableCollection<Distance> _pickerList;
        public ObservableCollection<Distance> PickerList
        {
            get
            {
                return _pickerList;
            }
            set
            {
                SetProperty(ref _pickerList, value);
            }
        }

        private Distance _selectedDistance;
        public Distance SelectedDistance
        {
            get => _selectedDistance;
            set
            {
                
                Console.WriteLine(value.Name);
                SetProperty(ref _selectedDistance, value);
            }
        }

        public Command LoadPartysCommand { get; }
        public Command UpdateTabVmsCommand { get; }



        public ObservableCollection<TabViewModel> _tabVms;
        public ObservableCollection<TabViewModel> TabVms
        {
            get => _tabVms;
            set
            {
                SetProperty(ref _tabVms, value);
            }
        }

        private TabViewModel _currentTabVm;
        public TabViewModel CurrentTabVm
        {
            get => _currentTabVm;
            set
            {
                SetProperty(ref _currentTabVm, value);
                SetSelection();
            }
        }

        public  MainPageViewModel()
        {
            Task.Factory.StartNew(() => initDayWeeklist());

            UpdateAdressListCommand = new Command(async () => await UpdateAdressList(_searchAdress));
            Title = "Browse";
            PartyList = new ObservableCollection<Party>();
            InitPickerListCommand = new Command(async () => await InitPickerList());
            InitPickerListCommand.Execute(this);
            UpdateTabVmsCommand = new Command(async () => await UpdateTabVms());
            LoadPartysCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task UpdateTabVms()
        {

            await ExecuteLoadItemsCommand(newDate);
            TabViewModel tabvm = new TabViewModel(PartyList, newDate);
            TabVms.Insert(0, tabvm);
            CurrentTabVm = tabvm;
            this.TabVms = TabVms;
        }

        // Neumachen
        private void SetSelection()
        {
    
            this.TabVms.ForEach(vm => vm.IsSelected = false);
            this.CurrentTabVm.IsSelected = true;    
        }

        private async Task InitPickerList()
        {
            ObservableCollection<Distance> distances = new ObservableCollection<Distance>();
            
            for (int i = 0; i <= 8; i++)
            {
                Distance distance;
                if (i == 0)
                    distance = new Distance("Ganzer Ort", 0);
                else if(i == 1)
                    distance = new Distance("+ 500m", 500);
                    
                else if (i == 2)
                    distance = new Distance("+ 1km", 1000);
                else if (i == 3)
                    distance = new Distance("+ 2km", 2000);
                else if (i == 4)
                    distance = new Distance("+ 5km", 5000);
                else if (i == 5)
                    distance = new Distance("+ 10km", 10000);
                else if (i == 6)
                    distance = new Distance("+ 30km", 30000);
                else if (i == 7)
                    distance = new Distance("+ 50km", 50000);
                else if (i == 8)
                    distance = new Distance("+ 100km", 100000);
                else
                    distance = new Distance("+ Fehler", 0);
                if (i == 0)
                    SelectedDistance = distance;

                distances.Add(distance);
            }
            
            PickerList = distances;
        }
        private async Task UpdateAdressList(String placeText)
        {
            PlacesAutocomplete autocompleteObject = new PlacesAutocomplete("AIzaSyCNStgUqAq-Qps0OXMR8vLi8fwNazgq_E0");
            Predictions predictions = await autocompleteObject.GetAutocomplete(placeText);

           
            ObservableCollection<String> list = new ObservableCollection<string>(); 
            foreach(Prediction prediction in predictions.predictions)
            {
                list.Add(prediction.Description);
            }

            AdressList = list;

        }

        private async Task initDayWeeklist()
        {
            ObservableCollection<TabViewModel> tabVms = new ObservableCollection<TabViewModel>();
            
            DateTime now = DateTime.Now;
            for (int i = 0; i < 14; i++)
            {
                await ExecuteLoadItemsCommand(now);
                tabVms.Add(new TabViewModel(PartyList, now));

                now = now.AddDays(1);
            }
            TabVms = tabVms;
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {

                PartyList = new ObservableCollection<Party>();
                var partys = await DataStore.GetItemsAsync();
                foreach (var party in partys)
                {
                    PartyList.Add(party);
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

        async Task ExecuteLoadItemsCommand(DateTime time)
        {
            IsBusy = true;
            ObservableCollection<Party> partyList = new ObservableCollection<Party>();
            try
            {
                
                var partys = await DataStore.GetItemsAsync(time);
                foreach (var party in partys)
                {
                    partyList.Add(party);
                }
                this.PartyList = partyList;
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
