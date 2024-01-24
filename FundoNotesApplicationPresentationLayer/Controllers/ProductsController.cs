using FundoBusinessLayer.Interface;
using FundoRepositoryLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;

namespace FundoNotesApplicationPresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductBusiness proBusiness;

        // intializing a constructor and creating a object through dependency injection
        public ProductsController(IProductBusiness proBusiness)
        {
            this.proBusiness = proBusiness;
        }



        [HttpGet]
        [Route("getallproducts")]
        public ActionResult GetAllProductsController()
        {
            var result = proBusiness.GetProducts();
            if (result != null)
            {
                return Ok(new ResponseModel<List<Product>> { Success = true, Message = "product details", Data =result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "fetching details failed", Data = "opeartion failed" });
            }
        }
        [HttpPost]
        [Route("addpro")]
        public ActionResult AddProductController(ProductModel product)
        {


            var result = proBusiness.AddProduct(product);

            if (result != null)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "product is added", Data = "operation successfull" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "product added failed", Data = "opeartion failed" });
            }
        }
    }
}
