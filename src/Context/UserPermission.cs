//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace userDemo1.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserPermission
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public bool ViewPermission { get; set; }
        public bool AddPermission { get; set; }
        public bool EditPermission { get; set; }
        public bool DeletePermission { get; set; }
        public int UserId { get; set; }
    
        public virtual User User { get; set; }
    }
}
