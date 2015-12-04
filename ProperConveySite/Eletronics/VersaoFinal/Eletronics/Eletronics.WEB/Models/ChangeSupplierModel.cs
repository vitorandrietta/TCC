using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eletronics.WEB.Models
{
    public class ChangeSupplierModel
    {
        public IList<SupplierScreenModel> Suppliers { get; set; }
        public IList<SupplierTypeScreenModel> SupplierTypes { get; set; }
    }
}