using System.Collections.Generic;
using Eletronicos.Model;
using System;
using System.Data.SqlClient;
using System.Configuration;

namespace ClientService
{
    /// <summary>
    /// a class that represents a web service and is a wrapper for ClientBO class methods
    /// </summary>
    public class ClientService : IClientService
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
        public void AddClient(Client client)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("exec PR_CLIENTE_INSERT @FIRSTNAME,@LASTNAME,@CPF,@RG,@CLIENTTYPEID");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@FIRSTNAME", client.FirstName);
                this.command.Parameters.AddWithValue("@LASTNAME", client.LastName);
                this.command.Parameters.AddWithValue("@CPF", client.CPF);
                this.command.Parameters.AddWithValue("@RG", client.RG);
                this.command.Parameters.AddWithValue("@CLIENTTYPEID", client.ClientTypeID);
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
        public void RemoveClient(Client client)
        {
            try
            {   
                this.connection.Open();
                this.command = new SqlCommand("EXEC PR_CLIENT_DELETE @CPF");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CPF", client.CPF);
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
        public void UpdateClient(Client client)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("EXEC PR_CLIENT_UPDATE @CPF,@FIRSTNAME,@LASTNAME,@RG,@CLIENTTYPEID");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@FIRSTNAME", client.FirstName);
                this.command.Parameters.AddWithValue("@LASTNAME", client.LastName);
                this.command.Parameters.AddWithValue("@CPF", client.CPF);
                this.command.Parameters.AddWithValue("@RG", client.RG);
                this.command.Parameters.AddWithValue("@CLIENTTYPEID", client.ClientTypeID);
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



        public IList<Client> FindClientsByFilter(Client clientFilter)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("EXEC PR_ClientSelectByFilter @NM_FirstName,@NM_LastName,@CD_CPF,@CD_RG,@NR_ClientTypeID");

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
        public string FindTypeDescription(ClientType clientType)
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT DBO.FN_GetClientTypeDescription(@CLIENTETYPEID)");
                this.command.Connection = this.connection;
                this.command.Parameters.AddWithValue("@CLIENTETYPEID", clientType.ClientTypeID);
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
        /// Return all Registers of the ClientType table in the database
        /// </summary>
        /// <returns> A List object where all data is stored</returns>
        public IList<ClientType> FindAllClientTypes()
        {
            try
            {
                this.connection.Open();
                this.command = new SqlCommand("SELECT*FROM DBO.FN_SelectAllClientType()");
                this.command.Connection = this.connection;
                this.query = this.command.ExecuteReader();
                IList<ClientType> clientTypes = new List<ClientType>();
                while (this.query.Read())
                {
                    clientTypes.Add(new ClientType()
                    {
                        ClientTypeID = this.query.GetInt32(0),
                        ClientTypeDescription = this.query.GetString(1)
                    });
                }

                return clientTypes;
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


