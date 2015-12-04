using Eletronicos.Data;
using Eletronicos.Model;
using Eletronicos.Model.Product;
using Eletronicos.Model.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EletronicStore.WEB.Business
{

    public class PurchaseBO
    {
        private PurchaseDAO purchaseDAO = new PurchaseDAO();
        private ProductDAO productDAO = new ProductDAO();

        public void RegisterPurchase(Purchase purchase)
        {
            try
            {
                if (purchase.ProductQuantity <= 0)
                {
                    throw new ApplicationException("a compra precisa de pelo menos um produto");
                }

                int productQuantity = productDAO.Find
                (new Product() { ProductID = purchase.ProductID }).AvaiableQuantity;

                if (productQuantity - purchase.ProductQuantity < 0)
                {
                    throw new ApplicationException("essa quantidade de produto nao está disponível, a quantidade disponível é:" + productQuantity);
                }

                purchaseDAO.Insert(purchase);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void RemovePurchase(Purchase purchase)
        {
            try
            {
                Purchase purchaseFilled = purchaseDAO.Find(new Purchase() { PurchaseID = purchase.PurchaseID });
                Product product = productDAO.Find(new Product() { ProductID = purchaseFilled.ProductID });
                purchaseDAO.Delete(purchase);
                productDAO.Update(new Product()
                {
                    AvaiableQuantity = product.AvaiableQuantity + purchaseFilled.ProductQuantity,
                    ProductDescription = product.ProductDescription,
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    SupplierId = product.SupplierId
                });
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public IList<Purchase> ClientPurchases(Client client)
        {
            try
            {
                return purchaseDAO.FindAll()
                   .Where(pu => pu.ClientID == client.CPF).ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}