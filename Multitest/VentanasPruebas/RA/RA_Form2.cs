using System;
using System.Windows.Forms;

namespace Multitest
{
    public partial class RA_Form2 : Form
    {
        public int entrenamiento { get; set; }
        public String variante { get; set; }

        public bool resultado = false;
        public RA_Form2(int entrenamiento)
        {
            InitializeComponent();
            entrenamiento = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            entrenamiento = 10;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            entrenamiento = 5;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            variante = "P" + numericUpDown1.Value;
            this.button1.DialogResult = DialogResult.OK;
            resultado = true;
            Close();
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
