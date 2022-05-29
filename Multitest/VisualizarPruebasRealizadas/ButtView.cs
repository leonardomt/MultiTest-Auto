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
    public partial class ButtView : UserControl
    {
        private static ButtView _instance;
        public MotivDeporButt prueba { get; set; }
        public static ButtView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ButtView();

                return _instance;
            }
        }

        public ButtView()
        {
            InitializeComponent();
            prueba = new MotivDeporButt();
        }


        public void buscarPrueba(String id)
        {
            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados inner join MotivDeporButt on SujetosEvaluados.PMotivDepButt =  MotivDeporButt.idTest where PMotivDepButt ='" + id + "'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();

                                label20.Text = res["Conflicto"].ToString() != "" ? res["Conflicto"].ToString() : "";
                                label18.Text = res["Rivalidad"].ToString() != "" ? res["Rivalidad"].ToString() : "";
                                label10.Text = res["Suficiencia"].ToString() != "" ? res["Suficiencia"].ToString() : "";

                                label22.Text = res["Cooperacion"].ToString() != "" ? res["Cooperacion"].ToString() : "";

                                label3.Text = res["calFilna"].ToString() != "" ? res["calFilna"].ToString() : "";

                                label8.Text = res["Agresividad"].ToString() != "" ? res["Agresividad"].ToString() : "";

                                label6.Text = res["Pregunta"].ToString() != "" ? res["Pregunta"].ToString() : "";

                                label12.Text = res["PuntuacionTotal"].ToString() != "" ? res["PuntuacionTotal"].ToString() + " ptos" : "";


                                //_-------------------//

                                prueba.Agresividad = res["Agresividad"].ToString();
                                prueba.Conflicto = res["Conflicto"].ToString();
                                prueba.Rivalidad = res["Rivalidad"].ToString();
                                prueba.Suficiencia = res["Suficiencia"].ToString();
                                prueba.Cooperacion = res["Cooperacion"].ToString();
                                prueba.PuntuacionTotal = res["PuntuacionTotal"].ToString();
                                prueba.Pregunta = res["Pregunta"].ToString();
                                prueba.calFilna = res["calFilna"].ToString();
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
