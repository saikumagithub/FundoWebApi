using FundoRepositoryLayer.Context;
using FundoRepositoryLayer.Entity;
using FundoRepositoryLayer.Interface;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoRepositoryLayer.Service
{
    public class ProductRepo : IProductRepo
    {

        private readonly FundoContext productContext;

        public ProductRepo(FundoContext fundoContext)
        {
            this.productContext = fundoContext;
        }

        public Product AddProduct(ProductModel productModel)
        {
            Product product = new Product();
            product.ProductName = productModel.ProductName;
            product.brand = productModel.brand;
            product.Price = productModel.Price;
            product.Quantity = productModel.Quantity;

            productContext.Products.Add(product);
            productContext.SaveChanges();

            return product;
        }
        public List<Product> GetProducts()
        {
            var results = productContext.Products.ToList();
            if(results.Count > 0)
            {
                return results;
            }
            return null;
        }
    }
}
