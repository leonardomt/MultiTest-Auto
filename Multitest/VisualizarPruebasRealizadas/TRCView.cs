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
    public partial class TRCView : UserControl
    {

        private static TRCView _instance;

        public static TRCView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TRCView();

                return _instance;
            }
        }
        public TRCView()
        {
            InitializeComponent();
        }

        public void cambiarNombreAtleta(String nombreAtleta)
        {
           // label2.Text = nombreAtleta;
        }
    }
}
