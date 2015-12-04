namespace Eletronicos.Model
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
    /// The Model That Represents a Supplier Company
    /// </summary>
    [DataContract]
    public class Supplier
    {
        /// <summary>
        /// Gets or sets The Name of The Supplier Corporation
        /// </summary>
        [DataMember]
        public string CorporateName { get; set; }

        /// <summary>
        /// Gets or sets The CNPJ Of The Supplier Corporation
        /// </summary>
        [DataMember]
        public string CNPJ { get; set; }

        /// <summary>
        /// Gets or sets The  SupplierTypeID of the Supplier Corporation
        /// </summary>
        [DataMember]
        public int SupplierTypeID { get; set; }
    }
}
