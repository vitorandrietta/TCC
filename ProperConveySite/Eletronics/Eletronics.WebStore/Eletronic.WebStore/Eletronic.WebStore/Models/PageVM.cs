using Eletronicos.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EletronicStore.WEB.Models
{
    public class PageVM
    {
        public IEnumerable<ProductVM> products { get; set; }
        public int TotalOfPages { get; set; }
        public int currentPage { get; set; }
    }
}