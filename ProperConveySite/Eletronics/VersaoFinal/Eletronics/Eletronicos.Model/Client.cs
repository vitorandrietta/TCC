﻿namespace Eletronicos.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;
       
   /// <summary>
   ///  The Model that Represents a Client
   /// </summary>
    [DataContract]
    public class Client
    {
        /// <summary>
        /// Gets or sets The First Name of The Client
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets The Last Name of The Client
        /// </summary>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets The 11 digits Client CPF
        /// </summary>
        [DataMember]
        public string CPF { get; set; }

        /// <summary>
        /// Gets or sets The Client RG
        /// </summary>
        [DataMember]
        public string RG { get; set; }

        /// <summary>
        /// Gets or sets The ID of the type of the client
        /// </summary>
        [DataMember]
        public int ClientTypeID { get; set; }
    }
}
