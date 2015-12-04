namespace Eletronicos.Data
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
    public class ClientDAO
    {

        private ClientService.ClientServiceClient clientData = new ClientService.ClientServiceClient();

        public void Insert(Client client)
        {
            this.clientData.AddClient(client);
        }

        public void Delete(Client client)
        {
            this.clientData.RemoveClient(client);
        }

        public void Update(Client client)
        {
            this.clientData.UpdateClient(client);
        }

        public IList<Client> FindAll()
        {
            return this.clientData.FindAll();
        }

        public Client Find(Client client)
        {
            return this.clientData.Find(client);
        }

        public IList<Client> FindClientsByFilter(Client filterClient)
        {
            return this.clientData.FindClientsByFilter(filterClient);
        }

        public string FindClientTypeDescription(int clientTypeId)
        {
            return this.clientData.FindTypeDescription(new ClientType() { ClientTypeID = clientTypeId });
        }

        public IList<ClientType> FindAllClientTypes()
        {
            return this.clientData.FindAllClientTypes();
        }
    }
}
