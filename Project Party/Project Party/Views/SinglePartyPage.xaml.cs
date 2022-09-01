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
    public partial class SinglePartyPage : ContentPage
    {
        public SinglePartyPage(Party party)
        {
            InitializeComponent();
            this.BindingContext = new SinglePartyViewModel(party);
        }
    }
}