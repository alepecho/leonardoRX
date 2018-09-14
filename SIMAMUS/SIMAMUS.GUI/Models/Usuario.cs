//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIMAMUS.GUI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Medico = new HashSet<Medico>();
            this.Radiologo = new HashSet<Radiologo>();
            this.RegistroResultados = new HashSet<RegistroResultados>();
        }
    
        public string NombreUsuario { get; set; }
        public string Contrasenna { get; set; }
        public int IdNivel { get; set; }
        public int Cedula { get; set; }
        public bool Activo { get; set; }
    
        public virtual NivelUsuario NivelUsuario { get; set; }
        public virtual Persona Persona { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medico> Medico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Radiologo> Radiologo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistroResultados> RegistroResultados { get; set; }
    }
}
