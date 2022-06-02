using AppMobileProductos.Base;
using AppMobileProductos.Models;
using AppMobileProductos.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppMobileProductos.ViewModels
{
    public class ProductoDetailsViewModel: ViewModelBase
    {
        private ServiceApiProductos service;

        public ProductoDetailsViewModel(ServiceApiProductos service)
        {
            this.service = service;
        }

        public Command DeleteProduct
        {
            get
            {
                return new Command(async() =>
                {
                    int id = this.Producto.IdProducto;
                    await this.service.DeleteProductoAsync(id);
                    await
                    Application.Current.MainPage
                    .DisplayAlert("Alert", "Producto eliminado", "Ok");
                    MessagingCenter.Send<ProductosListViewModel>
                    (App.ServiceLocator.ProductosListViewModel, "RELOAD");
                    await
                    Application.Current.MainPage.Navigation
                    .PopModalAsync();
                });
            }
        }

        private Producto _Producto;

        public Producto Producto
        {
            get { return this._Producto; }
            set {
                this._Producto = value;
                OnPropertyChanged("Producto");
            }
        }
    }
}
