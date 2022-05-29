using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multitest
{
    public partial class TRC_Form2 : Form
    {
        public bool sonido { get; set; }
        public int cant { get; set; }
        public string variante { get; set; }

        public bool resultado = false;

        public bool entrenamiento { get; set; }
        public TRC_Form2(bool entrenamiento,bool sonido)
        {
            InitializeComponent();
         
            this.entrenamiento = entrenamiento;
            checkBox2.Checked = entrenamiento;

            if (sonido == true)
                label6.Text = "Si";
            else
                label6.Text = "No";

        }



        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

       

        private void button1_Click(object sender, EventArgs e)
        {

            variante = "v" + numericUpDown2.Value.ToString();
            cant = Convert.ToInt32(numericUpDown1.Value);
            resultado = true;
            this.button1.DialogResult = DialogResult.OK;
            Close();

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
                entrenamiento = true;
            else
                entrenamiento = false;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
         //   Application.Exit();
        }

        private void label257_Click(object sender, EventArgs e)
        {

        }
    }
}
