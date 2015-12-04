using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletronicos.Model.Product
{
    public class Product
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public int AvaiableQuantity { get; set; }

        public string SupplierId { get; set; }
    }
}
