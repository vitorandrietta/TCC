using Eletronicos.Data;
using Eletronicos.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EletronicStore.WEB.Business
{
    public class ProductBOcs
    {
        private ProductDAO productDAO = new ProductDAO();
        public IList<Product> AvaiableProducts()
        {
            return productDAO.FindProductsByFilter(new Product() { AvaiableQuantity = 1 });
        }
    }
}