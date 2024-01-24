using FundoRepositoryLayer.Entity;
using ModelLayer.Models;

namespace FundoRepositoryLayer.Interface
{
    public interface IProductRepo
    {
        Product AddProduct(ProductModel productModel);
        List<Product> GetProducts();
    }
}