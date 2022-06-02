using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using AppMobileProductos.Models;
using Newtonsoft.Json;

namespace AppMobileProductos.Services
{
    public class ServiceApiProductos
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue Header;


        public ServiceApiProductos()
        {
            this.ApiUrl = "https://apiproductoslabs.azurewebsites.net/";
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Uri uri = new Uri(this.ApiUrl + request);
                HttpResponseMessage response = 
                    await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    T data =
                        await response.Content.ReadAsAsync<T>();
                    return data;
                }else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            string request = "/api/productos";
            List<Producto> products = await CallApiAsync<List<Producto>>(request);
            return products;
        }

        public async Task<Producto> FindProductoAsync(int id)
        {
            string request = "/api/productos/" + id;
            Producto producto = await CallApiAsync<Producto>(request);
            return producto;
        }

        public async Task AddProductoAsync(Producto producto)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/productos";
                Uri uri = new Uri(this.ApiUrl + request);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(producto);
                StringContent content = 
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(uri, content);
            }
        }

        public async Task UpdateProductoAsync(Producto producto)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/productos";
                Uri uri = new Uri(this.ApiUrl + request);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(producto);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(uri, content);
            }
        }

        public async Task DeleteProductoAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/productos/" + id;
                Uri uri = new Uri(this.ApiUrl + request);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.DeleteAsync(uri);
            }
        }
    }
}
