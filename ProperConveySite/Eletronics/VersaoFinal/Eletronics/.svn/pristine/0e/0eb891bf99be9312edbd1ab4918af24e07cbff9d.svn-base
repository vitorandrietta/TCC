namespace SupplierService.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Eletronicos.Data;
    using Eletronicos.Model;

    /// <summary>
    /// A class that is wrapper for SupplierTypeDAO class methods
    /// </summary>
    public class SupplierTypeBO
    {
        /// <summary>
        /// A member that represents  a data access object for Supplier Type information
        /// </summary>
        private SupplierTypeDAO supplierTypeDAO = new SupplierTypeDAO();

        /// <summary>
        /// Returns the wanted SupplierType Description
        /// </summary>
        /// <param name="supplierTypeID">the primary key to be used in the search</param>
        /// <returns>a Register that corresponds to the primary key passed in the parameter</returns>
        public string FindDescription(int supplierTypeID)
        {
            try
            {
                return this.supplierTypeDAO.FindTypeDescription(supplierTypeID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Return all Registers of the SupplierType table in the database
        /// </summary>
        /// <returns> A List object where all data is stored</returns>
        public IList<SupplierType> FindAll()
        {
            try
            {
                return this.supplierTypeDAO.FindAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}