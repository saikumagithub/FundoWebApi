using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public  class ProductModel
    {
        public string ProductName { get; set; }

        public string brand { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
