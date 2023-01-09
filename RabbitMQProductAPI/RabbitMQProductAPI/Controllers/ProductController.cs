using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQProductAPI.Models;
using RabbitMQProductAPI.RabbitMQ;
using RabbitMQProductAPI.Services;
using System.Reflection.Metadata.Ecma335;

namespace RabbitMQProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IRabbitMQProducer _rabbitMQProducer;
        public ProductController(IProductService _productService, IRabbitMQProducer rabbitMQProducer)
        {
            productService = _productService;
            _rabbitMQProducer = rabbitMQProducer;
        }

        [HttpGet("productlist")]
        public IEnumerable<Product> ProductList()
        {
            var productList = productService.GetProductList();
            return productList;
        }

        [HttpGet("getproductbyid")]
        public Product GetProduct(int id)
        {
            return productService.GetProductById(id);
        }

        [HttpPost("addproduct")]
        public Product AddProduct(Product product)
        {
            var productData = productService.AddProduct(product);
            //Send data to queue for consumer
            _rabbitMQProducer.SendProductMessage(productData);
            return productData;
        }

        [HttpPut("updateproduct")]
        public Product UpdateProduct(Product product)
        {
            return productService.UpdateProduct(product);
        }

        [HttpDelete("deleteproduct")]
        public string DeleteProduct(int id) 
        {
            productService.DeleteProduct(id);
            return "Product Deleted";
        }

    }
}
