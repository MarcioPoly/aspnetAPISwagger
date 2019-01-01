using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPIImprove.Interfaces;

namespace WebAPIImprove.Models
{
    public class Product : IProduct
    {
        public string GetProductDesciption()
        {
            return "Sample product description";
        }
    }
}