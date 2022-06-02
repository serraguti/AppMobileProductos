using AppMobileProductos.Base;
using AppMobileProductos.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppMobileProductos.ViewModels
{
    public class ProductoDetailsViewModel: ViewModelBase
    {
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
