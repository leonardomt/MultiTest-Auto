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
    public partial class IpedView : UserControl
    {
        private static IpedView _instance;

        public Iped iped { get; set; }
        public static IpedView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IpedView();

                return _instance;
            }
        }

        public IpedView()
        {
            InitializeComponent();
            iped = new Iped();
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
                    using (SQLiteCommand command = new SQLiteCommand("select * from SujetosEvaluados inner join Iped on SujetosEvaluados.PIped =  Iped.idTest where PIped ='" + id + "'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();


                                String calAutoConfianza = res["calAutoConfianza"].ToString() != "" ? res["calAutoConfianza"].ToString() : "";
                                String calContAfrontNega = res["calContAfrontNega"].ToString() != "" ? res["calContAfrontNega"].ToString() : "";
                                String calContAten = res["calContAten"].ToString() != "" ? res["calContAten"].ToString() : "";
                                String calContVisuo = res["calContVisuo"].ToString() != "" ? res["calContVisuo"].ToString() : "";

                                String calNivelMot = res["calNivelMot"].ToString() != "" ? res["calNivelMot"].ToString() : "";
                                String calContAfroPos = res["calContAfroPos"].ToString() != "" ? res["calContAfroPos"].ToString() : "";
                                String calContActitud = res["calContActitud"].ToString() != "" ? res["calContActitud"].ToString() : "";



                                label19.Text = res["Autoconfianza"].ToString() != "" ? res["Autoconfianza"].ToString() + "(" + calAutoConfianza + ")" : "";
                                label7.Text = res["ContVisuoimag"].ToString() != "" ? res["ContVisuoimag"].ToString() + "(" + calContVisuo + ")" : "";
                                label18.Text = res["ContAfronNegativ"].ToString() != "" ? res["ContAfronNegativ"].ToString() + "(" + calContAfrontNega + ")" : "";
                                label23.Text = res["ContAtencional"].ToString() != "" ? res["ContAtencional"].ToString() + "(" + calContAten + ")" : "";


                                label20.Text = res["NivelMotiv"].ToString() != "" ? res["NivelMotiv"].ToString() + "(" + calNivelMot + ")" : "";
                                label9.Text = res["ContActitudinal"].ToString() != "" ? res["ContActitudinal"].ToString() + "(" + calContActitud + ")" : "";
                                label5.Text = res["ContAfrontPositiv"].ToString() != "" ? res["ContAfrontPositiv"].ToString() + "(" + calContAfroPos + ")" : "";


                                iped.Autoconfianza = label19.Text;
                                iped.ContVisuoimag = label7.Text;
                                iped.ContAfronNegativ = label18.Text;
                                iped.ContAtencional = label23.Text;

                                iped.NivelMotiv = label20.Text;
                                iped.ContActitudinal = label9.Text;
                                iped.ContAfrontPositiv = label5.Text;




                                //label12.Text = "Calificacion final: " + res["calFinal"].ToString() != "" ? "Calificacion final: " + res["calFinal"].ToString() : "Calificacion final:";



                                //label13.Text= res["calAutoConfianza"].ToString() != "" ? res["calAutoConfianza"].ToString() : "";
                                //label8.Text = res["calContAfrontNega"].ToString() != "" ? res["calContAfrontNega"].ToString() : "";
                                //label27.Text = res["calContAten"].ToString() != "" ? res["calContAten"].ToString() : "";
                                //label31.Text = res["calContVisuo"].ToString() != "" ? res["calContVisuo"].ToString() : "";

                                //label25.Text = res["calNivelMot"].ToString() != "" ? res["calNivelMot"].ToString() : "";
                                //label29.Text = res["calContAfroPos"].ToString() != "" ? res["calContAfroPos"].ToString() : "";
                                //label32.Text = res["calContActitud"].ToString() != "" ? res["calContActitud"].ToString() : "";

                            }
                        }
                    }
                }

            }
        }
    }
}
