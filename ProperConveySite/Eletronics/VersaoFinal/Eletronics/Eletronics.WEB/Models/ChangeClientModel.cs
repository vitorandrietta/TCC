using Eletronicos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eletronics.WEB.Models
{
    public class ChangeClientModel
    {
        public IList<ClientTypeScreenModel> ClientTypes { get; set; }
        public IList<ClientScreenModel> Clients { get; set; }
    }
}