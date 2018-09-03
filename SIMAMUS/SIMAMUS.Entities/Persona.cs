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
    
    public partial class Persona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Persona()
        {
            this.Paciente = new HashSet<Paciente>();
            this.Usuario = new HashSet<Usuario>();
        }
    
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string ApellidoUno { get; set; }
        public string ApellidoDos { get; set; }
        public int Cedula { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public int IdSexo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paciente> Paciente { get; set; }
        public virtual Sexo Sexo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}