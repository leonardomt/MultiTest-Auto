using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Multitest.ADOmodel;

namespace Multitest.VisualizarPruebasRealizadas
{
    public partial class WeilView :  UserControl
    {


        private static WeilView _instance;
        public PruWeil prueba { get; set; }

        public static WeilView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WeilView();

                return _instance;
            }
        }

        public WeilView()
        {
            InitializeComponent();
            prueba = new PruWeil();
        }


        public void buscarPrueba(String id)
        {
            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados inner join PruWeil on SujetosEvaluados.PWeil =  PruWeil.idTest where PWeil ='" + id + "'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();



                                label19.Text = res["PuntajeTotal"].ToString() != "" ? res["PuntajeTotal"].ToString() + " ptos" : "";
                                label18.Text = res["Rango"].ToString() != "" ? res["Rango"].ToString() : "";
                                label20.Text = res["Porcentaje"].ToString() != "" ? res["Porcentaje"].ToString() + " ptos" : "";

                                label21.Text = res["Diagnostico"].ToString() != "" ? res["Diagnostico"].ToString() : "";


                                

                                //---------------------------------------------------------------//

                                prueba.Diagnostico = res["Diagnostico"].ToString();
                                prueba.Rango = res["Rango"].ToString();
                                prueba.Porcentaje = res["Porcentaje"].ToString();
                                prueba.PuntajeTotal = res["PuntajeTotal"].ToString();
                            }
                        }
                    }
                }

            }
        }

        public void cambiarNombreAtleta(String nombreAtleta, String fecha)
        {
            label2.Text = nombreAtleta;
            label24.Text = fecha;
        }

    }
}
