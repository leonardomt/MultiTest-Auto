using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multitest.VisualizarPruebas
{
    public partial class UserControlTodasPruebasTAB : UserControl
    {

        private static UserControlTodasPruebasTAB _instance;

        public static UserControlTodasPruebasTAB Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserControlTodasPruebasTAB();

                return _instance;
            }
        }
        public UserControlTodasPruebasTAB()
        {
            InitializeComponent();

            tabControl1.TabPages[0].Controls.Add(VisualizarPruebasTodas.Instance);
            VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
            VisualizarPruebasTodas.Instance.BringToFront();
            VisualizarPruebasTodas.Instance.cambiarNombre("Raven");






        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {



            if (e.TabPageIndex == 0)
            {

                tabControl1.TabPages[0].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("Raven");
            }

            if (e.TabPageIndex == 1)
            {
                tabControl1.TabPages[1].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("16PF");
            }

            if (e.TabPageIndex == 2)
            {
                tabControl1.TabPages[2].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("Eysenck");
            }

            if (e.TabPageIndex == 3)
            {
                tabControl1.TabPages[3].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("Test de Motivos Deportivos de Butt");
            }

            if (e.TabPageIndex == 4)
            {
                tabControl1.TabPages[4].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("IDARE (Situacional)");
            }

            if (e.TabPageIndex == 5)
            {
                tabControl1.TabPages[5].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("IDARE (Rasgo)");

            }

            if (e.TabPageIndex == 6)
            {
                tabControl1.TabPages[6].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("Dominó");

            }


            if (e.TabPageIndex == 7)
            {

                tabControl1.TabPages[7].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("POMS");

            }

            if (e.TabPageIndex == 8)
            {

                tabControl1.TabPages[8].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("Inventario Psicológico de Ejecución Deportiva(IPED)");

            }

            if (e.TabPageIndex == 9)
            {
                tabControl1.TabPages[9].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("Weil");

            }

            if (e.TabPageIndex == 10)
            {
                tabControl1.TabPages[10].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("Catell");

            }





            if (e.TabPageIndex == 11)
            {
                tabControl1.TabPages[11].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("Actitud ante la Competencia");

            }



            if (e.TabPageIndex == 12)
            {
                tabControl1.TabPages[12].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("Ansiedad Precompetitiva CSAI-2R");


            }

            if (e.TabPageIndex == 13)
            {
                tabControl1.TabPages[13].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("Tiempo de Reacción Simple");

            }

            if (e.TabPageIndex == 14)
            {
                tabControl1.TabPages[14].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("Tiempo de Reacción Complejo");

            }

            if (e.TabPageIndex == 15)
            {
                tabControl1.TabPages[15].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("Tiempo de Reacción Complejo con Sonido");
            }

            if (e.TabPageIndex == 16)
            {
                tabControl1.TabPages[16].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("Tiempo de Respuesta Anticipada");

            }

            if (e.TabPageIndex == 17)
            {
                tabControl1.TabPages[17].Controls.Add(VisualizarPruebasTodas.Instance);
                VisualizarPruebasTodas.Instance.Dock = DockStyle.Fill;
                VisualizarPruebasTodas.Instance.BringToFront();
                VisualizarPruebasTodas.Instance.cambiarNombre("Tabla Rojo y Negra");

            }



        }
    }
}
