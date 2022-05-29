using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Multitest.FormAux;
using System.Threading;
using System.IO.Ports;

namespace Multitest.PanelesPrincipales
{
    public partial class DeshailitadoControl1 : UserControl
    {

        private static DeshailitadoControl1 _instance;

        public static DeshailitadoControl1 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DeshailitadoControl1();

                return _instance;
            }
        }
        public DeshailitadoControl1()
        {
            InitializeComponent();
         
        }

        public void PonerNombreArduino()
        {
            label1.Text = "Debe tener el dispotivo Multitest conectado para realizar las pruebas.";
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
