using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitest
{
    public class Result
    {
        public bool sonido { get; set; }
        public string colorPantalla { get; set; }
        public string colorArduino { get; set; }
        public string tiempo { get; set; }

        public Result(bool sonido, string colorPantalla, string colorArduino, string tiempo)
        {
            this.sonido = sonido;
            this.colorPantalla = colorPantalla;
            this.colorArduino = colorArduino;
            this.tiempo = tiempo;
        }
    }
}
