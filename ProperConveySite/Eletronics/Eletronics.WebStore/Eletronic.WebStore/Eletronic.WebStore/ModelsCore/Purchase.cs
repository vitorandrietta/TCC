using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eletronicos.Model.Purchase
{
    public class Purchase
    {
        public int PurchaseID { get; set; }
        public int ProductQuantity { get; set; }
        public string ClientID { get; set; }
        public int ProductID { get; set; }
        public int PurchaseStatus { get; set; }
    }
}
