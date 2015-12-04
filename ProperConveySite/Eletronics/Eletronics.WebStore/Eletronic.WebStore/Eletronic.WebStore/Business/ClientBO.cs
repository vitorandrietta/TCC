using Eletronicos.Data;
using Eletronicos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EletronicStore.WEB.Business
{
    public class ClientBO
    {
        private ClientLoginDAO clientLoginDAO = new ClientLoginDAO();

        public bool login(ClientLogin client)
        {
            try
            {
                return this.clientLoginDAO.Login(client);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ClientLogin FindClient(ClientLogin client) {

            try
            {
                return clientLoginDAO.Find(client);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}