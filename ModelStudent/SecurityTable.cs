//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentProject.ModelStudent
{
    using System;
    using System.Collections.Generic;
    
    public partial class SecurityTable
    {
        public int Id { get; set; }
        public System.Guid UserId { get; set; }
        public int SecurityQuestion { get; set; }
    
        public virtual UserRegistration UserRegistration { get; set; }
    }
}
