using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eletronicos.Data;
using Eletronicos.Model.Product;
using Eletronicos.Model.Purchase;
using Eletronicos.Model;
namespace Eletronics.WEB.Business
{

    public class ProductBO
    {
        private ProductDAO productDAO = new ProductDAO();
        private SupplierDAO supplierDAO = new SupplierDAO();
        private PurchaseDAO purchaseDAO = new PurchaseDAO();

        public void AddProduct(Product product)
        {
            try
            { 
                if (product.AvaiableQuantity < 0)
                {
                    throw new ApplicationException("é proibido adicionar uma quantidade de produto menor que zero");
                }

                if (supplierDAO.Find(new Supplier() { CNPJ = product.SupplierId }) == null)
                {
                    throw new ApplicationException("o fornecedor do produto nao existe");
                }

                productDAO.Insert(product);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteProduct(Product product)
        {
            try
            {
                if (purchaseDAO.FindAll().Where(prod => prod.ProductID == product.ProductID).Any())
                {
                    throw new ApplicationException("você nao pode deletar um produto registrado em uma compra");
                }

                productDAO.Delete(product);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                if (product.AvaiableQuantity < 0)
                {
                    throw new ApplicationException("é proibido adicionar uma quantidade de produtos menor que zero");
                }

                if (productDAO.Find(product) == null)
                {
                    throw new ApplicationException("o ID do produto nao existe");
                }

                if (supplierDAO.Find(new Supplier() { CNPJ = product.SupplierId }) == null)
                {
                    throw new ApplicationException("O fornecedor do produto não existe");
                }

                this.productDAO.Update(product);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        
        public Product FindProduct(Product product)
        {
            try
            {
                return this.productDAO.Find(product);
            }
            catch (Exception e)
            {
                
                throw e;
            }

        }

        public IList<Product> FindAllProducts() {

            try
            {
                return this.productDAO.FindAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IList<Product> FindProductsByFilter(Product product) {
            try
            {
                return productDAO.FindProductsByFilter(product);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}