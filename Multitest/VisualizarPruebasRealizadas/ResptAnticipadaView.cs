using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multitest.VisualizarPruebasRealizadas
{
    public partial class ResptAnticipadaView : UserControl
    {
        private static ResptAnticipadaView _instance;

        public static ResptAnticipadaView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ResptAnticipadaView();

                return _instance;
            }
        }
        public ResptAnticipadaView()
        {
            InitializeComponent();
        }

        public void cambiarNombreAtleta(String nombreAtleta)
        {
            label2.Text = nombreAtleta;
        }
    }
}
