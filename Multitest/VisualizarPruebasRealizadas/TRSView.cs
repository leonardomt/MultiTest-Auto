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
    public partial class TRSView : UserControl
    {

        private static TRSView _instance;

        public static TRSView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TRSView();

                return _instance;
            }
        }

        public TRSView()
        {
            InitializeComponent();
        }

        public void cambiarNombreAtleta(String nombreAtleta)
        {
            //label2.Text = nombreAtleta;
        }
    }
}
