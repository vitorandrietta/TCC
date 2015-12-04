using Eletronicos.Data;
using Eletronicos.Model;
using Eletronicos.Model.Product;
using Eletronicos.Model.Purchase;
using EletronicStore.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eletronic.WebStore.Mapper
{
    public class Mapper
    {
        private ProductDAO productDAO = new ProductDAO();
        private SupplierDAO supplierDAO = new SupplierDAO();


        public ProductVM ProductToProductVM(Product product) {

            return new ProductVM
            {
                AvaiableQuantity = product.AvaiableQuantity,
                ProductDescription = product.ProductDescription,
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                SupplierName = supplierDAO.Find(new Supplier() { CNPJ = product.SupplierId}).CorporateName

            };
        }


        public ProductPurchaseVM PurchaseToProductPurchaseVM(Purchase purchase) {
            Product product = productDAO.Find(new Product() { ProductID = purchase.ProductID});
            return new ProductPurchaseVM()
            {
                
                AvaiableQuantity = purchase.ProductQuantity,
                ProductDescription = product.ProductDescription,
                ProductID = purchase.ProductID,
                ProductName = product.ProductName,
                PurchaseID = purchase.PurchaseID,
                SupplierName = supplierDAO.Find(new Supplier(){CNPJ = product.SupplierId}).CorporateName
                               
                
            };
        }
    }
}






