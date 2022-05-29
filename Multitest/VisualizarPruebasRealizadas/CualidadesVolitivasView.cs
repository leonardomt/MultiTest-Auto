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
    public partial class CualidadesVolitivasView : UserControl
    {
        int PtoAutoIndepen = 0;
        int PtoTenacidadResol = 0;
        int PtoPersePersis = 0;
        int PtoAutodAutocon = 0;


        private static CualidadesVolitivasView _instance;

        public CualiVolitivasDep cualidades { set; get; }
        public static CualidadesVolitivasView Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CualidadesVolitivasView();

                return _instance;
            }
        }

        public CualidadesVolitivasView()
        {
            InitializeComponent();
            cualidades = new CualiVolitivasDep();
        }


        public void GraficoExportar()
        {
      
        }
        public void buscarPrueba(String id)
        {
            using (mainEntities db = new mainEntities())
            {

                using (SQLiteConnection ne = new SQLiteConnection(db.Database.Connection.ConnectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand("select * from CualiVolitivasDep inner join SujetosEvaluados on SujetosEvaluados.PCualiVolitiv =  CualiVolitivasDep.idTest where PCualiVolitiv ='" + id + "'", ne))
                    {
                        ne.Open();
                        using (SQLiteDataReader res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                res.Read();



                                PtoAutoIndepen = Convert.ToInt32(res["PtoAutoIndepen"].ToString());
                                PtoTenacidadResol = Convert.ToInt32(res["PtoTenacidadResol"].ToString());
                                PtoPersePersis = Convert.ToInt32(res["PtoPersePersis"].ToString());
                                PtoAutodAutocon = Convert.ToInt32(res["PtoAutodAutocon"].ToString());


                                label19.Text = res["PtoAutoIndepen"].ToString() != "" ? res["PtoAutoIndepen"].ToString() + " ptos" : "";
                                label20.Text = res["PtoTenacidadResol"].ToString() != "" ? res["PtoTenacidadResol"].ToString() + " ptos" : "";
                                label18.Text = res["PtoPersePersis"].ToString() != "" ? res["PtoPersePersis"].ToString() + " ptos" : "";
                                label23.Text = res["PtoAutodAutocon"].ToString() != "" ? res["PtoAutodAutocon"].ToString() + " ptos" : "";


                                label10.Text = res["autoIndepen"].ToString() != "" ? res["autoIndepen"].ToString()  : "";
                                label8.Text = res["tenacidadResol"].ToString() != "" ? res["tenacidadResol"].ToString() : "";
                                label4.Text = res["persePersis"].ToString() != "" ? res["persePersis"].ToString()   : "";
                                label6.Text = res["autodAutocon"].ToString() != "" ? res["autodAutocon"].ToString() : "";


                                label13.Text = res["Falseamiento"].ToString() ;

                                //label25.Text = res["DuraPru"].ToString() != "" ? res["DuraPru"].ToString() : "";

                                cualidades.PtoAutoIndepen = label19.Text;
                                cualidades.PtoTenacidadResol = label20.Text;
                                cualidades.PtoPersePersis = label18.Text;
                                cualidades.PtoAutodAutocon = label23.Text;


                                cualidades.autoIndepen = label10.Text;
                                cualidades.tenacidadResol = label8.Text;
                                cualidades.persePersis = label4.Text;
                                cualidades.autodAutocon = label6.Text;

                                cualidades.Falseamiento = label13.Text;


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

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            GraficoVolitiv dsd = new GraficoVolitiv(PtoAutoIndepen.ToString(), PtoTenacidadResol.ToString(), PtoPersePersis.ToString(), PtoAutodAutocon.ToString(), label2.Text);
            dsd.ShowDialog();

        }
    }
}
