namespace ClientService.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using Eletronicos.Data;
    using Eletronicos.Model;
    using Eletronics.Infrastructure;
    using System.ServiceModel;
    using EletronicStore.WEB.Business;

    /// <summary>
    /// A class that makes the necessary validation for client information, and is wrapper  for ClientDAO class methods
    /// </summary>
    public class ClientBO
    {


        /// <summary>
        /// A member that represents  a data access object for client information
        /// </summary>
        private ClientDAO clientDAO = new ClientDAO();

        /// <summary>
        /// A member that represents a data access object for supplier type information
        /// </summary>

        private PurchaseBO purchaseBO = new PurchaseBO();

        /// <summary>
        /// validate the attributes of the client and if everything is ok, the client will be inserted
        /// </summary>
        /// <param name="client">the Client to be inserted in the client table of the database</param>
        public void Add(Client client)
        {
            try
            {
                if (!Validator.IsCPF(client.CPF))
                {
                    throw new ApplicationException("O CPF do cliente é inválido");
                }

                if (this.Find(client) != null)
                {
                    throw new ApplicationException("Um cliente com esse CPF já existe");
                }

                if (client.FirstName != null)
                {

                    if (!Validator.IsValidName(client.FirstName))
                    {
                        throw new ApplicationException("O Nome do cliente é inválido");
                    }

                }

                if (client.LastName != null)
                {

                    if (!Validator.IsValidName(client.LastName))
                    {
                        throw new ApplicationException("O sobrenome do cliente é inválido");
                    }
                }


                if (this.clientDAO.FindClientTypeDescription(client.ClientTypeID) == null)
                {
                    throw new ApplicationException("O id do tipo de cliente informado nao existe");
                }


             if (client.FirstName == null) 
                {
                    client.FirstName = client.LastName;
                    client.LastName = "";
                }

                client.RG = client.RG.Replace(".", string.Empty).Replace("-", string.Empty);
                client.CPF = client.CPF.Replace(".", string.Empty).Replace("-", string.Empty);
                this.clientDAO.Insert(client);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        /// <summary>
        /// deletes the row of the table that corresponds to the primary key of the parameter
        /// </summary>
        /// <param name="client">represents the model that gonna be deleted from the database table</param>
        public void Remove(Client client)
        {
            try
            {
                if (purchaseBO.ClientPurchases(client).Any())
                {
                    throw new ApplicationException("você não pode remover um cliente que possui uma compra registrada");
                }

                this.clientDAO.Delete(client);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returns the wanted Client
        /// </summary>
        /// <param name="client">A client containing the primary key to be used in the search</param>
        /// <returns>a Register that corresponds to the primary key passed in the parameter object</returns>
        public Client Find(Client client)
        {
            try
            {
                client.CPF = client.CPF.Replace(".", string.Empty).Replace("-", string.Empty);
                return this.clientDAO.Find(client);
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
        public IList<Client> FindAll()
        {
            try
            {
                return this.clientDAO.FindAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// validate the attributes of the client and if everything is ok, Updates a specific row of a table using the reference of the primary key
        /// </summary>
        /// <param name="client">represents the model that gonna be Updated from the database table</param>
        public void Update(Client client)
        {
            try
            {

                if (this.clientDAO.Find(client) == null)
                {
                    throw new ApplicationException("O cliente nao existe nos registros");
                }

                if (client.FirstName != null)
                {

                    if (!Validator.IsValidName(client.FirstName))
                    {
                        throw new ApplicationException("O nome do cliente é inválido");
                    }

                }

                if (client.LastName != null)
                {

                    if (!Validator.IsValidName(client.LastName))
                    {
                        throw new ApplicationException("O sobrenome do cliente é inválido");
                    }
                }

                if (this.clientDAO.FindClientTypeDescription(client.ClientTypeID) == null)
                {
                    throw new ApplicationException("O id do tipo de cliente informado nao existe");
                }

                if (client.FirstName == null)
                {
                    client.FirstName = client.LastName;
                    client.LastName = "";
                }


                client.RG = client.RG.Replace(".", string.Empty).Replace("-", string.Empty);
                client.CPF = client.CPF.Replace(".", string.Empty).Replace("-", string.Empty);
                this.clientDAO.Update(client);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IList<ClientType> FindAllClientTypes()
        {

            return this.clientDAO.FindAllClientTypes();
        }

        public string FindClientTypeDescription(int clientId)
        {
            return this.clientDAO.FindClientTypeDescription(clientId);
        }

        public IList<Client> FindClientsByFilter(Client filterClient)
        {
            try
            {
                return clientDAO.FindClientsByFilter(filterClient);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}