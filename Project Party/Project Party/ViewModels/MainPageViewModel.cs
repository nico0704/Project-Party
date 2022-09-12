using GoogleApi.Entities.Interfaces;
using Org.BouncyCastle.Cms;
using Project_Party.Models;
using Sharpnado.Tabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Project_Party.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {

        public Task KeepaliveTask { get; private set; }

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

        public Command LoadPartysCommand { get; }
        
        public String TestLabel { get; } = "Test"; 

       
        

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
            Title = "Browse";
            PartyList = new ObservableCollection<Party>();
            LoadPartysCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        // Neumachen
        private void SetSelection()
        {
            
            this.TabVms.ForEach(vm => vm.IsSelected = false);
            this.CurrentTabVm.IsSelected = true;
            
        }
        private async Task initDayWeeklist()
        {
            ObservableCollection<TabViewModel>  tabVms = new ObservableCollection<TabViewModel>();
            
            DateTime now = DateTime.Now;
            for(int i = 0; i < 14; i++)
            {
                await ExecuteLoadItemsCommand(now);
                tabVms.Add(new TabViewModel(PartyList, now));
                
                now = now.AddDays(1);
            }
            TabVms = tabVms;
          
            /*
            ObservableCollection<ObservableCollection<Party>> dayWeekList = new ObservableCollection<ObservableCollection<Party>>();
           for(int i = 0; i < 10; i++)
            {
               dayWeekList.Add(PartyList);
            }
            DayWeekList = dayWeekList;
            */
        }
      
        private ObservableCollection<Party> CreateTestList()
        {
            return PartyList;
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
