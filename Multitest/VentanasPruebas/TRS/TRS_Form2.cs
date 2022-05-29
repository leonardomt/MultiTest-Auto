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
    public partial class TRS_Form2 : Form
    {
        public String color { get; set; }
        public int cant { get; set; }
        public TRS_Form2()
        {
            InitializeComponent();
        }



        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            cant = Convert.ToInt32(numericUpDown1.Value);
            if (cant > 0)
            {
                if (radioButton1.Checked)
                    color = "Amarillo";
                else
                    color = "Rojo";


                this.button1.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("La cantidad de estimulos debe ser mayor a cero");
            }



        }
    }
}
