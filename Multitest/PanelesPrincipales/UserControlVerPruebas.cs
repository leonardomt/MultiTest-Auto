using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Multitest.VisualizarPruebas;
using Multitest.FormAux;

namespace Multitest.PanelesPrincipales
{
    public partial class UserControlVerPruebas : UserControl
    {
        private static UserControlVerPruebas _instance;
        bool usuarioPanel = false;
        bool pruebaPanel = false;
        public static UserControlVerPruebas Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserControlVerPruebas();

                return _instance;
            }
        }

        public UserControlVerPruebas()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Esperar d = new Esperar();
            d.Show();
            Application.DoEvents();
            button1.BackColor = Color.FromArgb(55, 79, 105);
            button1.ForeColor = Color.White;
            button1.Image = global::Multitest.Properties.Resources.test_passed;

            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;
            Application.DoEvents();
            button2.Image = global::Multitest.Properties.Resources.icons8_user_20;
            panel1.Controls.Add(UserControlTodasPruebasTAB.Instance);
            UserControlTodasPruebasTAB.Instance.Dock = DockStyle.Fill;
            UserControlTodasPruebasTAB.Instance.BringToFront();
            ActiveControlUser.Instance.verResultados = 1;


            d.Close();

            // VisualizarPruebasTodas.Instance.LimpiarCampos();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Esperar d = new Esperar();
            d.Show();
            Application.DoEvents();
            button2.BackColor = Color.FromArgb(55, 79, 105);
            button2.ForeColor = Color.White;
            button2.Image = global::Multitest.Properties.Resources.user_20;

            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;
            button1.Image = global::Multitest.Properties.Resources.icons8_test_passed_20;
            Application.DoEvents();
            panel1.Controls.Add(UserControlVisualizarPruebaAtleta.Instance);
            UserControlVisualizarPruebaAtleta.Instance.Dock = DockStyle.Fill;
            UserControlVisualizarPruebaAtleta.Instance.BringToFront();
            ActiveControlUser.Instance.verResultados = 2;
            d.Close();
            //    UserControlVisualizarPruebaAtleta.Instance.LimpiarCampos();
        }

        

    }
}
