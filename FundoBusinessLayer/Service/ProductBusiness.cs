using FundoBusinessLayer.Interface;
using FundoRepositoryLayer.Entity;
using FundoRepositoryLayer.Interface;
using FundoRepositoryLayer.Service;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoBusinessLayer.Service
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepo proRepo;

        
        public ProductBusiness(IProductRepo proRepo)
        {
            this.proRepo = proRepo;
        }
        public Product AddProduct(ProductModel productModel)
        {
            return proRepo.AddProduct(productModel);
        }
        public List<Product> GetProducts()
        {
            return proRepo.GetProducts();
        }
    }
}
