//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KEA.BA.Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shopping_group
    {
        public long store_ID { get; set; }
        public long group_ID { get; set; }
        public Nullable<long> group_size { get; set; }
    
        public virtual Store Store { get; set; }
    }
}
