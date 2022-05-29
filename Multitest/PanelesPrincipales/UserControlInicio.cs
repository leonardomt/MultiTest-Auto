using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multitest.PanelesPrincipales
{
    public partial class UserControlInicio : UserControl
    {

        private static UserControlInicio _instance;
         
        public static UserControlInicio Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserControlInicio();

                return _instance;
            }
        }

        public UserControlInicio()
        {
            InitializeComponent();
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            Random r = new Random();
            label1.ForeColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), 0);
        }
    }
}
