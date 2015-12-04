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
    /// The Model That Represents a ClientType
    /// </summary>
    
    public class ClientType
    {
        /// <summary>
        /// Gets or sets The ClientTypeID
        /// </summary>
    
         public int ClientTypeID { get; set; }
        
        /// <summary>
        /// Gets or sets the ClientTypeDescription
        /// </summary>
    
        public string ClientTypeDescription { get; set; }
    }
}
