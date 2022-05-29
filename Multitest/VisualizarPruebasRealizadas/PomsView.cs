using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Multitest.ADOmodel;
using System.Data.SQLite;
using Multitest.FormAux;

namespace Multitest.VisualizarPruebasRealizadas
{
    public partial class PomsView : UserControl
    {


        private static PomsView _instance;

        public PruPoms poms { get; set; }
        public static PomsView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PomsView();

                return _instance;
            }
        }


        public PomsView()
        {
            InitializeComponent();

            poms = new PruPoms();
        }


        public void cambiarNombreAtleta(String nombreAtleta, String fecha)
        {
            label2.Text = nombreAtleta;
            label24.Text = fecha;
        }


        public void buscarPrueba(String id)
        {
            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados inner join PruPoms on SujetosEvaluados.PPoms =  PruPoms.idTest where PPoms ='" + id + "'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();


                                poms.TensionAnsiedad = res["TensionAnsiedad"].ToString();
                                poms.DepresionMelancolia = res["DepresionMelancolia"].ToString();
                                poms.AngustiaHostilidad = res["AngustiaHostilidad"].ToString();
                                poms.VigorActividad = res["VigorActividad"].ToString();
                                poms.FatigaInercia = res["FatigaInercia"].ToString();
                                poms.ConfusionDesorient = res["ConfusionDesorient"].ToString();
                                poms.Amistosidad = res["Amistosidad"].ToString();


                                label19.Text = res["TensionAnsiedad"].ToString() != "" ? res["TensionAnsiedad"].ToString() + " ptos" : "";
                                label11.Text = res["DepresionMelancolia"].ToString() != "" ? res["DepresionMelancolia"].ToString() + " ptos" : "";
                                label20.Text = res["AngustiaHostilidad"].ToString() != "" ? res["AngustiaHostilidad"].ToString() + " ptos" : "";
                                label18.Text = res["VigorActividad"].ToString() != "" ? res["VigorActividad"].ToString() + " ptos" : "";


                                label6.Text = res["FatigaInercia"].ToString() != "" ? res["FatigaInercia"].ToString() + " ptos" : "";
                                label7.Text = res["ConfusionDesorient"].ToString() != "" ? res["ConfusionDesorient"].ToString() + " ptos" : "";
                                label8.Text = res["Amistosidad"].ToString() != "" ? res["Amistosidad"].ToString() + " ptos" : "";



                            }
                        }
                    }
                }

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GraficoPoms grafico = new GraficoPoms(poms,label2.Text);
            grafico.ShowDialog();
        }
    }
}
