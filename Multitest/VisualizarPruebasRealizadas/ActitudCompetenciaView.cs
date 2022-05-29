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
    public partial class ActitudCompetenciaView : UserControl
    {
        private static ActitudCompetenciaView _instance;

        public ActitudAnteCompetencia actitud { set; get; }

        public static ActitudCompetenciaView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ActitudCompetenciaView();

                return _instance;
            }
        }

        public ActitudCompetenciaView()
        {
            InitializeComponent();
            actitud = new ActitudAnteCompetencia();
        }


        public void buscarPrueba(String id)
        {
            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados inner join ActitudAnteCompetencia on SujetosEvaluados.PActiAnteComp =  ActitudAnteCompetencia.idTest where PActiAnteComp ='" + id + "'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();



                                label19.Text = res["Certeza"].ToString() != "" ? res["Certeza"].ToString() : "";
                                label20.Text = res["Opinion"].ToString() != "" ? res["Opinion"].ToString() : "";
                                label10.Text = res["Contrario"].ToString() != "" ? res["Contrario"].ToString() : "";
                                label8.Text = res["Significacion"].ToString() != "" ? res["Significacion"].ToString() : "";

                                 
                                label4.Text = res["ptoCerteza"].ToString() != "" ? res["ptoCerteza"].ToString() : "";
                                label9.Text = res["ptoContrio"].ToString() != "" ? res["ptoContrio"].ToString() : "";
                                label12.Text = res["ptoSignificacion"].ToString() != "" ? res["ptoContrio"].ToString() : "";
                                label15.Text = res["ptoOpinion"].ToString() != "" ? res["ptoOpinion"].ToString() : "";

                                actitud.Certeza = label19.Text;
                                actitud.Opinion = label20.Text;
                                actitud.Contrario = label10.Text;
                                actitud.Significacion = label8.Text;

                                actitud.ptoCerteza = label4.Text;
                                actitud.ptoOpinion = label9.Text;
                                actitud.ptoContrio = label12.Text;
                                actitud.ptoSignificacion = label15.Text;



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
