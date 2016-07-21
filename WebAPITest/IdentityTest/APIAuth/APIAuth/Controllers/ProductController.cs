using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIAuth.Models;
using APIAuth.DTOs;
using System.Web.Http.Cors;

namespace APIAuth.Controllers
{

    public class ProductController : ApiController
    {
        EmployeeDBEntities eDB = new EmployeeDBEntities();
        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        [Route("api/product")]
        [Authorize]
        public IEnumerable<ProductDetails> GetProductDetails()
        {
            var data = (from p in eDB.Products
                        join c in eDB.Categories
                            on p.CategoryId equals c.CategoryId
                        select new ProductDetails
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            UnitPrice = p.UnitPrice,
                            CategoryId = p.CategoryId,
                            CategoryName = c.CategoryName
                        });
            return data;
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        //[Route("api/Product")]
        [ActionName("ProductById")]
        [Authorize]
        public IEnumerable<ProductDetails> GetProductDetails(int id)
        {
            var data = (from p in eDB.Products
                        where p.ProductId == id
                        join c in eDB.Categories
                             on p.CategoryId equals c.CategoryId
                        select new ProductDetails
                          {
                              ProductId = p.ProductId,
                              ProductName = p.ProductName,
                              UnitPrice = p.UnitPrice,
                              CategoryId = p.CategoryId,
                              CategoryName = c.CategoryName
                          });
            return data;
        }
    }
}
