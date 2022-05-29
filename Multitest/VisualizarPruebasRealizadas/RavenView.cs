using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    public partial class RavenView : UserControl
    {

        private static RavenView _instance;
       public PruRaven prueba { set; get; }

        public static RavenView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RavenView();

                return _instance;
            }
        }


        public RavenView()
        {
            InitializeComponent();
            prueba = new PruRaven();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }



        public void buscarPrueba(String id)
        {
            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados inner join PruRaven on SujetosEvaluados.PRaven =  PruRaven.idTest where PRaven='" + id + "'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();

                                label9.Text = res["PuntajeA"].ToString() != "" ? res["PuntajeA"].ToString() + " ptos" : "";
                                label10.Text = res["PuntajeB"].ToString() != "" ? res["PuntajeB"].ToString() + " ptos" : "";
                                label11.Text = res["PuntajeC"].ToString() != "" ? res["PuntajeC"].ToString() + " ptos" : "";
                                label12.Text = res["PuntajeD"].ToString() != "" ? res["PuntajeD"].ToString() + " ptos" : "";
                                label13.Text = res["PuntajeE"].ToString() != "" ? res["PuntajeE"].ToString() + " ptos" : "";

                                label19.Text = res["PuntajeTotal"].ToString() != "" ? res["PuntajeTotal"].ToString() + " ptos" : "";
                                label18.Text = res["Rango"].ToString() != "" ? res["Rango"].ToString() : "";
                                label20.Text = res["Porcentaje"].ToString() != "" ? res["Porcentaje"].ToString() : "";

                                label23.Text = res["Consistencia"].ToString() != "" ? "Si" : "No";
                                label21.Text = res["Diagnostico"].ToString();
                                //--------------------------------------------------------------//

                                prueba.PuntajeA = res["PuntajeA"].ToString();
                                prueba.PuntajeB = res["PuntajeB"].ToString();
                                prueba.PuntajeC = res["PuntajeC"].ToString();
                                prueba.PuntajeD = res["PuntajeD"].ToString();
                                prueba.PuntajeE = res["PuntajeE"].ToString();

                                prueba.PuntajeTotal = res["PuntajeTotal"].ToString();
                                prueba.Rango = res["Rango"].ToString();
                                prueba.Porcentaje = res["Porcentaje"].ToString();
                                prueba.Consistencia = res["Consistencia"].ToString() != "" ? "Si" : "No";
                                prueba.Diagnostico = res["Diagnostico"].ToString();
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
