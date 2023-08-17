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
    
    public partial class Residente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Residente()
        {
            this.Album = new HashSet<Album>();
            this.Incidente = new HashSet<Incidente>();
            this.ResidenteDonante = new HashSet<ResidenteDonante>();
            this.ResidentePatologia = new HashSet<ResidentePatologia>();
        }
    
        public int id { get; set; }
        public byte[] fotoId { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Nullable<bool> sexo { get; set; }
        public bool esterilizado { get; set; }
        public System.DateTime fechaIngreso { get; set; }
        public Nullable<System.DateTime> fechaNacimiento { get; set; }
        public Nullable<System.DateTime> fechaDesaparicion { get; set; }
        public Nullable<System.DateTime> fechaDefuncion { get; set; }
        public Nullable<int> zonaId { get; set; }
        public int estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album> Album { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Incidente> Incidente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResidenteDonante> ResidenteDonante { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResidentePatologia> ResidentePatologia { get; set; }
        public virtual Zona Zona { get; set; }
    }
}