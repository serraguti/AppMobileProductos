using AppMobileProductos.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobileProductos
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ProductosListView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
