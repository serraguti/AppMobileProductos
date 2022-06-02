using AppMobileProductos.Base;
using AppMobileProductos.Models;
using AppMobileProductos.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMobileProductos.ViewModels
{
    public class ProductosListViewModel: ViewModelBase
    {
        private ServiceApiProductos service;
        public ProductosListViewModel()
        {
            this.service = new ServiceApiProductos();
        }

        public Command CargarProductos
        {
            get
            {
                return new Command(async() =>
                {
                    await this.LoadProductosAsync();
                });
            }
        }

        private async Task LoadProductosAsync()
        {
            List<Producto> productos =
                await this.service.GetProductosAsync();
            this.Productos =
                new ObservableCollection<Producto>(productos);
        }

        private ObservableCollection<Producto> _Productos;

        public ObservableCollection<Producto> Productos
        {
            get { return this._Productos; }
            set {
                this._Productos = value;
                OnPropertyChanged("Productos");
            }
        }
    }
}
