using FundoRepositoryLayer.Entity;
using ModelLayer.Models;

namespace FundoBusinessLayer.Interface
{
    public interface IProductBusiness
    {
        Product AddProduct(ProductModel productModel);
        List<Product> GetProducts();
    }
}