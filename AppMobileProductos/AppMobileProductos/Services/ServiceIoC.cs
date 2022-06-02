using AppMobileProductos.ViewModels;
using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace AppMobileProductos.Services
{
    public class ServiceIoC
    {
        private IContainer container;

        public ServiceIoC()
        {
            this.RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            ContainerBuilder builder =
                new ContainerBuilder();
            builder.RegisterType<ServiceApiProductos>();
            builder.RegisterType<ProductosListViewModel>();
            builder.RegisterType<ProductoDetailsViewModel>();
            string resourceName = "AppMobileProductos.appsettings.json";
            Stream stream = GetType().GetTypeInfo().Assembly.GetManifestResourceStream(resourceName);
            IConfiguration configuration =
                new ConfigurationBuilder().AddJsonStream(stream)
                .Build();
            builder.Register<IConfiguration>(x => configuration);
            this.container = builder.Build();
        }

        public ProductoDetailsViewModel ProductoDetailsViewModel
        {
            get
            {
                return this.container.Resolve<ProductoDetailsViewModel>();
            }
        }

        //PROPIEDADES CON LOS VIEWMODELS PARA PODER RECUPERARLOS DENTRO 
        //DE LOS CODIGOS XAML
        public ProductosListViewModel ProductosListViewModel
        {
            get
            {
                return this.container.Resolve<ProductosListViewModel>();
            }
        }
    }
}
