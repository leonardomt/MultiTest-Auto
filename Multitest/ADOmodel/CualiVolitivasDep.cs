//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Multitest.ADOmodel
{
    using System;
    using System.Collections.Generic;
    
    public partial class CualiVolitivasDep
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CualiVolitivasDep()
        {
            this.SujetosEvaluados = new HashSet<SujetosEvaluados>();
        }
    
        public long idTest { get; set; }
        public string PtoAutoIndepen { get; set; }
        public string PtoTenacidadResol { get; set; }
        public string PtoPersePersis { get; set; }
        public string PtoAutodAutocon { get; set; }
        public string DuraPru { get; set; }
        public string Fecha { get; set; }
        public string Falseamiento { get; set; }
        public string autoIndepen { get; set; }
        public string tenacidadResol { get; set; }
        public string persePersis { get; set; }
        public string autodAutocon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SujetosEvaluados> SujetosEvaluados { get; set; }
    }
}