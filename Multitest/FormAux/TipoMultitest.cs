using Multitest.AuxClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Multitest.FormAux
{
    public partial class TipoMultitest : Form
    {
        bool radio1 = false;
        bool radio2 = false;
        public TipoMultitest()
        {
            InitializeComponent();


            VerificarHardware res = new VerificarHardware();
            res.configurarHardware();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!radio1)
            {
                if (radioButton1.Checked)
                {
                    String tipoMultitest = "Arduino";
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    xmlDoc.SelectSingleNode("//appSettings/add[@key='Tipomultitest']").Attributes["value"].Value = tipoMultitest;
                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    ConfigurationManager.RefreshSection("appSettings");
                    Form1 f = new Form1( );
                    f.añadirPanelPrueba();
                }
            }
            else
                radio1 = false;
             

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!radio2)
            {
                if (radioButton2.Checked)
                {
                    String tipoMultitest = "FTDI";
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    xmlDoc.SelectSingleNode("//appSettings/add[@key='Tipomultitest']").Attributes["value"].Value = tipoMultitest;
                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    ConfigurationManager.RefreshSection("appSettings");
                    Form1 f = new Form1( );
                    f.añadirPanelPrueba();
                }
            }
            else
                radio2 = false;
             
        }

        private void TipoMultitest_Load(object sender, EventArgs e)
        {
            radio1 = true;
            radio2 = true;

            String temp = ConfigurationManager.AppSettings["Tipomultitest"];
            if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                radioButton1.Checked = true;
            if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                radioButton2.Checked = true;

            radio1 = false;
            radio2 = false;


        }
    }
}
