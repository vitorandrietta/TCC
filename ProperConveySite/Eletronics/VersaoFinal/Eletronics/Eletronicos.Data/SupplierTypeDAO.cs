﻿namespace Eletronicos.Data
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Eletronicos.Model;
    
    /// <summary>
    /// A class that performs CRUD operations of SupplierType Table
    /// </summary>
    public class SupplierTypeDAO
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
        /// Returns the wanted SupplierType Description
        /// </summary>
        /// <param name="supplierTypeID">the primary key to be used in the search</param>
        /// <returns>a Register that corresponds to the primary key passed in the parameter</returns>
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
        public IList<SupplierType> FindAll()
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
