using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitest.AuxClass
{
    class Edad
    {
        public int edadMin { get; set; }
        public int edadMax { get; set; }

        public List<int> puntos { get; set; }

        public Edad(int edadMin, int edadMax, List<int> puntos)
        {
            this.edadMin = edadMin;
            this.edadMax = edadMax;
            this.puntos = puntos;

        }
    }
}
