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
        private PurchaseDAO purchaseDAO = new PurchaseDAO();
        private SupplierDAO supplierDAO = new SupplierDAO();

        public void AddProduct(Product product)
        {//1

            try
            {
                if (product.AvaiableQuantity < 0)
                {
                    throw new ApplicationException("é proibido adicionar uma quantidade menor do que zero");
                }

                if (supplierDAO.Find(new Supplier() { CNPJ = product.SupplierId }) == null)
                {
                    throw new ApplicationException("O fornecedor desse produto nao existe");
                }

                productDAO.Insert(product);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteProduct(Product product) //2
        {
            try
            {
                if (purchaseDAO.Find(new Purchase() { ProductID = product.ProductID }) != null)
                {
                    throw new ApplicationException("você nao pode deletar um produto que já foi registrado em uma venda");
                }

                productDAO.Delete(product);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void UpdateProduct(Product product)
        {//3
            try
            {
                if (product.AvaiableQuantity < 0)
                {
                    throw new ApplicationException("é proibido adicionar uma quantidade de produto menor que zero");
                }

                if (productDAO.Find(product) == null)
                {
                    throw new ApplicationException("o ID do produto nao existe");
                }

                if (supplierDAO.Find(new Supplier() { CNPJ = product.SupplierId }) == null)
                {
                    throw new ApplicationException("O ID do fornecedor nao existe");
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