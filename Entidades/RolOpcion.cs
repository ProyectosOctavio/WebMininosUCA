//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class RolOpcion
    {
        public int id { get; set; }
        public Nullable<int> opcionId { get; set; }
        public Nullable<int> rolId { get; set; }
        public int estado { get; set; }
    
        public virtual Opcion Opcion { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
