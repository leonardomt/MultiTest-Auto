using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitest.AuxClass
{
    class _16PfTablePercentil
    {
        public int factorAB { get; set; }
        public List<String> factores { get; set; }

        public _16PfTablePercentil(int factorAB, List<String> factores)
        {
            this.factorAB = factorAB;
            this.factores = factores;
                
        }

    }
}
