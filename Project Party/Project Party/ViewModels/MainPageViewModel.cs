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
    public class MainPageViewModel : BaseViewModel
    {
        private Party _selectedParty;
        public ObservableCollection<Party> PartyList { get; }

        public Command LoadPartysCommand { get; }

        public MainPageViewModel()
        {
            Title = "Browse";
            PartyList = new ObservableCollection<Party>();
            ExecuteLoadItemsCommand();
            LoadPartysCommand = new Command(async () => await ExecuteLoadItemsCommand());
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

                PartyList.Clear();
                var partys = await DataStore.GetItemsAsync(true);
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
    }
}
