using AppMobileProductos.Base;
using AppMobileProductos.Models;
using AppMobileProductos.Services;
using AppMobileProductos.Views;
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
        public ProductosListViewModel(ServiceApiProductos service)
        {
            this.service = service;
        }

        public Command DeleteProduct
        {
            get
            {
                return new Command(async(idProducto) =>
                {
                    int id = (int)idProducto;
                    await this.service.DeleteProductoAsync(id);
                    await Application.Current.MainPage.DisplayAlert("Alert"
                        , "Producto eliminado", "Ok");
                    //RECARGAMOS LOS PRODUCTOS
                    await this.LoadProductosAsync();
                });
            }
        }

        public Command ShowProductDetails
        {
            get
            {
                return new Command(async(idProducto) =>
                {
                    int id = (int)idProducto;
                    //BUSCAMOS EL PRODUCTO DENTRO DEL API
                    Producto producto = await this.service.FindProductoAsync(id);
                    //CREAMOS EL VIEWMODEL
                    ProductoDetailsViewModel viewmodel =
                    new ProductoDetailsViewModel();
                    //INDICAMOS AL VIEWMODEL QUE ELEMENTO DIBUJARA
                    viewmodel.Producto = producto;
                    //CREAMOS LA VISTA DE DETALLES
                    ProductoDetailsView view = new ProductoDetailsView();
                    //ENLAZAMOS LA VISTA CON SU VIEWMODEL
                    view.BindingContext = viewmodel;
                    //MOSTRAMOS LA VISTA PARA VISUALIZAR SUS DATOS
                    await Application.Current.MainPage.Navigation
                    .PushModalAsync(view);
                });
            }
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
