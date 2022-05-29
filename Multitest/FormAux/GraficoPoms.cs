using Multitest.ADOmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multitest.FormAux
{
    public partial class GraficoPoms : Form
    {
        List<Label> listLabel;
        public GraficoPoms(PruPoms poms,string nombre)
        {
            InitializeComponent();

            llenarGrafico(poms,nombre);
        }

        private void llenarGrafico(PruPoms poms, string nombre)
        {
            label334.Text = poms.TensionAnsiedad;
            label335.Text = poms.DepresionMelancolia;
            label336.Text = poms.AngustiaHostilidad;
            label337.Text = poms.VigorActividad;
            label338.Text = poms.FatigaInercia;
            label339.Text = poms.ConfusionDesorient;
            label71.Text = nombre;

            listLabel = new List<Label>();
            Label tension = buscarLabel("T", poms.TensionAnsiedad);
            Label depresion = buscarLabel("D", poms.DepresionMelancolia);
            Label angustia = buscarLabel("N", poms.AngustiaHostilidad);
            Label vigor = buscarLabel("V", poms.VigorActividad);
            Label fatiga = buscarLabel("F", poms.FatigaInercia);
            Label confusion = buscarLabel("C", poms.ConfusionDesorient);

            listLabel.Add(tension);
            listLabel.Add(depresion);
            listLabel.Add(angustia);
            listLabel.Add(vigor);
            listLabel.Add(fatiga);
            listLabel.Add(confusion);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Label buscarLabel(String letra, String valor)
        {
            Label lab = null;
            foreach (var item in tableLayoutPanel1.Controls)
            {
                Label temp = item as Label;


                if (temp.Name.Contains(letra) && temp.Text == valor)
                {
                    lab = temp;
                }
            }

            return lab;
        }

        private void Label190_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < listLabel.Count; i++)
            {
                Pen pen = new Pen(Color.Red);

                if (i + 1 != listLabel.Count)
                    e.Graphics.DrawLine(pen, listLabel[i].Location.X, listLabel[i].Location.Y, listLabel[i + 1].Location.X, listLabel[i + 1].Location.Y);
            }
        }



    }
}
