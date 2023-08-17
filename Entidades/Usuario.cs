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
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.AES = new HashSet<AES>();
            this.Incidente = new HashSet<Incidente>();
        }
    
        public int id { get; set; }
        public byte[] fotoId { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string username { get; set; }
        public string pw { get; set; }
        public string email { get; set; }
        public string telefonoCel { get; set; }
        public System.DateTime fechaCreacion { get; set; }
        public Nullable<int> rolId { get; set; }
        public int estado { get; set; }
        public string TokenRestablecimiento { get; set; }
        public Nullable<System.DateTime> FechaCreacionToken { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AES> AES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Incidente> Incidente { get; set; }
        public virtual Rol Rol { get; set; }
    }
}