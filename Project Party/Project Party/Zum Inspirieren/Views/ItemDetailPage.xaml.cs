using Project_Party.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Project_Party.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}