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
    public partial class TRCSView : UserControl
    {
        private static TRCSView _instance;

        public static TRCSView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TRCSView();

                return _instance;
            }
        }

        public TRCSView()
        {
            InitializeComponent();
        }

        public void cambiarNombreAtleta(String nombreAtleta)
        {
            label2.Text = nombreAtleta;
        }
    }
}
