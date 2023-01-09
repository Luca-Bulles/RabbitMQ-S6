﻿using RabbitMQProductAPI.Models;

namespace RabbitMQProductAPI.Services
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProductList();
        public Product GetProductById(int id);
        public Product AddProduct(Product product);
        public Product UpdateProduct(Product product);
        public string DeleteProduct(int id);
    }
}
