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
    
    public partial class Citizen_group
    {
        public long citizen_CPR { get; set; }
        public long shopping_group_ID { get; set; }
    
        public virtual Citizen Citizen { get; set; }
    }
}
