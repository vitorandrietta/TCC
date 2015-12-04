﻿namespace ClientService
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
    /// A interface representing the methods do be exposed in the ClientService webservice
    /// </summary>
    [ServiceContract]
    public interface IClientService
    {
        /// <summary>
        /// Adds a client to the database
        /// </summary>
        /// <param name="client">a model that represents a client be inserted</param>
        [OperationContract]
        void AddClient(Client client);

        /// <summary>
        /// Removes a client from the database
        /// </summary>
        /// <param name="client">A model that represents a client to me removed</param>
        [OperationContract]
        void RemoveClient(Client client);

        /// <summary>
        /// Update client information in the database
        /// </summary>
        /// <param name="client">a model that represents a client to be updated</param>
        [OperationContract]
        void UpdateClient(Client client);

        /// <summary>
        /// Returns All The Clients
        /// </summary>
        /// <returns>A list containing all the clients</returns>
        [OperationContract]
        IList<Client> FindAll();

        /// <summary>
        /// return a specific client from the database
        /// </summary>
        /// <param name="objectToBeFound">a client object containing the cpf to be used as a primaryKey</param>
        /// <returns>the wanted client</returns>
        [OperationContract]
        Client Find(Client objectToBeFound);

        /// <summary>
        /// return a description of a specific client type
        /// </summary>
        /// <param name="clientType">a Client Type containing the clientType id</param>
        /// <returns> a description of a client type</returns>
        [OperationContract]
        string FindTypeDescription(ClientType clientType);

        /// <summary>
        /// Returns all Client Types
        /// </summary>
        /// <returns>a List of Client Types</returns>
        [OperationContract]
        IList<ClientType> FindAllClientTypes();

        [OperationContract]
        IList<Client> FindClientsByFilter(Client clientFilter);
    }
}
