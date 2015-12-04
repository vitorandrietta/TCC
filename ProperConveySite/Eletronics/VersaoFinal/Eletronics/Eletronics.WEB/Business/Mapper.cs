using Eletronicos.Data;
using Eletronicos.Model;
using Eletronicos.Model.Product;
using Eletronics.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eletronics.WEB.Business
{

    public class Mapper
    {
        private ClientDAO clientDAO = new ClientDAO();
        private SupplierTypeDAO supplierTypeDAO = new SupplierTypeDAO();
        private SupplierDAO supplierDAO = new SupplierDAO();
        public ClientScreenModel ClientToClientScreen(Client client)
        {
            return new ClientScreenModel()
            {
                ClientTypeDescription = clientDAO.FindClientTypeDescription(client.ClientTypeID),
                CPF = client.CPF,
                FirstName = client.FirstName,
                LastName = client.LastName,
                RG = client.RG
            };
        }

        public ClientTypeScreenModel ClientTypeToClientTypeScreen(ClientType clientType)
        {
            return new ClientTypeScreenModel()
            {
                ClientTypeDescription = clientType.ClientTypeDescription,
                ClientTypeID = clientType.ClientTypeID
            };

        }

        public SupplierTypeScreenModel SupplierTypeToSupplierTypeScreen(SupplierType supplierType)
        {
            return new SupplierTypeScreenModel()
            {
                SupplierTypeDescription = supplierType.SupplierTypeDescription,
                SupplierTypeID = supplierType.SupplierTypeID
            };
        }

        public SupplierScreenModel SupplierToSupplierScreenModel(Supplier supplier)
        {
            return new SupplierScreenModel()
            {
                CNPJ = supplier.CNPJ,
                CorporateName = supplier.CorporateName,
                SupplierTypeDescription = this.supplierTypeDAO.FindTypeDescription(supplier.SupplierTypeID)
            };
        }

        public ProductScreenModel ProductToProductScreenModel(Product product)
        {
            return new ProductScreenModel()
            {
                AvaiableQuantity = product.AvaiableQuantity,
                ProductDescription = product.ProductDescription,
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                SupplierId = product.SupplierId,
                SupplierName = supplierDAO.Find(new Supplier() { CNPJ = product.SupplierId }).CorporateName
            };

        }

        public ProductSupplierScreenModel SupplierToProductSupplier(Supplier supplier)
        {


            return new ProductSupplierScreenModel()
            {
                SupplierID = supplier.CNPJ,
                SupplierName = supplier.CorporateName
            };

        }
    }
}