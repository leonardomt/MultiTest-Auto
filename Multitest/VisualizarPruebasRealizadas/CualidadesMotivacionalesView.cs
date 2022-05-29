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
    public partial class CualidadesMotivacionalesView : UserControl
    {
        private static CualidadesMotivacionalesView _instance;

        public CualidMotivDeportiv cualidades { set; get; }
        public static CualidadesMotivacionalesView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CualidadesMotivacionalesView();

                return _instance;
            }
        }
        public CualidadesMotivacionalesView()
        {
            InitializeComponent();
            cualidades = new CualidMotivDeportiv();
        }

        public void buscarPrueba(String id)
        {
            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados inner join CualidMotivDeportiv on SujetosEvaluados.PCualidMotivDepor =  CualidMotivDeportiv.idTest where PCualidMotivDepor ='" + id + "'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();



                                label19.Text = res["motivLogro"].ToString() != "" ? res["motivLogro"].ToString() : "";
                                label11.Text = res["noMotivLogro"].ToString() != "" ? res["noMotivLogro"].ToString() : "";



                                label20.Text = res["motivIntrínseca"].ToString() != "" ? res["motivIntrínseca"].ToString() : "";
                                label18.Text = res["motivExtrínseca"].ToString() != "" ? res["motivExtrínseca"].ToString() : "";

                                label10.Text = res["expecExito"].ToString() != "" ? res["expecExito"].ToString() : "";
                                label22.Text = res["expecEficacia"].ToString() != "" ? res["expecEficacia"].ToString() : "";

                                label8.Text = res["motivAproExito"].ToString() != "" ? res["motivAproExito"].ToString() : "";
                                label25.Text = res["movEvitarFracaso"].ToString() != "" ? res["movEvitarFracaso"].ToString() : "";


                                label13.Text = res["motivMater"].ToString() != "" ? res["motivMater"].ToString() : "";
                                label27.Text = res["motivRecono"].ToString() != "" ? res["motivRecono"].ToString() : "";



                                label6.Text = res["motivAutoDeportiva"].ToString() != "" ? res["motivAutoDeportiva"].ToString() : "";
                                label29.Text = res["motivAutoPersono"].ToString() != "" ? res["motivAutoPersono"].ToString() : "";




                                label31.Text = res["motivSuprain"].ToString() != "" ? res["motivSuprain"].ToString() : "";


                                cualidades.motivLogro = label19.Text;
                                cualidades.noMotivLogro = label11.Text;
                                cualidades.motivIntrínseca = label20.Text;
                                cualidades.motivExtrínseca = label18.Text;
                                cualidades.expecExito = label10.Text;
                                cualidades.expecEficacia = label22.Text;
                                cualidades.motivAproExito = label8.Text;
                                cualidades.movEvitarFracaso = label25.Text;
                                cualidades.motivMater = label13.Text;
                                cualidades.motivRecono = label27.Text;
                                cualidades.motivAutoDeportiva = label6.Text;
                                cualidades.motivAutoPersono = label29.Text;
                                cualidades.motivSuprain = label31.Text;

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
