//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIMAMUS.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Paciente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paciente()
        {
            this.RegistroResultados = new HashSet<RegistroResultados>();
        }
    
        public int IdPaciente { get; set; }
        public int IdCentro { get; set; }
        public int IdSector { get; set; }
        public int IdPersona { get; set; }
    
        public virtual CentroSalud CentroSalud { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual Sector Sector { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistroResultados> RegistroResultados { get; set; }
    }
}