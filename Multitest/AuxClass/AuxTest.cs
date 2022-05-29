using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitest.AuxClass
{
    class AuxTest
    {
        public String valorDown { get; set; }
        public String valorUP { get; set; }
        public String figura { get; set; }

        public AuxTest(String valor, String figura)
        {
            this.valorUP = valor;
            this.figura = figura;
        }

        public AuxTest(String valor, String figura, String valorDown)
        {
            this.valorUP = valor;
            this.figura = figura;
            this.valorDown= valorDown;
        }
    }
}
