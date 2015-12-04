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
    using System.Data;

    /// <summary>
    /// A class that performs CRUD operations of Client Table
    /// </summary>
    public class ClientDAO : IData<Client>
    {
        /// <summary>
        /// A variable that represents the connection with the database
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
        /// insert a new row in the Client table of the database
        /// </summary>
        /// <param name="objectToBeInserted">represents the model that gonna be inserted in the database table</param>
        public void Insert(Client objectToBeInserted)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("exec PR_CLIENTE_INSERT @FIRSTNAME,@LASTNAME,@CPF,@RG,@CLIENTTYPEID");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@FIRSTNAME", objectToBeInserted.FirstName);
                this.command.Parameters.AddWithValue("@LASTNAME", objectToBeInserted.LastName);
                this.command.Parameters.AddWithValue("@CPF", objectToBeInserted.CPF);
                this.command.Parameters.AddWithValue("@RG", objectToBeInserted.RG);
                this.command.Parameters.AddWithValue("@CLIENTTYPEID", objectToBeInserted.ClientTypeID);
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
        public void Delete(Client objectToBeDeleted)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("EXEC PR_CLIENT_DELETE @CPF");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CPF", objectToBeDeleted.CPF);
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
        public void Update(Client objectToBeUpdated)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("EXEC PR_CLIENT_UPDATE @CPF,@FIRSTNAME,@LASTNAME,@RG,@CLIENTTYPEID");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@FIRSTNAME", objectToBeUpdated.FirstName);
                this.command.Parameters.AddWithValue("@LASTNAME", objectToBeUpdated.LastName);
                this.command.Parameters.AddWithValue("@CPF", objectToBeUpdated.CPF);
                this.command.Parameters.AddWithValue("@RG", objectToBeUpdated.RG);
                this.command.Parameters.AddWithValue("@CLIENTTYPEID", objectToBeUpdated.ClientTypeID);
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
        public IList<Client> FindAll()
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT*FROM DBO.FN_Client_Select()");
                this.command.Connection = this.connection;
                this.query = this.command.ExecuteReader();
                IList<Client> clients = new List<Client>();
                while (this.query.Read())
                {
                    clients.Add(new Client()
                    {
                        FirstName = this.query.GetString(0),
                        LastName = this.query.GetString(1),
                        CPF = this.query.GetString(2),
                        RG = this.query.GetString(3),
                        ClientTypeID = this.query.GetInt32(4),
                    });
                }

                return clients;
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



        public IList<Client> FindClientByFilter(Client clientFilter)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("EXEC PR_ClientSelectByFilter @NM_FirstName,@NM_LastName,@CD_CPF,@CD_RG,@NR_ClientTypeID");
                //this.command.CommandType = CommandType.StoredProcedure;
                if (clientFilter.FirstName == null)
                {
                    clientFilter.FirstName = "";
                }
                if (clientFilter.LastName == null)
                {
                    clientFilter.LastName = "";
                }
                if (clientFilter.CPF == null)
                {
                    clientFilter.CPF = "";
                }
                clientFilter.CPF = clientFilter.CPF.Replace(".", "").Replace("-", "");
                if (clientFilter.RG == null)
                {
                    clientFilter.RG = "";
                }
                clientFilter.RG = clientFilter.RG.Replace(".", "").Replace("-", "");
                
                this.command.Parameters.AddWithValue("@NM_FirstName", clientFilter.FirstName);
                this.command.Parameters.AddWithValue("@NM_LastName", clientFilter.LastName);
                this.command.Parameters.AddWithValue("@CD_CPF", clientFilter.CPF);
                this.command.Parameters.AddWithValue("@CD_RG", clientFilter.RG);
                this.command.Parameters.AddWithValue("@NR_ClientTypeID", clientFilter.ClientTypeID);
                this.command.Connection = this.connection;
                this.query = this.command.ExecuteReader();
                IList<Client> clients = new List<Client>();
                while (this.query.Read())
                {
                    clients.Add(new Client()
                    {
                        FirstName = this.query.GetString(0),
                        LastName = this.query.GetString(1),
                        CPF = this.query.GetString(2),
                        RG = this.query.GetString(3),
                        ClientTypeID = this.query.GetInt32(4),
                    });
                }

                return clients;
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
        /// Returns the wanted Client
        /// </summary>
        /// <param name="objectToBeFound">A client containing the primary key to be used in the search</param>
        /// <returns>a Register that corresponds to the primary key passed in the parameter object</returns>
        public Client Find(Client objectToBeFound)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT*FROM DBO.FN_ClientSelectSingle(@CPF)");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CPF", objectToBeFound.CPF);
                this.query = this.command.ExecuteReader();
                Client client = null;
                if (this.query.Read())
                {
                    client = new Client()
                    {
                        FirstName = this.query.GetString(0),
                        LastName = this.query.GetString(1),
                        CPF = this.query.GetString(2),
                        RG = this.query.GetString(3),
                        ClientTypeID = this.query.GetInt32(4),
                    };
                }

                return client;
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
