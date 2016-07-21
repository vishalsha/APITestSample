using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APIAuth.Models;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;


namespace APIAuth.DTOs
{
    public class ProductDetails
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    
}