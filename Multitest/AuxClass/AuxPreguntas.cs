using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitest.AuxClass
{
    class AuxPreguntas
    {
        public  String valor { get; set; }
        public String pregunta { get; set; }

        public AuxPreguntas(String valor, String pregunta)
        {
            this.valor = valor;
            this.pregunta = pregunta;
          
        }
    }
}
