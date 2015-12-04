using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eletronics.WEB.Models
{
    public class ProductScreenModel
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public int AvaiableQuantity { get; set; }

        public string SupplierId { get; set; }

        public string SupplierName { get; set; }
    }
}