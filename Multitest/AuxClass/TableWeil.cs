using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitest.AuxClass
{
    public class TableWeil
    {
        public string Edad { get; set; }
        public List<int> column { get; set; }
        public TableWeil(string Edad, List<int> column)
        {
            this.Edad = Edad;
            this.column = column;
        }
    }
}
