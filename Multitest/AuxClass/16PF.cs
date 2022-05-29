using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitest.AuxClass
{
    class _16PF
    {
        public String Pregunta { get; set; }
        public String A { get; set; }
        public String B { get; set; }
        public String C { get; set; }

        public String  valor{ get; set; }

        public _16PF(String Pregunta, String A, String B, String C)
        {
            this.Pregunta = Pregunta;
            this.A = A;
            this.B = B;
            this.C = C;
        }
    }
}
