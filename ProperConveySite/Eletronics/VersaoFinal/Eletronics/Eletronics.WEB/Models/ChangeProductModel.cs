using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eletronics.WEB.Models
{
    public class ChangeProductModel
    {
        public IList<ProductScreenModel> products { get; set; }
        public IList<ProductSupplierScreenModel> suppliers { get; set; }
    }
}