namespace SupplierService.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Eletronicos.Data;
    using Eletronicos.Model;
    using Eletronics.Infrastructure;
    using Eletronicos.Model.Product;

    /// <summary>
    /// A class that make the necessary validation for Supplier information, and is wrapper for SupplierDAO class methods
    /// </summary>
    public class SupplierBO
    {
        /// <summary>
        /// A member that represents  a data access object for Supplier information
        /// </summary>
        private SupplierDAO supplierDAO = new SupplierDAO();
        private ProductDAO productDAO = new ProductDAO();
        /// <summary>
        ///  A member that represents  a data access object for Supplier Type information
        /// </summary>

        private SupplierTypeDAO supplierTypeDAO = new SupplierTypeDAO();

        /// <summary>
        /// validate the attributes of the supplier and if everything is ok, the supplier will be inserted
        /// </summary>
        /// <param name="supplier">the supplier to be inserted inserted the supplier table of the database</param>
        public void Add(Supplier supplier)
        {
            try
            {
                if (this.supplierTypeDAO.FindTypeDescription(supplier.SupplierTypeID) == null)
                {
                    throw new ApplicationException("O id to tipo de fornecedor não existe");
                }

                if (!Validator.IsCNPJ(supplier.CNPJ))
                {
                    throw new ApplicationException("O CNPJ é inválido");
                }

                if (this.supplierDAO.Find(supplier) != null)
                {
                    throw new ApplicationException("Um fornecedor com esse CNPJ já existe");
                }

                supplier.CNPJ = supplier.CNPJ.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
                this.supplierDAO.AddSupplier(supplier);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// deletes the row of the table that corresponds to the primary key of the parameter
        /// </summary>
        /// <param name="supplier">represents the model that gonna be deleted from the database table</param>
        public void Remove(Supplier supplier)
        {
            try
            {
                if (productDAO.FindProductsByFilter(new Product() { SupplierId = supplier.CNPJ }).Any())
                {
                    throw new ApplicationException("Você nao pode remover um fornecedor de um produto já cadastrado");
                }
                this.supplierDAO.RemoveSupplier(supplier);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// validate the attributes of the supplier and if everything is ok, Update a specific row of a table using the reference of the primary key
        /// </summary>
        /// <param name="supplier">represents the model that gonna be Updated from the database table</param>
        public void Update(Supplier supplier)
        {
            try
            {
                if (this.supplierDAO.Find(supplier) == null)
                {
                    throw new ApplicationException("O fornecedor informado nao existe");
                }

                if (this.supplierTypeDAO.FindTypeDescription(supplier.SupplierTypeID) == null)
                {
                    throw new ApplicationException("O ID do tipo do fornecedor nao existe");
                }

                supplier.CNPJ = supplier.CNPJ.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
                this.supplierDAO.UpdateSupplier(supplier);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returns the wanted Supplier
        /// </summary>
        /// <param name="supplier">A Supplier containing the primary key to be used in the search</param>
        /// <returns>a Register that corresponds to the primary key passed in the parameter object</returns>
        public Supplier Find(Supplier supplier)
        {
            try
            {
                return this.supplierDAO.Find(supplier);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Return all Registers of the Client table in the database
        /// </summary>
        /// <returns> A List object where all data is stored</returns>
        public IList<Supplier> FindAll()
        {
            try
            {
                return this.supplierDAO.FindAllSuppliers();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public IList<SupplierType> FindAllSupplierTypes()
        {

            return this.supplierDAO.FindAllSupplierTypes();
        }

        //public IList<Supplier> FindSuppliersByFilter(Supplier supplierFilter)
        //{
        //    try
        //    {
        //        return this.supplierDAO.FindSuppliersByFilter(supplierFilter);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
    }
}