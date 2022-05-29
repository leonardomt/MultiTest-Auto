using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitest.AuxClass
{
    class FactorAB
    {
        public String factor { get; set; }

        public int puntuacionAB { get; set; }

        public int puntuacionAP { get; set; }
        public FactorAB(String factor, int puntuacionAB,int puntuacionAP)
        {
            this.factor = factor;
            this.puntuacionAP = puntuacionAP;
            this.puntuacionAB = puntuacionAB;
        }


    }
}
