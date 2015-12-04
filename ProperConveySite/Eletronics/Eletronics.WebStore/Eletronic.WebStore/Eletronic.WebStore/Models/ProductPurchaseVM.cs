using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EletronicStore.WEB.Models
{
    public class ProductPurchaseVM
    {
        public string SupplierName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int AvaiableQuantity { get; set; }
        public int PurchaseID { get; set; }
    
    }
}