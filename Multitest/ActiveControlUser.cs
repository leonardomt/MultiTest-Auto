using Multitest.VisualizarPruebas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multitest
{
    public class ActiveControlUser
    {
        List<String> listControl = new List<String>();
        public int verResultados { set; get; }
        public bool inTest { set; get; }

        private static ActiveControlUser _instance;

        public bool enPanelPrueba { set; get; }

        public ActiveControlUser()
        {
            enPanelPrueba = false;
            verResultados = 0;
        }

        public static ActiveControlUser Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ActiveControlUser();

                return _instance;
            }
        }

        public void addControl(String res)
        {
            listControl.Add(res);
        }

        public void upDateEtapa(String etapa)
        {

            foreach (String item in listControl)
            {
                if (item == "VisualizarPruebasTodas")
                {
                    VisualizarPruebasTodas.Instance.cambiarEtapa();
                }
                if (item == "UserControlVisualizarPruebaAtleta")
                {
                    UserControlVisualizarPruebaAtleta.Instance.cambiarEtapa();
                }
                if (item == "UserControlPruebas")
                {
                    UserControlPruebas.Instance.RefrescarEtapa();
                }
            }
        }



    }
}
