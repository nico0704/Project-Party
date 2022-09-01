using Project_Party.Models;
using Project_Party.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project_Party.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel();
        }

        void  OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // A bug that after Ive set the SelectedItem, the Detail Page will show second time 
            if(listView.SelectedItem == null) 
                    return;

            Party selectedParty = (e.CurrentSelection.FirstOrDefault() as Party);
            Navigation.PushAsync(new SinglePartyPage(selectedParty));

            listView.SelectedItem = null;


        }


    }
}