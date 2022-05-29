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
    public partial class DominoView : UserControl


    {
        private static DominoView _instance;
        public PruDomino prueba { set; get; }

        public static DominoView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DominoView();

                return _instance;
            }
        }

        public DominoView()
        {
            InitializeComponent();
            prueba = new PruDomino();
        }

        public void buscarPrueba(String id)
        {
            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados inner join PruDomino on SujetosEvaluados.PDomino =  PruDomino.idTest where PDomino='" + id + "' ", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();

                                label19.Text = res["Puntaje"].ToString() != "" ? res["Puntaje"].ToString() + " ptos" : "";

                                label20.Text = res["Porcentaje"].ToString() != "" ? res["Porcentaje"].ToString() + " ptos" : "";
                                label18.Text = res["Rango"].ToString() != "" ? res["Rango"].ToString() : "";

                                label21.Text = res["Diagnostico"].ToString() != "" ? res["Diagnostico"].ToString() : "";
                                //  label23.Text = res["DuraPru"].ToString() != "" ? res["DuraPru"].ToString() : "";


                                //-------------------------------------------------------------//

                                prueba.Puntaje = res["Puntaje"].ToString() != "" ? res["Puntaje"].ToString() : "";
                                prueba.Rango = res["Rango"].ToString() != "" ? res["Rango"].ToString() : "";
                                prueba.Diagnostico = res["Diagnostico"].ToString() != "" ? res["Diagnostico"].ToString() : "";
                                prueba.Porcentaje = res["Porcentaje"].ToString() != "" ? res["Porcentaje"].ToString() : "";
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
