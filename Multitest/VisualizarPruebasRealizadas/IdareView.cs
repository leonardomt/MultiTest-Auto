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
    public partial class IdareView : UserControl
    {
        private static IdareView _instance;

        public PruIdareRago idareRasgo { set; get; }

        public PruIdareSituacional idareSit { set; get; }

        public static IdareView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IdareView();

                return _instance;
            }
        }

        public IdareView()
        {
            InitializeComponent();

            idareRasgo = new PruIdareRago();
            idareSit = new PruIdareSituacional();
        }

        public void buscarPrueba(String id, string tabla)
        {
            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    string comand = "";
                    if (tabla == "Situacional")
                    {
                        comand = "select * from SujetosEvaluados inner join  PruIdareSituacional on SujetosEvaluados.PIdareSitua =  PruIdareSituacional.idTest where PIdareSitua ='" + id + "'";
                        label14.Text = "Puntaje Ansiedad Situacional:";
                        label15.Text = "Diagnóstico Ansiedad Situacional:";
                        //label3.Text = "Test Idare (Situacional)";
                    }

                    else
                    {
                        comand = "select * from SujetosEvaluados inner join  PruIdareRago on SujetosEvaluados.PIdareRasgo =  PruIdareRago.idTest where PIdareRasgo ='" + id + "'";
                        label14.Text = "Puntaje Ansiedad de Rasgo:";
                        label15.Text = "Diagnóstico Ansiedad de Rasgo:";
                        // label3.Text = "Test Idare (Rasgo)";
                    }



                    using (SQLiteCommand command = new SQLiteCommand(comand, ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();


                                if (tabla == "Situacional")
                                {
                                    label19.Text = res["PAnsiedadSituacional"].ToString() != "" ? res["PAnsiedadSituacional"].ToString() + " ptos" : "";
                                    label18.Text = res["DiagAnsSituacional"].ToString() != "" ? res["DiagAnsSituacional"].ToString() : "";

                                    idareSit.PAnsiedadSituacional = res["PAnsiedadSituacional"].ToString();
                                    idareSit.DiagAnsSituacional = res["DiagAnsSituacional"].ToString();
                                }
                                else
                                {
                                    label19.Text = res["PAnsiedadRasgo"].ToString() != "" ? res["PAnsiedadRasgo"].ToString() + " ptos" : "";
                                    label18.Text = res["DiagAnsRasgo"].ToString() != "" ? res["DiagAnsRasgo"].ToString() : "";

                                    idareRasgo.PAnsiedadRasgo = res["PAnsiedadRasgo"].ToString();
                                    idareRasgo.DiagAnsRasgo = res["DiagAnsRasgo"].ToString();
                                }

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
