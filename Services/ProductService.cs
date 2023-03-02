using AppMvc.Net.Models;
using System.Collections.Generic;

namespace AppMvc.Net.Services
{
    public class ProductService : List<ProductModel>
    {
        public ProductService() 
        {
            this.AddRange(new List<ProductModel>
            {
                new ProductModel() {Id = 1, Name = "IPhone", Price = 1000},
                new ProductModel() {Id = 2, Name = "Samsung", Price = 1200},
                new ProductModel() {Id = 3, Name = "Sony", Price = 890},
                new ProductModel() {Id = 4, Name = "Nokia", Price = 200},
            });
        }
    }
}
