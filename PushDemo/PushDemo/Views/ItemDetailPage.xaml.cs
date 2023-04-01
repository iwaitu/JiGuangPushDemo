using PushDemo.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PushDemo.Views
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