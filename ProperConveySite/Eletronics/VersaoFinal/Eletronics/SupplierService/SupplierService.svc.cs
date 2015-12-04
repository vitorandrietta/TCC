using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eletronicos.Model;
using System.Data.SqlClient;
using System.Configuration;

namespace SupplierService
{
    /// <summary>
    /// a class that represents a web service and is a wrapper for SupplierBO class methods
    /// </summary>
    public class SupplierService : ISupplierService
    {
        /// <summary>
        ///  A variable that represents the connection with the database
        /// </summary>
        private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBELETRONICS"].ConnectionString);

        /// <summary>
        /// A variable that represents the result of a query in the database
        /// </summary>
        private SqlDataReader query;

        /// <summary>
        /// A variable that represents a command to be executed in the database
        /// </summary>
        private SqlCommand command;

        /// <summary>
        /// insert a new row in the Supplier table of the database
        /// </summary>
        /// <param name="objectToBeInserted">represents the model that gonna be inserted in the database table</param>
        public void Insert(Supplier objectToBeInserted)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("PR_SUPPLIER_INSERT @CorporationName,@CNPJ,@SupplierTypeID");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CorporationName", objectToBeInserted.CorporateName);
                this.command.Parameters.AddWithValue("@CNPJ", objectToBeInserted.CNPJ);
                this.command.Parameters.AddWithValue("@SupplierTypeID", objectToBeInserted.SupplierTypeID);
                this.command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.connection.Close();
                this.command = null;
            }
        }

        /// <summary>
        /// deletes the row of the table that corresponds to the primary key of the parameter
        /// </summary>
        /// <param name="objectToBeDeleted">represents the model that gonna be deleted from the database table</param>
        public void Delete(Supplier objectToBeDeleted)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("PR_SUPPLIER_DELETE @CNPJ");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CNPJ", objectToBeDeleted.CNPJ);
                this.command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.connection.Close();
                this.command = null;
            }
        }

        /// <summary>
        /// Update a specific row of a table using the reference of the primary key
        /// </summary>
        /// <param name="objectToBeUpdated">represents the model that gonna be Updated from the database table</param>
        public void Update(Supplier objectToBeUpdated)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("PR_SUPPLIER_UPDATE @CNPJ,@CorporationName,@SupplierTypeID");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CorporationName", objectToBeUpdated.CorporateName);
                this.command.Parameters.AddWithValue("@CNPJ", objectToBeUpdated.CNPJ);
                this.command.Parameters.AddWithValue("@SupplierTypeID", objectToBeUpdated.SupplierTypeID);
                this.command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.connection.Close();
                this.command = null;
            }
        }

        /// <summary>
        /// Return all Registers of the Client table in the database
        /// </summary>
        /// <returns> A List object where all data is stored</returns>
        public IList<Supplier> FindAllSuppliers()
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT*FROM DBO.FN_SupplierSelectAll()");
                this.command.Connection = this.connection;
                this.query = this.command.ExecuteReader();
                IList<Supplier> suppliers = new List<Supplier>();
                while (this.query.Read())
                {
                    suppliers.Add(new Supplier()
                    {
                        CorporateName = this.query.GetString(0),
                        CNPJ = this.query.GetString(1),
                        SupplierTypeID = this.query.GetInt32(2)
                    });
                }

                return suppliers;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.connection.Close();
                this.command = null;
                this.query.Close();
                this.query = null;
            }
        }

        public IList<Supplier> FindSuppliersByFilter(Supplier supplierFilter)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("EXEC PR_SupplierSelectByFilter @NM_CorporateName,@CD_CNPJ,@NR_SupplierTypeID");
                if (supplierFilter.CorporateName == null)
                {
                    supplierFilter.CorporateName = "";
                }
                if (supplierFilter.CNPJ == null)
                {
                    supplierFilter.CNPJ = "";
                }
                supplierFilter.CNPJ = supplierFilter.CNPJ.Replace(".", "").Replace("-", "");

                this.command.Parameters.AddWithValue("@NM_CorporateName", supplierFilter.CorporateName);
                this.command.Parameters.AddWithValue("@CD_CNPJ", supplierFilter.CNPJ);
                this.command.Parameters.AddWithValue("@NR_SupplierTypeID", supplierFilter.SupplierTypeID);
                this.command.Connection = this.connection;
                this.query = this.command.ExecuteReader();
                IList<Supplier> suppliers = new List<Supplier>();
                while (this.query.Read())
                {
                    suppliers.Add(new Supplier()
                    {
                        CorporateName = this.query.GetString(0),
                        CNPJ = this.query.GetString(1),
                        SupplierTypeID = this.query.GetInt32(2)
                    });
                }

                return suppliers;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.connection.Close();
                this.command = null;
                this.query.Close();
                this.query = null;
            }
        }

        /// <summary>
        /// Returns the wanted Supplier
        /// </summary>
        /// <param name="objectToBeFound">A Supplier containing the primary key to be used in the search</param>
        /// <returns>a Register that corresponds to the primary key passed in the parameter object</returns>
        public Supplier Find(Supplier objectToBeFound)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT*FROM DBO.FN_SupplierSingleSelect(@CNPJ)");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CNPJ", objectToBeFound.CNPJ);
                this.query = this.command.ExecuteReader();
                Supplier supplier = null;
                if (this.query.Read())
                {
                    supplier = new Supplier()
                    {
                        CorporateName = this.query.GetString(0),
                        CNPJ = this.query.GetString(1),
                        SupplierTypeID = this.query.GetInt32(2)
                    };
                }

                return supplier;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.connection.Close();
                this.command = null;
                this.query.Close();
                this.query = null;
            }
        }

        public string FindTypeDescription(int supplierTypeID)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT DBO.FN_GetSupplierTypeDescription(@SUPPLIERTYPEID)");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@SUPPLIERTYPEID", supplierTypeID);
                this.query = this.command.ExecuteReader();
                string description = null;
                if (this.query.Read())
                {
                    description = this.query.GetString(0);
                }

                return description;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.connection.Close();
                this.command = null;
                this.query.Close();
                this.query = null;
            }
        }

        /// <summary>
        /// Return all Registers of the SupplierType table in the database
        /// </summary>
        /// <returns> A List object where all data is stored</returns>
        public IList<SupplierType> FindAllSupplierTypes()
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT*FROM DBO.FN_GetAllSupplierType()");
                this.command.Connection = this.connection;
                this.query = this.command.ExecuteReader();
                IList<SupplierType> supplierTypes = new List<SupplierType>();
                while (this.query.Read())
                {
                    supplierTypes.Add(new SupplierType()
                    {
                        SupplierTypeID = this.query.GetInt32(0),
                        SupplierTypeDescription = this.query.GetString(1)
                    });
                }

                return supplierTypes;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.connection.Close();
                this.command = null;
                this.query.Close();
                this.query = null;
            }
        }
    }
}
