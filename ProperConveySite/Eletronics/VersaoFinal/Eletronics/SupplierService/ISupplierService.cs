namespace SupplierService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using System.Text;
    using Eletronicos.Model;

    /// <summary>
    /// A interface representing the methods do be exposed in the SupplierService webservice
    /// </summary>
    [ServiceContract]
    public interface ISupplierService
    {
        /// <summary>
        /// insert a new row in the Supplier table of the database
        /// </summary>
        /// <param name="supplier">represents the model that gonna be inserted in the database table</param>
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "AddSupplier")]
        [OperationContract]
         void Insert(Supplier objectToBeInserted);

        /// <summary>
        /// deletes the row of the table that corresponds to the primary key of the parameter
        /// </summary>
        /// <param name="supplier">represents the model that gonna be deleted from the database table</param>
        [WebInvoke(Method = "DELETE",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "RemoveSupplier")]
        [OperationContract]
        void Delete(Supplier objectToBeDeleted);

        /// <summary>
        /// validate the attributes of the supplier and if everything is ok, Update a specific row of a table using the reference of the primary key
        /// </summary>
        /// <param name="supplier">represents the model that gonna be Updated from the database table</param>
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "UpdateSupplier")]
        [OperationContract]
        void Update(Supplier objectToBeUpdated);

        /// <summary>
        /// Return all Registers of the Client table in the database
        /// </summary>
        /// <returns> A List object where all data is stored</returns>
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "FindAllSuppliers")]
        [OperationContract]
        IList<Supplier> FindAllSuppliers();

        /// <summary>
        /// Returns the wanted Supplier
        /// </summary>
        /// <param name="supplier">A Supplier containing the primary key to be used in the search</param>
        /// <returns>a Register that corresponds to the primary key passed in the parameter object</returns>
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "FindSupplier")]
        [OperationContract]
        Supplier Find(Supplier objectToBeFound);

        /// <summary>
        /// Return all Registers of the SupplierType table in the database
        /// </summary>
        /// <returns> A List object where all data is stored</returns>s
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "FindAllSupplierTypes")]
        [OperationContract]
        IList<SupplierType> FindAllSupplierTypes();

        /// <summary>
        /// Returns the wanted SupplierType Description
        /// </summary>
        /// <param name="supplierTypeID">the primary key to be used in the search</param>
        /// <returns>a Register that corresponds to the primary key passed in the parameter</returns>
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "FindSupplierType")]
        [OperationContract]
        string FindTypeDescription(int supplierTypeID);

        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "FindSuppliersByFilter")]
        [OperationContract]
        IList<Supplier> FindSuppliersByFilter(Supplier supplierFilter);
        
    }
}
