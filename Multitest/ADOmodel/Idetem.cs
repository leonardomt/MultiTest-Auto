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
    
    public partial class Idetem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Idetem()
        {
            this.SujetosEvaluados = new HashSet<SujetosEvaluados>();
        }
    
        public long idTest { get; set; }
        public string durPrub { get; set; }
        public string sanguineo { get; set; }
        public string colerico { get; set; }
        public string flematico { get; set; }
        public string melancolico { get; set; }
        public string Fecha { get; set; }
        public string equilibrio { get; set; }
        public string deseqExita { get; set; }
        public string deseqInhibi { get; set; }
        public string fuerza { get; set; }
        public string debilidad { get; set; }
        public string movilidad { get; set; }
        public string inercia { get; set; }
        public string dinamPsiq { get; set; }
        public string pocoDinaPsiq { get; set; }
        public string labilidad { get; set; }
        public string actividad { get; set; }
        public string reactModer { get; set; }
        public string reactAlta { get; set; }
        public string resisAlta { get; set; }
        public string resisBaja { get; set; }
        public string ritmPsiRap { get; set; }
        public string ritmPsiLent { get; set; }
        public string sensibilidad { get; set; }
        public string pocaSensi { get; set; }
        public string extroversion { get; set; }
        public string introversion { get; set; }
        public string plasticidad { get; set; }
        public string rigidez { get; set; }
        public string porcientoColerico { get; set; }
        public string porcientoSanguineo { get; set; }
        public string porcientoFlematico { get; set; }
        public string porcientoMelancolico { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SujetosEvaluados> SujetosEvaluados { get; set; }
    }
}
