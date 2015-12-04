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
    /// The Model That Represents a Type of Supplier
    /// </summary>
    [DataContract]
    public class SupplierType
    {
        /// <summary>
        /// Gets or sets The SupplierTypeID
        /// </summary>
        [DataMember]
        public int SupplierTypeID { get; set; }

        /// <summary>
        /// Gets or sets the SupplierTypeDescription
        /// </summary>
        [DataMember]
        public string SupplierTypeDescription { get; set; }
    }
}
