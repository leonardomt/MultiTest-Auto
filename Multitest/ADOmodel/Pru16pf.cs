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
    
    public partial class Pru16pf
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pru16pf()
        {
            this.SujetosEvaluados = new HashSet<SujetosEvaluados>();
        }
    
        public long idTest { get; set; }
        public string DuraPru { get; set; }
        public string AnotBrutaMD { get; set; }
        public string AnotBrutaA { get; set; }
        public string AnotBrutaB { get; set; }
        public string AnotBrutaC { get; set; }
        public string AnotBrutaE { get; set; }
        public string AnotBrutaF { get; set; }
        public string AnotBrutaG { get; set; }
        public string AnotBrutaH { get; set; }
        public string AnotBrutaI { get; set; }
        public string AnotBrutaL { get; set; }
        public string AnotBrutaM { get; set; }
        public string AnotBrutaN { get; set; }
        public string AnotBrutaO { get; set; }
        public string AnotBrutaQ1 { get; set; }
        public string AnotBrutaQ2 { get; set; }
        public string AnotBrutaQ3 { get; set; }
        public string AnotBrutaQ4 { get; set; }
        public string AnotPesadaA { get; set; }
        public string AnotPesadaB { get; set; }
        public string AnotPesadaC { get; set; }
        public string AnotPesadaE { get; set; }
        public string AnotPesadaF { get; set; }
        public string AnotPesadaG { get; set; }
        public string AnotPesadaH { get; set; }
        public string AnotPesadaI { get; set; }
        public string AnotPesadaL { get; set; }
        public string AnotPesadaM { get; set; }
        public string AnotPesadaN { get; set; }
        public string AnotPesadaO { get; set; }
        public string AnotPesadaQ1 { get; set; }
        public string AnotPesadaQ2 { get; set; }
        public string AnotPesadaQ3 { get; set; }
        public string AnotPesadaQ4 { get; set; }
        public string Perfil1 { get; set; }
        public string Perfil2 { get; set; }
        public string Perfil3 { get; set; }
        public string Perfil4 { get; set; }
        public string Fecha { get; set; }
        public string Neuroticismo { get; set; }
        public string Distorsion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SujetosEvaluados> SujetosEvaluados { get; set; }
    }
}
