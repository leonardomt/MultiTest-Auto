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
    
    public partial class PruFHumano
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PruFHumano()
        {
            this.SujetosEvaluados = new HashSet<SujetosEvaluados>();
        }
    
        public long idTest { get; set; }
        public string DuraPru { get; set; }
        public string PuntajeFDR { get; set; }
        public string PuntajeFIR { get; set; }
        public string PuntajeFSR { get; set; }
        public string PuntajeFCR { get; set; }
        public string A { get; set; }
        public string PuntDifD { get; set; }
        public string PuntDifI { get; set; }
        public string PuntDifS { get; set; }
        public string PuntDifC { get; set; }
        public string PuntDifDPC { get; set; }
        public string PuntDifIPC { get; set; }
        public string PuntDifSPC { get; set; }
        public string PuntDifCPC { get; set; }
        public string PFinalD { get; set; }
        public string PFinalI { get; set; }
        public string PFinalS { get; set; }
        public string PFinalC { get; set; }
        public string PuntajeTR { get; set; }
        public string Fecha { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SujetosEvaluados> SujetosEvaluados { get; set; }
    }
}
