using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitest
{
    public class Auxiliar
    {
        public String tiempo { get; set; }
        public String Resultado { get; set; }
        public String variante { get; set; }
        public String diferencia { get; set; }


        public String sentidoVelocidad { get; set; }


        public Auxiliar(String tiempo, String variante, String resultado, String diferencia,String sentidoVelocidad)
        {
            this.tiempo = tiempo;
            this.variante = variante;
            this.Resultado = resultado;
            this.diferencia = diferencia;
            this.sentidoVelocidad = sentidoVelocidad;
        }


        public Auxiliar(String tiempo, String variante, String resultado, String diferencia)
        {
            this.tiempo = tiempo;
            this.variante = variante;
            this.Resultado = resultado;
            this.diferencia = diferencia;
     
        }
    }
}
